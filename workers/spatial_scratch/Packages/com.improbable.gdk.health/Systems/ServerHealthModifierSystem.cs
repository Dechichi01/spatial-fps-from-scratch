using Improbable.Gdk.Core;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Improbable.Gdk.Health
{
    [UpdateInGroup(typeof(SpatialOSUpdateGroup))]
    public class ServerHealthModifierSystem : ComponentSystem
    {
        private struct EntitiesWithModifiedHealth
        {
            public readonly int Length;
            [ReadOnly] public EntityArray Entity;
            public ComponentDataArray<HealthComponent.Component> HealthComponents;
            [ReadOnly] public ComponentDataArray<HealthComponent.CommandRequests.ModifyHealth> ModifyHealthRequests;
            public ComponentDataArray<HealthComponent.EventSender.HealthModified> HealthModifiedEventSenders;
        }

        [Inject] private EntitiesWithModifiedHealth entitiesWithModifiedHealth;

        protected override void OnUpdate()
        {
            for (int i = 0; i < entitiesWithModifiedHealth.Length; i++)
            {
                var healthComp = entitiesWithModifiedHealth.HealthComponents[i];

                if (healthComp.Health <= 0)
                {
                    return;
                }

                var modifyHealthCommand = entitiesWithModifiedHealth.ModifyHealthRequests[i];

                for (int j = 0; j < modifyHealthCommand.Requests.Count; j++)
                {
                    var healthModifiedInfo = HandleModifyHealthRequest(
                         modifyHealthCommand.Requests[j].Payload, ref healthComp);

                    entitiesWithModifiedHealth.HealthModifiedEventSenders[i].Events.Add(healthModifiedInfo);

                    if (healthModifiedInfo.Died)
                    {
                        break;
                    }
                }

                entitiesWithModifiedHealth.HealthComponents[i] = healthComp;
            }
        }

        private HealthModifiedInfo HandleModifyHealthRequest(HealthModifier modifier, ref HealthComponent.Component healthComp)
        {
            var newHealth = Mathf.Clamp(healthComp.Health + modifier.Amount, 0, healthComp.MaxHealth);
            var died = newHealth <= 0;

            var healthModifiedInfo = new HealthModifiedInfo(
                modifier: modifier,
                healthBefore: healthComp.Health,
                healthAfter: newHealth,
                died: died);

            healthComp.Health = newHealth;

            return healthModifiedInfo;
        }
    }
}