// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System;
using System.Collections.Generic;
using Unity.Entities;
using Improbable.Gdk.Core;
using Improbable.Gdk.Core.Commands;
using Improbable.Gdk.ReactiveComponents;
using Improbable.Worker.CInterop;
using Entity = Unity.Entities.Entity;

namespace Improbable.Gdk.Health
{
    public partial class HealthComponent
    {
        public class ModifyHealthReactiveCommandComponentManager : IReactiveCommandComponentManager
        {
            public void PopulateReactiveCommandComponents(CommandSystem commandSystem, EntityManager entityManager, WorkerSystem workerSystem, World world)
            {
                var receivedRequests = commandSystem.GetRequests<ModifyHealth.ReceivedRequest>();
                // todo Not efficient if it keeps jumping all over entities but don't care right now
                for (int i = 0; i < receivedRequests.Count; ++i)
                {
                    if (!workerSystem.TryGetEntity(receivedRequests[i].EntityId, out var entity))
                    {
                        continue;
                    }

                    List<ModifyHealth.ReceivedRequest> requests;
                    if (entityManager.HasComponent<Improbable.Gdk.Health.HealthComponent.CommandRequests.ModifyHealth>(entity))
                    {
                        requests = entityManager.GetComponentData<Improbable.Gdk.Health.HealthComponent.CommandRequests.ModifyHealth>(entity).Requests;
                    }
                    else
                    {
                        var data = new Improbable.Gdk.Health.HealthComponent.CommandRequests.ModifyHealth
                        {
                            CommandListHandle = ReferenceTypeProviders.ModifyHealthRequestsProvider.Allocate(world)
                        };
                        data.Requests = new List<ModifyHealth.ReceivedRequest>();
                        requests = data.Requests;
                        entityManager.AddComponentData(entity, data);
                    }

                    requests.Add(receivedRequests[i]);
                }


                var receivedResponses = commandSystem.GetResponses<ModifyHealth.ReceivedResponse>();
                // todo Not efficient if it keeps jumping all over entities but don't care right now
                for (int i = 0; i < receivedResponses.Count; ++i)
                {
                    ref readonly var response = ref receivedResponses[i];

                    if (response.SendingEntity == Unity.Entities.Entity.Null || !entityManager.Exists(response.SendingEntity))
                    {
                        continue;
                    }

                    List<ModifyHealth.ReceivedResponse> responses;
                    if (entityManager.HasComponent<Improbable.Gdk.Health.HealthComponent.CommandResponses.ModifyHealth>(response.SendingEntity))
                    {
                        responses = entityManager.GetComponentData<Improbable.Gdk.Health.HealthComponent.CommandResponses.ModifyHealth>(response.SendingEntity).Responses;
                    }
                    else
                    {
                        var data = new Improbable.Gdk.Health.HealthComponent.CommandResponses.ModifyHealth
                        {
                            CommandListHandle = ReferenceTypeProviders.ModifyHealthResponsesProvider.Allocate(world)
                        };
                        data.Responses = new List<ModifyHealth.ReceivedResponse>();
                        responses = data.Responses;
                        entityManager.AddComponentData(response.SendingEntity, data);
                    }

                    responses.Add(response);
                }
            }

            public void Clean(World world)
            {
                ReferenceTypeProviders.ModifyHealthRequestsProvider.CleanDataInWorld(world);
                ReferenceTypeProviders.ModifyHealthResponsesProvider.CleanDataInWorld(world);
            }
        }

        public class ModifyHealthCommandSenderComponentManager : ICommandSenderComponentManager
        {
            private Dictionary<EntityId, (uint Sender, uint Responder)> entityIdToAllocatedHandles =
                new Dictionary<EntityId, (uint Sender, uint Responder)>();

