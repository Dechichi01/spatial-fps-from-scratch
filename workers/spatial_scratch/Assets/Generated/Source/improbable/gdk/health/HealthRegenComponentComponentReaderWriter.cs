// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System;
using System.Collections.Generic;
using Improbable.Worker.CInterop;
using Improbable.Gdk.Core;
using Improbable.Gdk.Subscriptions;
using Unity.Entities;
using Entity = Unity.Entities.Entity;

namespace Improbable.Gdk.Health
{
    [AutoRegisterSubscriptionManager]
    public class HealthRegenComponentReaderSubscriptionManager : SubscriptionManager<HealthRegenComponentReader>
    {
        private readonly EntityManager entityManager;
        private readonly World world;
        private readonly WorkerSystem workerSystem;

        private Dictionary<EntityId, HashSet<Subscription<HealthRegenComponentReader>>> entityIdToReaderSubscriptions;

        private Dictionary<EntityId, (ulong Added, ulong Removed)> entityIdToCallbackKey =
            new Dictionary<EntityId, (ulong Added, ulong Removed)>();

        private HashSet<EntityId> entitiesMatchingRequirements = new HashSet<EntityId>();
        private HashSet<EntityId> entitiesNotMatchingRequirements = new HashSet<EntityId>();

        public HealthRegenComponentReaderSubscriptionManager(World world)
        {
            this.world = world;
            entityManager = world.GetOrCreateManager<EntityManager>();

            // todo Check that these are there
            workerSystem = world.GetExistingManager<WorkerSystem>();

            var constraintCallbackSystem = world.GetExistingManager<ComponentConstraintsCallbackSystem>();

            constraintCallbackSystem.RegisterComponentAddedCallback(HealthRegenComponent.ComponentId, entityId =>
            {
                if (!entitiesNotMatchingRequirements.Contains(entityId))
                {
                    return;
                }

                workerSystem.TryGetEntity(entityId, out var entity);

                foreach (var subscription in entityIdToReaderSubscriptions[entityId])
                {
                    subscription.SetAvailable(new HealthRegenComponentReader(world, entity, entityId));
                }

                entitiesMatchingRequirements.Add(entityId);
                entitiesNotMatchingRequirements.Remove(entityId);
            });

            constraintCallbackSystem.RegisterComponentRemovedCallback(HealthRegenComponent.ComponentId, entityId =>
            {
                if (!entitiesMatchingRequirements.Contains(entityId))
                {
                    return;
                }

                workerSystem.TryGetEntity(entityId, out var entity);

                foreach (var subscription in entityIdToReaderSubscriptions[entityId])
                {
                    ResetValue(subscription);
                    subscription.SetUnavailable();
                }

                entitiesNotMatchingRequirements.Add(entityId);
                entitiesMatchingRequirements.Remove(entityId);
            });
        }

        public override Subscription<HealthRegenComponentReader> Subscribe(EntityId entityId)
        {
            if (entityIdToReaderSubscriptions == null)
            {
                entityIdToReaderSubscriptions = new Dictionary<EntityId, HashSet<Subscription<HealthRegenComponentReader>>>();
            }

            var subscription = new Subscription<HealthRegenComponentReader>(this, entityId);

            if (!entityIdToReaderSubscriptions.TryGetValue(entityId, out var subscriptions))
            {
                subscriptions = new HashSet<Subscription<HealthRegenComponentReader>>();
                entityIdToReaderSubscriptions.Add(entityId, subscriptions);
            }

            if (workerSystem.TryGetEntity(entityId, out var entity)
                && entityManager.HasComponent<HealthRegenComponent.Component>(entity))
            {
                entitiesMatchingRequirements.Add(entityId);
                subscription.SetAvailable(new HealthRegenComponentReader(world, entity, entityId));
            }
            else
            {
                entitiesNotMatchingRequirements.Add(entityId);
            }

            subscriptions.Add(subscription);
            return subscription;
        }

        public override void Cancel(ISubscription subscription)
        {
            var sub = ((Subscription<HealthRegenComponentReader>) subscription);
            if (sub.HasValue)
            {
                var reader = sub.Value;
                reader.IsValid = false;
                reader.RemoveAllCallbacks();
            }

            var subscriptions = entityIdToReaderSubscriptions[sub.EntityId];
            subscriptions.Remove(sub);
            if (subscriptions.Count == 0)
            {
                entityIdToReaderSubscriptions.Remove(sub.EntityId);
                entitiesMatchingRequirements.Remove(sub.EntityId);
                entitiesNotMatchingRequirements.Remove(sub.EntityId);
            }
        }

