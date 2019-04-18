// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Collections;
using Improbable.Gdk.Core;
using Improbable.Gdk.Subscriptions;
using Improbable.Worker.CInterop;
using Entity = Unity.Entities.Entity;

namespace Improbable.Gdk.Health
{
    [AutoRegisterSubscriptionManager]
    public class HealthComponentCommandSenderSubscriptionManager : SubscriptionManager<HealthComponentCommandSender>
    {
        private readonly World world;
        private readonly WorkerSystem workerSystem;

        private Dictionary<EntityId, HashSet<Subscription<HealthComponentCommandSender>>>
            entityIdToSenderSubscriptions =
                new Dictionary<EntityId, HashSet<Subscription<HealthComponentCommandSender>>>();

        public HealthComponentCommandSenderSubscriptionManager(World world)
        {
            this.world = world;

            // Check that these are there
            workerSystem = world.GetExistingManager<WorkerSystem>();

            var constraintSystem = world.GetExistingManager<ComponentConstraintsCallbackSystem>();

            constraintSystem.RegisterEntityAddedCallback(entityId =>
            {
                if (!entityIdToSenderSubscriptions.TryGetValue(entityId, out var subscriptions))
                {
                    return;
                }

                workerSystem.TryGetEntity(entityId, out var entity);
                foreach (var subscription in subscriptions)
                {
                    if (!subscription.HasValue)
                    {
                        subscription.SetAvailable(new HealthComponentCommandSender(entity, world));
                    }
                }
            });

            constraintSystem.RegisterEntityRemovedCallback(entityId =>
            {
                if (!entityIdToSenderSubscriptions.TryGetValue(entityId, out var subscriptions))
                {
                    return;
                }

                foreach (var subscription in subscriptions)
                {
                    if (subscription.HasValue)
                    {
                        ResetValue(subscription);
                        subscription.SetUnavailable();
                    }
                }
            });
        }

        public override Subscription<HealthComponentCommandSender> Subscribe(EntityId entityId)
        {
            if (entityIdToSenderSubscriptions == null)
            {
                entityIdToSenderSubscriptions = new Dictionary<EntityId, HashSet<Subscription<HealthComponentCommandSender>>>();
            }

            if (entityId.Id < 0)
            {
                throw new ArgumentException("EntityId can not be < 0");
            }

            var subscription = new Subscription<HealthComponentCommandSender>(this, entityId);

            if (!entityIdToSenderSubscriptions.TryGetValue(entityId, out var subscriptions))
            {
                subscriptions = new HashSet<Subscription<HealthComponentCommandSender>>();
                entityIdToSenderSubscriptions.Add(entityId, subscriptions);
            }

            if (workerSystem.TryGetEntity(entityId, out var entity))
            {
                subscription.SetAvailable(new HealthComponentCommandSender(entity, world));
            }
            else if (entityId.Id == 0)
            {
                subscription.SetAvailable(new HealthComponentCommandSender(Entity.Null, world));
            }

            subscriptions.Add(subscription);
            return subscription;
        }

        public override void Cancel(ISubscription subscription)
        {
            var sub = ((Subscription<HealthComponentCommandSender>) subscription);
            if (sub.HasValue)
            {
                var sender = sub.Value;
                sender.IsValid = false;
            }

            var subscriptions = entityIdToSenderSubscriptions[sub.EntityId];
            subscriptions.Remove(sub);
            if (subscriptions.Count == 0)
            {
                entityIdToSenderSubscriptions.Remove(sub.EntityId);
            }
        }

        public override void ResetValue(ISubscription subscription)
        {
            var sub = ((Subscription<HealthComponentCommandSender>) subscription);
            if (sub.HasValue)
            {
                sub.Value.RemoveAllCallbacks();
            }
        }
    }

    [AutoRegisterSubscriptionManager]
    public class HealthComponentCommandReceiverSubscriptionManager : SubscriptionManager<HealthComponentCommandReceiver>
    {
        private readonly World world;
        private readonly WorkerSystem workerSystem;
        private readonly ComponentUpdateSystem componentUpdateSystem;

        private Dictionary<EntityId, HashSet<Subscription<HealthComponentCommandReceiver>>> entityIdToReceiveSubscriptions;

        private HashSet<EntityId> entitiesMatchingRequirements = new HashSet<EntityId>();
        private HashSet<EntityId> entitiesNotMatchingRequirements = new HashSet<EntityId>();

