using Improbable.Gdk.Core;
using Improbable.Gdk.ReactiveComponents;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Improbable.Gdk.Health
{
    [UpdateInGroup(typeof(SpatialOSUpdateGroup))]
    [UpdateAfter(typeof(ServerHealthModifierSystem))]
    public class HealthRegenSystem : ComponentSystem
    {
        public struct EntitiesNeedingRegenData
        {
            public readonly int Length;
            [ReadOnly] public EntityArray Entities;
            [ReadOnly] public ComponentDataArray<HealthRegenComponent.Component> HealthRegenComponents;
            [ReadOnly] public SubtractiveComponent<HealthRegenData> DenotesMissingData;
            [ReadOnly] public ComponentDataArray<Authoritative<HealthComponent.Component>> DenotesAuthority;
        }

        public struct TakingDamage
        {
            public readonly int Length;
            public ComponentDataArray<HealthRegenData> RegenData;
            public ComponentDataArray<HealthRegenComponent.Component> HealthRegenComponents;
            [ReadOnly] public ComponentDataArray<HealthComponent.ReceivedEvents.HealthModified> HealthModifiedEvents;
            [ReadOnly] public ComponentDataArray<Authoritative<HealthComponent.Component>> DenotesAuthority;
        }

        public struct EntitiesToRegen
        {
            public readonly int Length;
            public ComponentDataArray<HealthComponent.CommandSenders.ModifyHealth> ModifyHealthCommandSenders;
            public ComponentDataArray<HealthRegenComponent.Component> HealthRegenComponents;
            public ComponentDataArray<HealthRegenData> RegenData;
            [ReadOnly] public ComponentDataArray<HealthComponent.Component> HealthComponents;
            [ReadOnly] public ComponentDataArray<SpatialEntityId> EntityId;
            [ReadOnly] public ComponentDataArray<Authoritative<HealthComponent.Component>> DenotesAuthority;
        }

        [Inject] private EntitiesNeedingRegenData needData;
        [Inject] private TakingDamage takingDamage;
        [Inject] private EntitiesToRegen toRegen;

        protected override void OnUpdate()
        {
            Debug.Log("---> Running");
            for (var i = 0; i < needData.Length; i++)
            {
                AddHealthRegenData(i);
            }

            for (var i = 0; i < takingDamage.Length; i++)
            {
                HandleDamageEvents(i);
            }

            for (var i = 0; i < toRegen.Length; i++)
            {
                UpdateTimersAndRegenHealth(i);
            }
        }

        private void AddHealthRegenData(int i)
        {
            var healthRegenComponent = needData.HealthRegenComponents[i];

            var regenData = new HealthRegenData();

            if (healthRegenComponent.DamagedRecently)
            {
                regenData.DamagedRecentlyTimer = healthRegenComponent.RegenCooldownTimer;
                regenData.NextSpatialSyncTimer = healthRegenComponent.CooldownSyncInterval;
            }

            PostUpdateCommands.AddComponent(needData.Entities[i], regenData);
        }

        private void ResetTimers(HealthRegenComponent.Component regenComponent, HealthRegenData regenData)
        {
            regenComponent.RegenCooldownTimer = 0;
            regenData.DamagedRecentlyTimer = 0;
            regenData.NextSpatialSyncTimer = regenComponent.CooldownSyncInterval;
        }

        private void HandleDamageEvents(int i)
        {
            if (!WasDamagedRecently(takingDamage.HealthModifiedEvents[i]))
            {
                return;
            }

            var regenComponent = takingDamage.HealthRegenComponents[i];
            var regenData = takingDamage.RegenData[i];

            regenComponent.DamagedRecently = true;
            ResetTimers(regenComponent, regenData);

            takingDamage.HealthRegenComponents[i] = regenComponent;
            takingDamage.RegenData[i] = regenData;
        }

        private bool WasDamagedRecently(HealthComponent.ReceivedEvents.HealthModified healthModifiedEvents)
        {
            foreach (var modifiedEvent in healthModifiedEvents.Events)
            {
                var modifier = modifiedEvent.Modifier;
                if (modifier.Amount < 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateTimersAndRegenHealth(int i)
        {
            var healthComponent = toRegen.HealthComponents[i];
            var regenComponent = toRegen.HealthRegenComponents[i];
            var regenData = toRegen.RegenData[i];

            if (healthComponent.Health <= 0)
            {
                return;
            }

            // If damaged recently, tick down the timer.
            if (regenComponent.DamagedRecently)
            {
                UpdateDamagedRecentlyTimers(i, regenComponent, regenData);
            }
            else
            {
                // If not damaged recently, and not already fully healed, regen.
                RegenHealth(i, healthComponent, regenComponent, regenData);
            }
        }

        private void UpdateDamagedRecentlyTimers(
            int i,
            HealthRegenComponent.Component regenComponent,
            HealthRegenData regenData)
        {
            regenData.DamagedRecentlyTimer -= Time.deltaTime;

            if (regenData.DamagedRecentlyTimer <= 0)
            {
                regenData.DamagedRecentlyTimer = 0;
                regenComponent.DamagedRecently = false;
                regenComponent.RegenCooldownTimer = 0;
                toRegen.HealthRegenComponents[i] = regenComponent;
            }
            else
            {
                // Send a spatial update once every CooldownSyncInterval.
                regenData.NextSpatialSyncTimer -= Time.deltaTime;
                if (regenData.NextSpatialSyncTimer <= 0)
                {
                    regenData.NextSpatialSyncTimer += regenComponent.CooldownSyncInterval;
                    regenComponent.RegenCooldownTimer = regenData.DamagedRecentlyTimer;
                    toRegen.HealthRegenComponents[i] = regenComponent;
                }
            }

            toRegen.RegenData[i] = regenData;
        }

        private void RegenHealth(int i,
            HealthComponent.Component healthComponent,
            HealthRegenComponent.Component regenComponent,
            HealthRegenData regenData)
        {
            if (healthComponent.Health >= healthComponent.MaxHealth) { return; }

            regenData.NextRegenTimer -= Time.deltaTime;

            if (regenData.NextRegenTimer <= 0)
            {
                regenData.NextRegenTimer += regenComponent.RegenInterval;

                // Send command to regen entity.
                var commandSender = toRegen.ModifyHealthCommandSenders[i];
                var modifyHealthRequest = new HealthComponent.ModifyHealth.Request(
                    toRegen.EntityId[i].EntityId,
                    new HealthModifier()
                    {
                        Amount = regenComponent.RegenAmount
                    });
                commandSender.RequestsToSend.Add(modifyHealthRequest);
            }

            toRegen.RegenData[i] = regenData;
        }
    }
}