        public override void ResetValue(ISubscription subscription)
        {
            var sub = ((Subscription<HealthRegenComponentReader>) subscription);
            if (sub.HasValue)
            {
                sub.Value.RemoveAllCallbacks();
            }
        }

        private void OnComponentAdded(EntityId entityId)
        {
        }

        private void OnComponentRemoved(EntityId entityId)
        {
        }
    }

    [AutoRegisterSubscriptionManager]
    public class HealthRegenComponentWriterSubscriptionManager : SubscriptionManager<HealthRegenComponentWriter>
    {
        private readonly World world;
        private readonly WorkerSystem workerSystem;
        private readonly ComponentUpdateSystem componentUpdateSystem;

        private Dictionary<EntityId, HashSet<Subscription<HealthRegenComponentWriter>>> entityIdToWriterSubscriptions;

        private HashSet<EntityId> entitiesMatchingRequirements = new HashSet<EntityId>();
        private HashSet<EntityId> entitiesNotMatchingRequirements = new HashSet<EntityId>();

        public HealthRegenComponentWriterSubscriptionManager(World world)
        {
            this.world = world;

            // todo Check that these are there
            workerSystem = world.GetExistingManager<WorkerSystem>();
            componentUpdateSystem = world.GetExistingManager<ComponentUpdateSystem>();

            var constraintCallbackSystem = world.GetExistingManager<ComponentConstraintsCallbackSystem>();

            constraintCallbackSystem.RegisterAuthorityCallback(HealthRegenComponent.ComponentId, authorityChange =>
            {
                if (authorityChange.Authority == Authority.Authoritative)
                {
                    if (!entitiesNotMatchingRequirements.Contains(authorityChange.EntityId))
                    {
                        return;
                    }

                    workerSystem.TryGetEntity(authorityChange.EntityId, out var entity);

                    foreach (var subscription in entityIdToWriterSubscriptions[authorityChange.EntityId])
                    {
                        subscription.SetAvailable(new HealthRegenComponentWriter(world, entity, authorityChange.EntityId));
                    }

                    entitiesMatchingRequirements.Add(authorityChange.EntityId);
                    entitiesNotMatchingRequirements.Remove(authorityChange.EntityId);
                }
                else if (authorityChange.Authority == Authority.NotAuthoritative)
                {
                    if (!entitiesMatchingRequirements.Contains(authorityChange.EntityId))
                    {
                        return;
                    }

                    workerSystem.TryGetEntity(authorityChange.EntityId, out var entity);

                    foreach (var subscription in entityIdToWriterSubscriptions[authorityChange.EntityId])
                    {
                        ResetValue(subscription);
                        subscription.SetUnavailable();
                    }

                    entitiesNotMatchingRequirements.Add(authorityChange.EntityId);
                    entitiesMatchingRequirements.Remove(authorityChange.EntityId);
                }
            });
        }

        public override Subscription<HealthRegenComponentWriter> Subscribe(EntityId entityId)
        {
            if (entityIdToWriterSubscriptions == null)
            {
                entityIdToWriterSubscriptions = new Dictionary<EntityId, HashSet<Subscription<HealthRegenComponentWriter>>>();
            }

            var subscription = new Subscription<HealthRegenComponentWriter>(this, entityId);

            if (!entityIdToWriterSubscriptions.TryGetValue(entityId, out var subscriptions))
            {
                subscriptions = new HashSet<Subscription<HealthRegenComponentWriter>>();
                entityIdToWriterSubscriptions.Add(entityId, subscriptions);
            }

            if (workerSystem.TryGetEntity(entityId, out var entity)
                && componentUpdateSystem.HasComponent(HealthRegenComponent.ComponentId, entityId)
                && componentUpdateSystem.GetAuthority(entityId, HealthRegenComponent.ComponentId) != Authority.NotAuthoritative)
            {
                entitiesMatchingRequirements.Add(entityId);
                subscription.SetAvailable(new HealthRegenComponentWriter(world, entity, entityId));
            }
            else
            {
                entitiesNotMatchingRequirements.Add(entityId);
            }

            subscriptions.Add(subscription);
            return subscription;
        }

        public override void Cancel(ISubscription subscription)
        {
            var sub = ((Subscription<HealthRegenComponentWriter>) subscription);
            if (sub.HasValue)
            {
                var reader = sub.Value;
                reader.IsValid = false;
                reader.RemoveAllCallbacks();
            }

            var subscriptions = entityIdToWriterSubscriptions[sub.EntityId];
            subscriptions.Remove(sub);
            if (subscriptions.Count == 0)
            {
                entityIdToWriterSubscriptions.Remove(sub.EntityId);
                entitiesMatchingRequirements.Remove(sub.EntityId);
                entitiesNotMatchingRequirements.Remove(sub.EntityId);
            }
        }