        public HealthComponentCommandReceiverSubscriptionManager(World world)
        {
            this.world = world;

            // Check that these are there
            workerSystem = world.GetExistingManager<WorkerSystem>();
            componentUpdateSystem = world.GetExistingManager<ComponentUpdateSystem>();

            var constraintSystem = world.GetExistingManager<ComponentConstraintsCallbackSystem>();

            constraintSystem.RegisterAuthorityCallback(HealthComponent.ComponentId, authorityChange =>
            {
                if (authorityChange.Authority == Authority.Authoritative)
                {
                    if (!entitiesNotMatchingRequirements.Contains(authorityChange.EntityId))
                    {
                        return;
                    }

                    workerSystem.TryGetEntity(authorityChange.EntityId, out var entity);

                    foreach (var subscription in entityIdToReceiveSubscriptions[authorityChange.EntityId])
                    {
                        subscription.SetAvailable(new HealthComponentCommandReceiver(world, entity, authorityChange.EntityId));
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

                    foreach (var subscription in entityIdToReceiveSubscriptions[authorityChange.EntityId])
                    {
                        ResetValue(subscription);
                        subscription.SetUnavailable();
                    }

                    entitiesNotMatchingRequirements.Add(authorityChange.EntityId);
                    entitiesMatchingRequirements.Remove(authorityChange.EntityId);
                }
            });
        }

        public override Subscription<HealthComponentCommandReceiver> Subscribe(EntityId entityId)
        {
            if (entityIdToReceiveSubscriptions == null)
            {
                entityIdToReceiveSubscriptions = new Dictionary<EntityId, HashSet<Subscription<HealthComponentCommandReceiver>>>();
            }

            var subscription = new Subscription<HealthComponentCommandReceiver>(this, entityId);

            if (!entityIdToReceiveSubscriptions.TryGetValue(entityId, out var subscriptions))
            {
                subscriptions = new HashSet<Subscription<HealthComponentCommandReceiver>>();
                entityIdToReceiveSubscriptions.Add(entityId, subscriptions);
            }

            if (workerSystem.TryGetEntity(entityId, out var entity)
                && componentUpdateSystem.HasComponent(HealthComponent.ComponentId, entityId)
                && componentUpdateSystem.GetAuthority(entityId, HealthComponent.ComponentId) != Authority.NotAuthoritative)
            {
                entitiesMatchingRequirements.Add(entityId);
                subscription.SetAvailable(new HealthComponentCommandReceiver(world, entity, entityId));
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
            var sub = ((Subscription<HealthComponentCommandReceiver>) subscription);
            if (sub.HasValue)
            {
                var receiver = sub.Value;
                receiver.IsValid = false;
                receiver.RemoveAllCallbacks();
            }

            var subscriptions = entityIdToReceiveSubscriptions[sub.EntityId];
            subscriptions.Remove(sub);
            if (subscriptions.Count == 0)
            {
                entityIdToReceiveSubscriptions.Remove(sub.EntityId);
                entitiesMatchingRequirements.Remove(sub.EntityId);
                entitiesNotMatchingRequirements.Remove(sub.EntityId);
            }
        }

        public override void ResetValue(ISubscription subscription)
        {
            var sub = ((Subscription<HealthComponentCommandReceiver>) subscription);
            if (sub.HasValue)
            {
                sub.Value.RemoveAllCallbacks();
            }
        }
    }

    public class HealthComponentCommandSender
    {
        public bool IsValid;

        private readonly Entity entity;
        private readonly CommandSystem commandSender;
        private readonly CommandCallbackSystem callbackSystem;

        private int callbackEpoch;

        internal HealthComponentCommandSender(Entity entity, World world)
        {
            this.entity = entity;
            callbackSystem = world.GetOrCreateManager<CommandCallbackSystem>();
            // todo check that this exists
            commandSender = world.GetExistingManager<CommandSystem>();

            IsValid = true;
        }

        public void SendModifyHealthCommand(EntityId targetEntityId, global::Improbable.Gdk.Health.HealthModifier request, Action<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedResponse> callback = null)
        {
            var commandRequest = new HealthComponent.ModifyHealth.Request(targetEntityId, request);
            SendModifyHealthCommand(commandRequest, callback);
        }

        public void SendModifyHealthCommand(HealthComponent.ModifyHealth.Request request, Action<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedResponse> callback = null)
        {
            int validCallbackEpoch = callbackEpoch;
            var requestId = commandSender.SendCommand(request, entity);
            if (callback != null)
            {
                Action<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedResponse> wrappedCallback = response =>
                {
                    if (!this.IsValid || validCallbackEpoch != this.callbackEpoch)
                    {
                        return;
                    }

                    callback(response);
                };
                callbackSystem.RegisterCommandResponseCallback(requestId, wrappedCallback);
            }
        }
        public void SendRequestRespawnCommand(EntityId targetEntityId, global::Improbable.Common.Empty request, Action<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedResponse> callback = null)
        {
            var commandRequest = new HealthComponent.RequestRespawn.Request(targetEntityId, request);
            SendRequestRespawnCommand(commandRequest, callback);
        }

        public void SendRequestRespawnCommand(HealthComponent.RequestRespawn.Request request, Action<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedResponse> callback = null)
        {
            int validCallbackEpoch = callbackEpoch;
            var requestId = commandSender.SendCommand(request, entity);
            if (callback != null)
            {
                Action<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedResponse> wrappedCallback = response =>
                {
                    if (!this.IsValid || validCallbackEpoch != this.callbackEpoch)
                    {
                        return;
                    }

                    callback(response);
                };
                callbackSystem.RegisterCommandResponseCallback(requestId, wrappedCallback);
            }
        }

        public void RemoveAllCallbacks()
        {
            ++callbackEpoch;
        }
    }

    public class HealthComponentCommandReceiver
    {
        public bool IsValid;

        private readonly EntityId entityId;
        private readonly CommandCallbackSystem callbackSystem;
        private readonly CommandSystem commandSystem;

        private Dictionary<Action<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedRequest>, ulong> modifyHealthCallbackToCallbackKey;

        public event Action<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedRequest> OnModifyHealthRequestReceived
        {
            add
            {
                if (modifyHealthCallbackToCallbackKey == null)
                {
                    modifyHealthCallbackToCallbackKey = new Dictionary<Action<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedRequest>, ulong>();
                }

                var key = callbackSystem.RegisterCommandRequestCallback(entityId, value);
                modifyHealthCallbackToCallbackKey.Add(value, key);
            }
            remove
            {
                if (!modifyHealthCallbackToCallbackKey.TryGetValue(value, out var key))
                {
                    return;
                }

                callbackSystem.UnregisterCommandRequestCallback(key);
                modifyHealthCallbackToCallbackKey.Remove(value);
            }
        }
        private Dictionary<Action<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedRequest>, ulong> requestRespawnCallbackToCallbackKey;

        public event Action<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedRequest> OnRequestRespawnRequestReceived
        {
            add
            {
                if (requestRespawnCallbackToCallbackKey == null)
                {
                    requestRespawnCallbackToCallbackKey = new Dictionary<Action<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedRequest>, ulong>();
                }

                var key = callbackSystem.RegisterCommandRequestCallback(entityId, value);
                requestRespawnCallbackToCallbackKey.Add(value, key);
            }
            remove
            {
                if (!requestRespawnCallbackToCallbackKey.TryGetValue(value, out var key))
                {
                    return;
                }

                callbackSystem.UnregisterCommandRequestCallback(key);
                requestRespawnCallbackToCallbackKey.Remove(value);
            }
        }

        internal HealthComponentCommandReceiver(World world, Entity entity, EntityId entityId)
        {
            this.entityId = entityId;
            callbackSystem = world.GetOrCreateManager<CommandCallbackSystem>();
            commandSystem = world.GetExistingManager<CommandSystem>();
            // should check the system actually exists

            IsValid = true;
        }

        public void SendModifyHealthResponse(Improbable.Gdk.Health.HealthComponent.ModifyHealth.Response response)
        {
            commandSystem.SendResponse(response);
        }

        public void SendModifyHealthResponse(long requestId, global::Improbable.Common.Empty response)
        {
            commandSystem.SendResponse(new Improbable.Gdk.Health.HealthComponent.ModifyHealth.Response(requestId, response));
        }

        public void SendModifyHealthFailure(long requestId, string failureMessage)
        {
            commandSystem.SendResponse(new Improbable.Gdk.Health.HealthComponent.ModifyHealth.Response(requestId, failureMessage));
        }

        public void SendRequestRespawnResponse(Improbable.Gdk.Health.HealthComponent.RequestRespawn.Response response)
        {
            commandSystem.SendResponse(response);
        }

        public void SendRequestRespawnResponse(long requestId, global::Improbable.Common.Empty response)
        {
            commandSystem.SendResponse(new Improbable.Gdk.Health.HealthComponent.RequestRespawn.Response(requestId, response));
        }

        public void SendRequestRespawnFailure(long requestId, string failureMessage)
        {
            commandSystem.SendResponse(new Improbable.Gdk.Health.HealthComponent.RequestRespawn.Response(requestId, failureMessage));
        }

        public void RemoveAllCallbacks()
        {
            if (modifyHealthCallbackToCallbackKey != null)
            {
                foreach (var callbackToKey in modifyHealthCallbackToCallbackKey)
                {
                    callbackSystem.UnregisterCommandRequestCallback(callbackToKey.Value);
                }

                modifyHealthCallbackToCallbackKey.Clear();
            }

            if (requestRespawnCallbackToCallbackKey != null)
            {
                foreach (var callbackToKey in requestRespawnCallbackToCallbackKey)
                {
                    callbackSystem.UnregisterCommandRequestCallback(callbackToKey.Value);
                }

                requestRespawnCallbackToCallbackKey.Clear();
            }

        }
    }
}