            public void AddComponents(Entity entity, EntityManager entityManager, World world)
            {
                // todo error message if not the worker entity or spatial entity
                EntityId entityId = entityManager.HasComponent<SpatialEntityId>(entity)
                    ? entityManager.GetComponentData<SpatialEntityId>(entity).EntityId
                    : new EntityId(0);

                var commandSender = new Improbable.Gdk.Health.HealthComponent.CommandSenders.ModifyHealth();
                commandSender.CommandListHandle = Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.ModifyHealthSenderProvider.Allocate(world);
                commandSender.RequestsToSend = new List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.Request>();

                entityManager.AddComponentData(entity, commandSender);

                var commandResponder = new Improbable.Gdk.Health.HealthComponent.CommandResponders.ModifyHealth();
                commandResponder.CommandListHandle =
                    Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.ModifyHealthResponderProvider.Allocate(world);
                commandResponder.ResponsesToSend = new List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.Response>();

                entityManager.AddComponentData(entity, commandResponder);

                entityIdToAllocatedHandles.Add(entityId, (commandSender.CommandListHandle, commandResponder.CommandListHandle));
            }

            public void RemoveComponents(EntityId entityId, EntityManager entityManager, World world)
            {
                var workerSystem = world.GetExistingManager<WorkerSystem>();

                workerSystem.TryGetEntity(entityId, out var entity);

                if (entity != Entity.Null)
                {
                    entityManager.RemoveComponent<CommandSenders.ModifyHealth>(entity);
                    entityManager.RemoveComponent<CommandResponders.ModifyHealth>(entity);
                }

                if (!entityIdToAllocatedHandles.TryGetValue(entityId, out var handles))
                {
                    throw new ArgumentException("Command components not added to entity");
                }

                entityIdToAllocatedHandles.Remove(entityId);

                ReferenceTypeProviders.ModifyHealthSenderProvider.Free(handles.Sender);
                ReferenceTypeProviders.ModifyHealthResponderProvider.Free(handles.Responder);
            }

            public void Clean(World world)
            {
                ReferenceTypeProviders.ModifyHealthSenderProvider.CleanDataInWorld(world);
                ReferenceTypeProviders.ModifyHealthResponderProvider.CleanDataInWorld(world);
            }
        }

        public class RequestRespawnReactiveCommandComponentManager : IReactiveCommandComponentManager
        {
            public void PopulateReactiveCommandComponents(CommandSystem commandSystem, EntityManager entityManager, WorkerSystem workerSystem, World world)
            {
                var receivedRequests = commandSystem.GetRequests<RequestRespawn.ReceivedRequest>();
                // todo Not efficient if it keeps jumping all over entities but don't care right now
                for (int i = 0; i < receivedRequests.Count; ++i)
                {
                    if (!workerSystem.TryGetEntity(receivedRequests[i].EntityId, out var entity))
                    {
                        continue;
                    }

                    List<RequestRespawn.ReceivedRequest> requests;
                    if (entityManager.HasComponent<Improbable.Gdk.Health.HealthComponent.CommandRequests.RequestRespawn>(entity))
                    {
                        requests = entityManager.GetComponentData<Improbable.Gdk.Health.HealthComponent.CommandRequests.RequestRespawn>(entity).Requests;
                    }
                    else
                    {
                        var data = new Improbable.Gdk.Health.HealthComponent.CommandRequests.RequestRespawn
                        {
                            CommandListHandle = ReferenceTypeProviders.RequestRespawnRequestsProvider.Allocate(world)
                        };
                        data.Requests = new List<RequestRespawn.ReceivedRequest>();
                        requests = data.Requests;
                        entityManager.AddComponentData(entity, data);
                    }

                    requests.Add(receivedRequests[i]);
                }


                var receivedResponses = commandSystem.GetResponses<RequestRespawn.ReceivedResponse>();
                // todo Not efficient if it keeps jumping all over entities but don't care right now
                for (int i = 0; i < receivedResponses.Count; ++i)
                {
                    ref readonly var response = ref receivedResponses[i];

                    if (response.SendingEntity == Unity.Entities.Entity.Null || !entityManager.Exists(response.SendingEntity))
                    {
                        continue;
                    }

                    List<RequestRespawn.ReceivedResponse> responses;
                    if (entityManager.HasComponent<Improbable.Gdk.Health.HealthComponent.CommandResponses.RequestRespawn>(response.SendingEntity))
                    {
                        responses = entityManager.GetComponentData<Improbable.Gdk.Health.HealthComponent.CommandResponses.RequestRespawn>(response.SendingEntity).Responses;
                    }
                    else
                    {
                        var data = new Improbable.Gdk.Health.HealthComponent.CommandResponses.RequestRespawn
                        {
                            CommandListHandle = ReferenceTypeProviders.RequestRespawnResponsesProvider.Allocate(world)
                        };
                        data.Responses = new List<RequestRespawn.ReceivedResponse>();
                        responses = data.Responses;
                        entityManager.AddComponentData(response.SendingEntity, data);
                    }

                    responses.Add(response);
                }
            }