        public override void ResetValue(ISubscription subscription)
        {
            var sub = ((Subscription<HealthRegenComponentWriter>) subscription);
            if (sub.HasValue)
            {
                sub.Value.RemoveAllCallbacks();
            }
        }
    }

    public class HealthRegenComponentReader
    {
        public bool IsValid;

        protected readonly ComponentUpdateSystem ComponentUpdateSystem;
        protected readonly ComponentCallbackSystem CallbackSystem;
        protected readonly EntityManager EntityManager;
        protected readonly Entity Entity;
        protected readonly EntityId EntityId;

        public HealthRegenComponent.Component Data
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException("Oh noes!");
                }

                return EntityManager.GetComponentData<HealthRegenComponent.Component>(Entity);
            }
        }

        public Authority Authority
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException("Oh noes!");
                }

                return ComponentUpdateSystem.GetAuthority(EntityId, HealthRegenComponent.ComponentId);
            }
        }

        private Dictionary<Action<Authority>, ulong> authorityCallbackToCallbackKey;
        public event Action<Authority> OnAuthorityUpdate
        {
            add
            {
                if (authorityCallbackToCallbackKey == null)
                {
                    authorityCallbackToCallbackKey = new Dictionary<Action<Authority>, ulong>();
                }

                var key = CallbackSystem.RegisterAuthorityCallback(EntityId, HealthRegenComponent.ComponentId, value);
                authorityCallbackToCallbackKey.Add(value, key);
            }
            remove
            {
                if (!authorityCallbackToCallbackKey.TryGetValue(value, out var key))
                {
                    return;
                }

                CallbackSystem.UnregisterCallback(key);
                authorityCallbackToCallbackKey.Remove(value);
            }
        }

        private Dictionary<Action<HealthRegenComponent.Update>, ulong> updateCallbackToCallbackKey;
        public event Action<HealthRegenComponent.Update> OnUpdate
        {
            add
            {
                if (updateCallbackToCallbackKey == null)
                {
                    updateCallbackToCallbackKey = new Dictionary<Action<HealthRegenComponent.Update>, ulong>();
                }

                var key = CallbackSystem.RegisterComponentUpdateCallback(EntityId, value);
                updateCallbackToCallbackKey.Add(value, key);
            }
            remove
            {
                if (!updateCallbackToCallbackKey.TryGetValue(value, out var key))
                {
                    return;
                }

                CallbackSystem.UnregisterCallback(key);
                updateCallbackToCallbackKey.Remove(value);
            }
        }

        private Dictionary<Action<BlittableBool>, ulong> damagedRecentlyUpdateCallbackToCallbackKey;
        public event Action<BlittableBool> OnDamagedRecentlyUpdate
        {
            add
            {
                if (damagedRecentlyUpdateCallbackToCallbackKey == null)
                {
                    damagedRecentlyUpdateCallbackToCallbackKey = new Dictionary<Action<BlittableBool>, ulong>();
                }

                var key = CallbackSystem.RegisterComponentUpdateCallback<HealthRegenComponent.Update>(EntityId, update =>
                {
                    if (update.DamagedRecently.HasValue)
                    {
                        value(update.DamagedRecently.Value);
                    }
                });
                damagedRecentlyUpdateCallbackToCallbackKey.Add(value, key);
            }
            remove
            {
                if (!damagedRecentlyUpdateCallbackToCallbackKey.TryGetValue(value, out var key))
                {
                    return;
                }

                CallbackSystem.UnregisterCallback(key);
                damagedRecentlyUpdateCallbackToCallbackKey.Remove(value);
            }
        }

        private Dictionary<Action<float>, ulong> regenCooldownTimerUpdateCallbackToCallbackKey;
        public event Action<float> OnRegenCooldownTimerUpdate
        {
            add
            {
                if (regenCooldownTimerUpdateCallbackToCallbackKey == null)
                {
                    regenCooldownTimerUpdateCallbackToCallbackKey = new Dictionary<Action<float>, ulong>();
                }

                var key = CallbackSystem.RegisterComponentUpdateCallback<HealthRegenComponent.Update>(EntityId, update =>
                {
                    if (update.RegenCooldownTimer.HasValue)
                    {
                        value(update.RegenCooldownTimer.Value);
                    }
                });
                regenCooldownTimerUpdateCallbackToCallbackKey.Add(value, key);
            }
            remove
            {
                if (!regenCooldownTimerUpdateCallbackToCallbackKey.TryGetValue(value, out var key))
                {
                    return;
                }

                CallbackSystem.UnregisterCallback(key);
                regenCooldownTimerUpdateCallbackToCallbackKey.Remove(value);
            }
        }

        private Dictionary<Action<float>, ulong> cooldownSyncIntervalUpdateCallbackToCallbackKey;
        public event Action<float> OnCooldownSyncIntervalUpdate
        {
            add
            {
                if (cooldownSyncIntervalUpdateCallbackToCallbackKey == null)
                {
                    cooldownSyncIntervalUpdateCallbackToCallbackKey = new Dictionary<Action<float>, ulong>();
                }

                var key = CallbackSystem.RegisterComponentUpdateCallback<HealthRegenComponent.Update>(EntityId, update =>
                {
                    if (update.CooldownSyncInterval.HasValue)
                    {
                        value(update.CooldownSyncInterval.Value);
                    }
                });
                cooldownSyncIntervalUpdateCallbackToCallbackKey.Add(value, key);
            }
            remove
            {
                if (!cooldownSyncIntervalUpdateCallbackToCallbackKey.TryGetValue(value, out var key))
                {
                    return;
                }

                CallbackSystem.UnregisterCallback(key);
                cooldownSyncIntervalUpdateCallbackToCallbackKey.Remove(value);
            }
        }

        private Dictionary<Action<float>, ulong> regenPauseTimeUpdateCallbackToCallbackKey;
        public event Action<float> OnRegenPauseTimeUpdate
        {
            add
            {
                if (regenPauseTimeUpdateCallbackToCallbackKey == null)
                {
                    regenPauseTimeUpdateCallbackToCallbackKey = new Dictionary<Action<float>, ulong>();
                }

                var key = CallbackSystem.RegisterComponentUpdateCallback<HealthRegenComponent.Update>(EntityId, update =>
                {
                    if (update.RegenPauseTime.HasValue)
                    {
                        value(update.RegenPauseTime.Value);
                    }
                });
                regenPauseTimeUpdateCallbackToCallbackKey.Add(value, key);
            }
            remove
            {
                if (!regenPauseTimeUpdateCallbackToCallbackKey.TryGetValue(value, out var key))
                {
                    return;
                }

                CallbackSystem.UnregisterCallback(key);
                regenPauseTimeUpdateCallbackToCallbackKey.Remove(value);
            }
        }

        private Dictionary<Action<float>, ulong> regenIntervalUpdateCallbackToCallbackKey;
        public event Action<float> OnRegenIntervalUpdate
        {
            add
            {
                if (regenIntervalUpdateCallbackToCallbackKey == null)
                {
                    regenIntervalUpdateCallbackToCallbackKey = new Dictionary<Action<float>, ulong>();
                }

                var key = CallbackSystem.RegisterComponentUpdateCallback<HealthRegenComponent.Update>(EntityId, update =>
                {
                    if (update.RegenInterval.HasValue)
                    {
                        value(update.RegenInterval.Value);
                    }
                });
                regenIntervalUpdateCallbackToCallbackKey.Add(value, key);
            }
            remove
            {
                if (!regenIntervalUpdateCallbackToCallbackKey.TryGetValue(value, out var key))
                {
                    return;
                }

                CallbackSystem.UnregisterCallback(key);
                regenIntervalUpdateCallbackToCallbackKey.Remove(value);
            }
        }

        private Dictionary<Action<float>, ulong> regenAmountUpdateCallbackToCallbackKey;
        public event Action<float> OnRegenAmountUpdate
        {
            add
            {
                if (regenAmountUpdateCallbackToCallbackKey == null)
                {
                    regenAmountUpdateCallbackToCallbackKey = new Dictionary<Action<float>, ulong>();
                }

                var key = CallbackSystem.RegisterComponentUpdateCallback<HealthRegenComponent.Update>(EntityId, update =>
                {
                    if (update.RegenAmount.HasValue)
                    {
                        value(update.RegenAmount.Value);
                    }
                });
                regenAmountUpdateCallbackToCallbackKey.Add(value, key);
            }
            remove
            {
                if (!regenAmountUpdateCallbackToCallbackKey.TryGetValue(value, out var key))
                {
                    return;
                }

                CallbackSystem.UnregisterCallback(key);
                regenAmountUpdateCallbackToCallbackKey.Remove(value);
            }
        }


        internal HealthRegenComponentReader(World world, Entity entity, EntityId entityId)
        {
            Entity = entity;
            EntityId = entityId;

            IsValid = true;

            ComponentUpdateSystem = world.GetExistingManager<ComponentUpdateSystem>();
            CallbackSystem = world.GetExistingManager<ComponentCallbackSystem>();
            EntityManager = world.GetExistingManager<EntityManager>();
        }

        public void RemoveAllCallbacks()
        {
            if (authorityCallbackToCallbackKey != null)
            {
                foreach (var callbackToKey in authorityCallbackToCallbackKey)
                {
                    CallbackSystem.UnregisterCallback(callbackToKey.Value);
                }

                authorityCallbackToCallbackKey.Clear();
            }

            if (updateCallbackToCallbackKey != null)
            {
                foreach (var callbackToKey in updateCallbackToCallbackKey)
                {
                    CallbackSystem.UnregisterCallback(callbackToKey.Value);
                }

                updateCallbackToCallbackKey.Clear();
            }


            if (damagedRecentlyUpdateCallbackToCallbackKey != null)
            {
                foreach (var callbackToKey in damagedRecentlyUpdateCallbackToCallbackKey)
                {
                    CallbackSystem.UnregisterCallback(callbackToKey.Value);
                }

                damagedRecentlyUpdateCallbackToCallbackKey.Clear();
            }

            if (regenCooldownTimerUpdateCallbackToCallbackKey != null)
            {
                foreach (var callbackToKey in regenCooldownTimerUpdateCallbackToCallbackKey)
                {
                    CallbackSystem.UnregisterCallback(callbackToKey.Value);
                }

                regenCooldownTimerUpdateCallbackToCallbackKey.Clear();
            }

            if (cooldownSyncIntervalUpdateCallbackToCallbackKey != null)
            {
                foreach (var callbackToKey in cooldownSyncIntervalUpdateCallbackToCallbackKey)
                {
                    CallbackSystem.UnregisterCallback(callbackToKey.Value);
                }

                cooldownSyncIntervalUpdateCallbackToCallbackKey.Clear();
            }

            if (regenPauseTimeUpdateCallbackToCallbackKey != null)
            {
                foreach (var callbackToKey in regenPauseTimeUpdateCallbackToCallbackKey)
                {
                    CallbackSystem.UnregisterCallback(callbackToKey.Value);
                }

                regenPauseTimeUpdateCallbackToCallbackKey.Clear();
            }

            if (regenIntervalUpdateCallbackToCallbackKey != null)
            {
                foreach (var callbackToKey in regenIntervalUpdateCallbackToCallbackKey)
                {
                    CallbackSystem.UnregisterCallback(callbackToKey.Value);
                }

                regenIntervalUpdateCallbackToCallbackKey.Clear();
            }

            if (regenAmountUpdateCallbackToCallbackKey != null)
            {
                foreach (var callbackToKey in regenAmountUpdateCallbackToCallbackKey)
                {
                    CallbackSystem.UnregisterCallback(callbackToKey.Value);
                }

                regenAmountUpdateCallbackToCallbackKey.Clear();
            }
        }
    }

    public class HealthRegenComponentWriter : HealthRegenComponentReader
    {
        internal HealthRegenComponentWriter(World world, Entity entity, EntityId entityId)
            : base(world, entity, entityId)
        {
        }

        public void SendUpdate(HealthRegenComponent.Update update)
        {
            var component = EntityManager.GetComponentData<HealthRegenComponent.Component>(Entity);

            if (update.DamagedRecently.HasValue)
            {
                component.DamagedRecently = update.DamagedRecently.Value;
            }

            if (update.RegenCooldownTimer.HasValue)
            {
                component.RegenCooldownTimer = update.RegenCooldownTimer.Value;
            }

            if (update.CooldownSyncInterval.HasValue)
            {
                component.CooldownSyncInterval = update.CooldownSyncInterval.Value;
            }

            if (update.RegenPauseTime.HasValue)
            {
                component.RegenPauseTime = update.RegenPauseTime.Value;
            }

            if (update.RegenInterval.HasValue)
            {
                component.RegenInterval = update.RegenInterval.Value;
            }

            if (update.RegenAmount.HasValue)
            {
                component.RegenAmount = update.RegenAmount.Value;
            }

            EntityManager.SetComponentData(Entity, component);
        }


        public void AcknowledgeAuthorityLoss()
        {
            ComponentUpdateSystem.AcknowledgeAuthorityLoss(EntityId, HealthRegenComponent.ComponentId);
        }
    }
}
