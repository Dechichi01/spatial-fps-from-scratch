// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Collections;
using Improbable.Worker.CInterop;
using Improbable.Gdk.Core;
using Improbable.Gdk.ReactiveComponents;

namespace Improbable.Gdk.Health
{
    public partial class HealthComponent
    {
        internal class ReactiveComponentReplicator : IReactiveComponentReplicationHandler
        {
            public uint ComponentId => 2040;

            public EntityArchetypeQuery EventQuery => new EntityArchetypeQuery
            {
                All = new[]
                {
                    ComponentType.Create<EventSender.HealthModified>(),
                    ComponentType.Create<EventSender.Respawn>(),
                    ComponentType.ReadOnly<SpatialEntityId>()
                },
                Any = Array.Empty<ComponentType>(),
                None = Array.Empty<ComponentType>(),
            };

            public EntityArchetypeQuery[] CommandQueries => new EntityArchetypeQuery[]
            {
                new EntityArchetypeQuery()
                {
                    All = new[]
                    {
                        ComponentType.Create<Improbable.Gdk.Health.HealthComponent.CommandSenders.ModifyHealth>(),
                        ComponentType.Create<Improbable.Gdk.Health.HealthComponent.CommandResponders.ModifyHealth>(),
                    },
                    Any = Array.Empty<ComponentType>(),
                    None = Array.Empty<ComponentType>(),
                },
                new EntityArchetypeQuery()
                {
                    All = new[]
                    {
                        ComponentType.Create<Improbable.Gdk.Health.HealthComponent.CommandSenders.RequestRespawn>(),
                        ComponentType.Create<Improbable.Gdk.Health.HealthComponent.CommandResponders.RequestRespawn>(),
                    },
                    Any = Array.Empty<ComponentType>(),
                    None = Array.Empty<ComponentType>(),
                },
            };

            public void SendEvents(NativeArray<ArchetypeChunk> chunkArray, ComponentSystemBase system, ComponentUpdateSystem componentUpdateSystem)
            {
                Profiler.BeginSample("HealthComponent");

                var spatialOSEntityType = system.GetArchetypeChunkComponentType<SpatialEntityId>(true);
                var eventHealthModifiedType = system.GetArchetypeChunkComponentType<EventSender.HealthModified>(true);
                var eventRespawnType = system.GetArchetypeChunkComponentType<EventSender.Respawn>(true);
                foreach (var chunk in chunkArray)
                {
                    var entityIdArray = chunk.GetNativeArray(spatialOSEntityType);
                    var eventHealthModifiedArray = chunk.GetNativeArray(eventHealthModifiedType);
                    var eventRespawnArray = chunk.GetNativeArray(eventRespawnType);
                    for (var i = 0; i < entityIdArray.Length; i++)
                    {
                        foreach (var e in eventHealthModifiedArray[i].Events)
                        {
                            componentUpdateSystem.SendEvent(new HealthModified.Event(e), entityIdArray[i].EntityId);
                        }

                        eventHealthModifiedArray[i].Events.Clear();
                        foreach (var e in eventRespawnArray[i].Events)
                        {
                            componentUpdateSystem.SendEvent(new Respawn.Event(e), entityIdArray[i].EntityId);
                        }

                        eventRespawnArray[i].Events.Clear();
                    }
                }

                Profiler.EndSample();
            }

            public void SendCommands(NativeArray<ArchetypeChunk> chunkArray, ComponentSystemBase system, CommandSystem commandSystem)
            {
                Profiler.BeginSample("HealthComponent");
                var entityType = system.GetArchetypeChunkEntityType();
                var senderTypeModifyHealth = system.GetArchetypeChunkComponentType<Improbable.Gdk.Health.HealthComponent.CommandSenders.ModifyHealth>(true);
                var responderTypeModifyHealth = system.GetArchetypeChunkComponentType<Improbable.Gdk.Health.HealthComponent.CommandResponders.ModifyHealth>(true);
                var senderTypeRequestRespawn = system.GetArchetypeChunkComponentType<Improbable.Gdk.Health.HealthComponent.CommandSenders.RequestRespawn>(true);
                var responderTypeRequestRespawn = system.GetArchetypeChunkComponentType<Improbable.Gdk.Health.HealthComponent.CommandResponders.RequestRespawn>(true);

                foreach (var chunk in chunkArray)
                {
                    var entities = chunk.GetNativeArray(entityType);
                    if (chunk.Has(senderTypeModifyHealth))
                    {
                        var senders = chunk.GetNativeArray(senderTypeModifyHealth);
                        for (var i = 0; i < senders.Length; i++)
                        {
                            var requests = senders[i].RequestsToSend;
                            if (requests.Count > 0)
                            {
                                foreach (var request in requests)
                                {
                                    commandSystem.SendCommand(request, entities[i]);
                                }

                                requests.Clear();
                            }
                        }

                        var responders = chunk.GetNativeArray(responderTypeModifyHealth);
                        for (var i = 0; i < responders.Length; i++)
                        {
                            var responses = responders[i].ResponsesToSend;
                            if (responses.Count > 0)
                            {
                                foreach (var response in responses)
                                {
                                    commandSystem.SendResponse(response);
                                }

                                responses.Clear();
                            }
                        }
                    }

                    if (chunk.Has(senderTypeRequestRespawn))
                    {
                        var senders = chunk.GetNativeArray(senderTypeRequestRespawn);
                        for (var i = 0; i < senders.Length; i++)
                        {
                            var requests = senders[i].RequestsToSend;
                            if (requests.Count > 0)
                            {
                                foreach (var request in requests)
                                {
                                    commandSystem.SendCommand(request, entities[i]);
                                }

                                requests.Clear();
                            }
                        }

                        var responders = chunk.GetNativeArray(responderTypeRequestRespawn);
                        for (var i = 0; i < responders.Length; i++)
                        {
                            var responses = responders[i].ResponsesToSend;
                            if (responses.Count > 0)
                            {
                                foreach (var response in responses)
                                {
                                    commandSystem.SendResponse(response);
                                }

                                responses.Clear();
                            }
                        }
                    }

                }

                Profiler.EndSample();
            }
        }