            public void Clean(World world)
            {
                ReferenceTypeProviders.RequestRespawnRequestsProvider.CleanDataInWorld(world);
                ReferenceTypeProviders.RequestRespawnResponsesProvider.CleanDataInWorld(world);
            }
        }

        public class RequestRespawnCommandSenderComponentManager : ICommandSenderComponentManager
        {
            private Dictionary<EntityId, (uint Sender, uint Responder)> entityIdToAllocatedHandles =
                new Dictionary<EntityId, (uint Sender, uint Responder)>();

            public void AddComponents(Entity entity, EntityManager entityManager, World world)
            {
                // todo error message if not the worker entity or spatial entity
                EntityId entityId = entityManager.HasComponent<SpatialEntityId>(entity)
                    ? entityManager.GetComponentData<SpatialEntityId>(entity).EntityId
                    : new EntityId(0);

                var commandSender = new Improbable.Gdk.Health.HealthComponent.CommandSenders.RequestRespawn();
                commandSender.CommandListHandle = Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.RequestRespawnSenderProvider.Allocate(world);
                commandSender.RequestsToSend = new List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.Request>();

                entityManager.AddComponentData(entity, commandSender);

                var commandResponder = new Improbable.Gdk.Health.HealthComponent.CommandResponders.RequestRespawn();
                commandResponder.CommandListHandle =
                    Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.RequestRespawnResponderProvider.Allocate(world);
                commandResponder.ResponsesToSend = new List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.Response>();

                entityManager.AddComponentData(entity, commandResponder);

                entityIdToAllocatedHandles.Add(entityId, (commandSender.CommandListHandle, commandResponder.CommandListHandle));
            }

            public void RemoveComponents(EntityId entityId, EntityManager entityManager, World world)
            {
                var workerSystem = world.GetExistingManager<WorkerSystem>();

                workerSystem.TryGetEntity(entityId, out var entity);

                if (entity != Entity.Null)
                {
                    entityManager.RemoveComponent<CommandSenders.RequestRespawn>(entity);
                    entityManager.RemoveComponent<CommandResponders.RequestRespawn>(entity);
                }

                if (!entityIdToAllocatedHandles.TryGetValue(entityId, out var handles))
                {
                    throw new ArgumentException("Command components not added to entity");
                }

                entityIdToAllocatedHandles.Remove(entityId);

                ReferenceTypeProviders.RequestRespawnSenderProvider.Free(handles.Sender);
                ReferenceTypeProviders.RequestRespawnResponderProvider.Free(handles.Responder);
            }

            public void Clean(World world)
            {
                ReferenceTypeProviders.RequestRespawnSenderProvider.CleanDataInWorld(world);
                ReferenceTypeProviders.RequestRespawnResponderProvider.CleanDataInWorld(world);
            }
        }

    }
}
