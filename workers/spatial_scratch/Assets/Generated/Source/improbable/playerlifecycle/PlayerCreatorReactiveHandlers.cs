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

namespace Improbable.PlayerLifecycle
{
    public partial class PlayerCreator
    {
        internal class ReactiveComponentReplicator : IReactiveComponentReplicationHandler
        {
            public uint ComponentId => 13000;

            public EntityArchetypeQuery EventQuery => new EntityArchetypeQuery
            {
                All = new[]
                {
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
                        ComponentType.Create<Improbable.PlayerLifecycle.PlayerCreator.CommandSenders.CreatePlayer>(),
                        ComponentType.Create<Improbable.PlayerLifecycle.PlayerCreator.CommandResponders.CreatePlayer>(),
                    },
                    Any = Array.Empty<ComponentType>(),
                    None = Array.Empty<ComponentType>(),
                },
            };

            public void SendEvents(NativeArray<ArchetypeChunk> chunkArray, ComponentSystemBase system, ComponentUpdateSystem componentUpdateSystem)
            {
            }

            public void SendCommands(NativeArray<ArchetypeChunk> chunkArray, ComponentSystemBase system, CommandSystem commandSystem)
            {
                Profiler.BeginSample("PlayerCreator");
                var entityType = system.GetArchetypeChunkEntityType();
                var senderTypeCreatePlayer = system.GetArchetypeChunkComponentType<Improbable.PlayerLifecycle.PlayerCreator.CommandSenders.CreatePlayer>(true);
                var responderTypeCreatePlayer = system.GetArchetypeChunkComponentType<Improbable.PlayerLifecycle.PlayerCreator.CommandResponders.CreatePlayer>(true);

                foreach (var chunk in chunkArray)
                {
                    var entities = chunk.GetNativeArray(entityType);
                    if (chunk.Has(senderTypeCreatePlayer))
                    {
                        var senders = chunk.GetNativeArray(senderTypeCreatePlayer);
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

                        var responders = chunk.GetNativeArray(responderTypeCreatePlayer);
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
                    ComponentType.Create<ComponentAdded<Improbable.PlayerLifecycle.PlayerCreator.Component>>(),
                    ComponentType.Create<ComponentRemoved<Improbable.PlayerLifecycle.PlayerCreator.Component>>(),
                    ComponentType.Create<Improbable.PlayerLifecycle.PlayerCreator.ReceivedUpdates>(),
                    ComponentType.Create<AuthorityChanges<Improbable.PlayerLifecycle.PlayerCreator.Component>>(),
                    ComponentType.Create<CommandRequests.CreatePlayer>(),
                    ComponentType.Create<CommandResponses.CreatePlayer>(),
                },
                None = Array.Empty<ComponentType>(),
            };

            public override void CleanComponents(NativeArray<ArchetypeChunk> chunkArray, ComponentSystemBase system,
                EntityCommandBuffer buffer)
            {
                var entityType = system.GetArchetypeChunkEntityType();
                var componentAddedType = system.GetArchetypeChunkComponentType<ComponentAdded<Improbable.PlayerLifecycle.PlayerCreator.Component>>();
                var componentRemovedType = system.GetArchetypeChunkComponentType<ComponentRemoved<Improbable.PlayerLifecycle.PlayerCreator.Component>>();
                var receivedUpdateType = system.GetArchetypeChunkComponentType<Improbable.PlayerLifecycle.PlayerCreator.ReceivedUpdates>();
                var authorityChangeType = system.GetArchetypeChunkComponentType<AuthorityChanges<Improbable.PlayerLifecycle.PlayerCreator.Component>>();

                var createPlayerRequestType = system.GetArchetypeChunkComponentType<CommandRequests.CreatePlayer>();
                var createPlayerResponseType = system.GetArchetypeChunkComponentType<CommandResponses.CreatePlayer>();

                foreach (var chunk in chunkArray)
                {
                    var entities = chunk.GetNativeArray(entityType);

                    // Updates
                    if (chunk.Has(receivedUpdateType))
                    {
                        var updateArray = chunk.GetNativeArray(receivedUpdateType);
                        for (int i = 0; i < entities.Length; ++i)
                        {
                            buffer.RemoveComponent<Improbable.PlayerLifecycle.PlayerCreator.ReceivedUpdates>(entities[i]);
                            var updateList = updateArray[i].Updates;

                            // Pool update lists to avoid excessive allocation
                            updateList.Clear();
                            Improbable.PlayerLifecycle.PlayerCreator.Update.Pool.Push(updateList);

                            ReferenceTypeProviders.UpdatesProvider.Free(updateArray[i].handle);
                        }
                    }

                    // Component Added
                    if (chunk.Has(componentAddedType))
                    {
                        for (int i = 0; i < entities.Length; ++i)
                        {
                            buffer.RemoveComponent<ComponentAdded<Improbable.PlayerLifecycle.PlayerCreator.Component>>(entities[i]);
                        }
                    }

                    // Component Removed
                    if (chunk.Has(componentRemovedType))
                    {
                        for (int i = 0; i < entities.Length; ++i)
                        {
                            buffer.RemoveComponent<ComponentRemoved<Improbable.PlayerLifecycle.PlayerCreator.Component>>(entities[i]);
                        }
                    }

                    // Authority
                    if (chunk.Has(authorityChangeType))
                    {
                        var authorityChangeArray = chunk.GetNativeArray(authorityChangeType);
                        for (int i = 0; i < entities.Length; ++i)
                        {
                            buffer.RemoveComponent<AuthorityChanges<Improbable.PlayerLifecycle.PlayerCreator.Component>>(entities[i]);
                            AuthorityChangesProvider.Free(authorityChangeArray[i].Handle);
                        }
                    }

                    // CreatePlayer Command
                    if (chunk.Has(createPlayerRequestType))
                    {
                        var createPlayerRequestArray = chunk.GetNativeArray(createPlayerRequestType);
                        for (int i = 0; i < entities.Length; ++i)
                        {
                            buffer.RemoveComponent<CommandRequests.CreatePlayer>(entities[i]);
                            ReferenceTypeProviders.CreatePlayerRequestsProvider.Free(createPlayerRequestArray[i].CommandListHandle);
                        }
                    }

                    if (chunk.Has(createPlayerResponseType))
                    {
                        var createPlayerResponseArray = chunk.GetNativeArray(createPlayerResponseType);
                        for (int i = 0; i < entities.Length; ++i)
                        {
                            buffer.RemoveComponent<CommandResponses.CreatePlayer>(entities[i]);
                            ReferenceTypeProviders.CreatePlayerResponsesProvider.Free(createPlayerResponseArray[i].CommandListHandle);
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
                    ComponentType.ReadOnly<AuthorityLossImminent<Improbable.PlayerLifecycle.PlayerCreator.Component>>(),
                    ComponentType.ReadOnly<SpatialEntityId>()
                },
                Any = Array.Empty<ComponentType>(),
                None = Array.Empty<ComponentType>()
            };

            public override void AcknowledgeAuthorityLoss(NativeArray<ArchetypeChunk> chunkArray, ComponentSystemBase system,
                ComponentUpdateSystem updateSystem)
            {
                var authorityLossType = system.GetArchetypeChunkComponentType<AuthorityLossImminent<Improbable.PlayerLifecycle.PlayerCreator.Component>>();
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
                                13000);
                        }
                    }
                }
            }
        }
    }
}