        internal class ComponentCleanup : ComponentCleanupHandler
        {
            public override EntityArchetypeQuery CleanupArchetypeQuery => new EntityArchetypeQuery
            {
                All = Array.Empty<ComponentType>(),
                Any = new ComponentType[]
                {
                    ComponentType.Create<ComponentAdded<Improbable.Gdk.Health.HealthComponent.Component>>(),
                    ComponentType.Create<ComponentRemoved<Improbable.Gdk.Health.HealthComponent.Component>>(),
                    ComponentType.Create<Improbable.Gdk.Health.HealthComponent.ReceivedUpdates>(),
                    ComponentType.Create<AuthorityChanges<Improbable.Gdk.Health.HealthComponent.Component>>(),
                    ComponentType.Create<ReceivedEvents.HealthModified>(),
                    ComponentType.Create<ReceivedEvents.Respawn>(),
                    ComponentType.Create<CommandRequests.ModifyHealth>(),
                    ComponentType.Create<CommandResponses.ModifyHealth>(),
                    ComponentType.Create<CommandRequests.RequestRespawn>(),
                    ComponentType.Create<CommandResponses.RequestRespawn>(),
                },
                None = Array.Empty<ComponentType>(),
            };

            public override void CleanComponents(NativeArray<ArchetypeChunk> chunkArray, ComponentSystemBase system,
                EntityCommandBuffer buffer)
            {
                var entityType = system.GetArchetypeChunkEntityType();
                var componentAddedType = system.GetArchetypeChunkComponentType<ComponentAdded<Improbable.Gdk.Health.HealthComponent.Component>>();
                var componentRemovedType = system.GetArchetypeChunkComponentType<ComponentRemoved<Improbable.Gdk.Health.HealthComponent.Component>>();
                var receivedUpdateType = system.GetArchetypeChunkComponentType<Improbable.Gdk.Health.HealthComponent.ReceivedUpdates>();
                var authorityChangeType = system.GetArchetypeChunkComponentType<AuthorityChanges<Improbable.Gdk.Health.HealthComponent.Component>>();
                var healthModifiedEventType = system.GetArchetypeChunkComponentType<ReceivedEvents.HealthModified>();
                var respawnEventType = system.GetArchetypeChunkComponentType<ReceivedEvents.Respawn>();

                var modifyHealthRequestType = system.GetArchetypeChunkComponentType<CommandRequests.ModifyHealth>();
                var modifyHealthResponseType = system.GetArchetypeChunkComponentType<CommandResponses.ModifyHealth>();

                var requestRespawnRequestType = system.GetArchetypeChunkComponentType<CommandRequests.RequestRespawn>();
                var requestRespawnResponseType = system.GetArchetypeChunkComponentType<CommandResponses.RequestRespawn>();

                foreach (var chunk in chunkArray)
                {
                    var entities = chunk.GetNativeArray(entityType);

                    // Updates
                    if (chunk.Has(receivedUpdateType))
                    {
                        var updateArray = chunk.GetNativeArray(receivedUpdateType);
                        for (int i = 0; i < entities.Length; ++i)
                        {
                            buffer.RemoveComponent<Improbable.Gdk.Health.HealthComponent.ReceivedUpdates>(entities[i]);
                            var updateList = updateArray[i].Updates;

                            // Pool update lists to avoid excessive allocation
                            updateList.Clear();
                            Improbable.Gdk.Health.HealthComponent.Update.Pool.Push(updateList);

                            ReferenceTypeProviders.UpdatesProvider.Free(updateArray[i].handle);
                        }
                    }

                    // Component Added
                    if (chunk.Has(componentAddedType))
                    {
                        for (int i = 0; i < entities.Length; ++i)
                        {
                            buffer.RemoveComponent<ComponentAdded<Improbable.Gdk.Health.HealthComponent.Component>>(entities[i]);
                        }
                    }

                    // Component Removed
                    if (chunk.Has(componentRemovedType))
                    {
                        for (int i = 0; i < entities.Length; ++i)
                        {
                            buffer.RemoveComponent<ComponentRemoved<Improbable.Gdk.Health.HealthComponent.Component>>(entities[i]);
                        }
                    }

                    // Authority
                    if (chunk.Has(authorityChangeType))
                    {
                        var authorityChangeArray = chunk.GetNativeArray(authorityChangeType);
                        for (int i = 0; i < entities.Length; ++i)
                        {
                            buffer.RemoveComponent<AuthorityChanges<Improbable.Gdk.Health.HealthComponent.Component>>(entities[i]);
                            AuthorityChangesProvider.Free(authorityChangeArray[i].Handle);
                        }
                    }

                    // HealthModified Event
                    if (chunk.Has(healthModifiedEventType))
                    {
                        var healthModifiedEventArray = chunk.GetNativeArray(healthModifiedEventType);
                        for (int i = 0; i < entities.Length; ++i)
                        {
                            buffer.RemoveComponent<ReceivedEvents.HealthModified>(entities[i]);
                            ReferenceTypeProviders.HealthModifiedProvider.Free(healthModifiedEventArray[i].handle);
                        }
                    }

                    // Respawn Event
                    if (chunk.Has(respawnEventType))
                    {
                        var respawnEventArray = chunk.GetNativeArray(respawnEventType);
                        for (int i = 0; i < entities.Length; ++i)
                        {
                            buffer.RemoveComponent<ReceivedEvents.Respawn>(entities[i]);
                            ReferenceTypeProviders.RespawnProvider.Free(respawnEventArray[i].handle);
                        }
                    }

                    // ModifyHealth Command
                    if (chunk.Has(modifyHealthRequestType))
                    {
                        var modifyHealthRequestArray = chunk.GetNativeArray(modifyHealthRequestType);
                        for (int i = 0; i < entities.Length; ++i)
                        {
                            buffer.RemoveComponent<CommandRequests.ModifyHealth>(entities[i]);
                            ReferenceTypeProviders.ModifyHealthRequestsProvider.Free(modifyHealthRequestArray[i].CommandListHandle);
                        }
                    }

                    if (chunk.Has(modifyHealthResponseType))
                    {
                        var modifyHealthResponseArray = chunk.GetNativeArray(modifyHealthResponseType);
                        for (int i = 0; i < entities.Length; ++i)
                        {
                            buffer.RemoveComponent<CommandResponses.ModifyHealth>(entities[i]);
                            ReferenceTypeProviders.ModifyHealthResponsesProvider.Free(modifyHealthResponseArray[i].CommandListHandle);
                        }
                    }
                    // RequestRespawn Command
                    if (chunk.Has(requestRespawnRequestType))
                    {
                        var requestRespawnRequestArray = chunk.GetNativeArray(requestRespawnRequestType);
                        for (int i = 0; i < entities.Length; ++i)
                        {
                            buffer.RemoveComponent<CommandRequests.RequestRespawn>(entities[i]);
                            ReferenceTypeProviders.RequestRespawnRequestsProvider.Free(requestRespawnRequestArray[i].CommandListHandle);
                        }
                    }

                    if (chunk.Has(requestRespawnResponseType))
                    {
                        var requestRespawnResponseArray = chunk.GetNativeArray(requestRespawnResponseType);
                        for (int i = 0; i < entities.Length; ++i)
                        {
                            buffer.RemoveComponent<CommandResponses.RequestRespawn>(entities[i]);
                            ReferenceTypeProviders.RequestRespawnResponsesProvider.Free(requestRespawnResponseArray[i].CommandListHandle);
                        }
                    }
                }
            }
        }

        internal class AcknowledgeAuthorityLossHandler : AbstractAcknowledgeAuthorityLossHandler
       {
            public override EntityArchetypeQuery Query => new EntityArchetypeQuery
            {
                All = new ComponentType[]
                {
                    ComponentType.ReadOnly<AuthorityLossImminent<Improbable.Gdk.Health.HealthComponent.Component>>(),
                    ComponentType.ReadOnly<SpatialEntityId>()
                },
                Any = Array.Empty<ComponentType>(),
                None = Array.Empty<ComponentType>()
            };

            public override void AcknowledgeAuthorityLoss(NativeArray<ArchetypeChunk> chunkArray, ComponentSystemBase system,
                ComponentUpdateSystem updateSystem)
            {
                var authorityLossType = system.GetArchetypeChunkComponentType<AuthorityLossImminent<Improbable.Gdk.Health.HealthComponent.Component>>();
                var spatialEntityType = system.GetArchetypeChunkComponentType<SpatialEntityId>();

                foreach (var chunk in chunkArray)
                {
                    var authorityArray = chunk.GetNativeArray(authorityLossType);
                    var spatialEntityIdArray = chunk.GetNativeArray(spatialEntityType);

                    for (int i = 0; i < authorityArray.Length; ++i)
                    {
                        if (authorityArray[i].AcknowledgeAuthorityLoss)
                        {
                            updateSystem.AcknowledgeAuthorityLoss(spatialEntityIdArray[i].EntityId,
                                2040);
                        }
                    }
                }
            }
        }
    }
}
