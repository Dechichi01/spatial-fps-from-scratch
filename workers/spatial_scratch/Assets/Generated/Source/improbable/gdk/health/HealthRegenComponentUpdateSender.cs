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
using Improbable.Gdk.Core;
using Improbable.Gdk.Core.CodegenAdapters;

namespace Improbable.Gdk.Health
{
    public partial class HealthRegenComponent
    {
        internal class ComponentReplicator : IComponentReplicationHandler
        {
            public uint ComponentId => 2041;

            public EntityArchetypeQuery ComponentUpdateQuery => new EntityArchetypeQuery
            {
                All = new[]
                {
                    ComponentType.Create<Improbable.Gdk.Health.HealthRegenComponent.Component>(),
                    ComponentType.Create<Improbable.Gdk.Health.HealthRegenComponent.ComponentAuthority>(),
                    ComponentType.ReadOnly<SpatialEntityId>()
                },
                Any = Array.Empty<ComponentType>(),
                None = Array.Empty<ComponentType>(),
            };

            public void SendUpdates(
                NativeArray<ArchetypeChunk> chunkArray,
                ComponentSystemBase system,
                EntityManager entityManager,
                ComponentUpdateSystem componentUpdateSystem)
            {
                Profiler.BeginSample("HealthRegenComponent");

                var spatialOSEntityType = system.GetArchetypeChunkComponentType<SpatialEntityId>(true);
                var componentType = system.GetArchetypeChunkComponentType<Improbable.Gdk.Health.HealthRegenComponent.Component>();

                var authorityType = system.GetArchetypeChunkSharedComponentType<ComponentAuthority>();

                foreach (var chunk in chunkArray)
                {
                    var entityIdArray = chunk.GetNativeArray(spatialOSEntityType);
                    var componentArray = chunk.GetNativeArray(componentType);

                    var authorityIndex = chunk.GetSharedComponentIndex(authorityType);

                    if (!entityManager.GetSharedComponentData<ComponentAuthority>(authorityIndex).HasAuthority)
                    {
                        continue;
                    }

                    for (var i = 0; i < componentArray.Length; i++)
                    {
                        var data = componentArray[i];
                        if (data.IsDataDirty())
                        {
                            Update update = new Update();

                            if (data.IsDataDirty(0))
                            {
                                update.DamagedRecently = data.DamagedRecently;
                            }

                            if (data.IsDataDirty(1))
                            {
                                update.RegenCooldownTimer = data.RegenCooldownTimer;
                            }

                            if (data.IsDataDirty(2))
                            {
                                update.CooldownSyncInterval = data.CooldownSyncInterval;
                            }

                            if (data.IsDataDirty(3))
                            {
                                update.RegenPauseTime = data.RegenPauseTime;
                            }

                            if (data.IsDataDirty(4))
                            {
                                update.RegenInterval = data.RegenInterval;
                            }

                            if (data.IsDataDirty(5))
                            {
                                update.RegenAmount = data.RegenAmount;
                            }

                            componentUpdateSystem.SendUpdate(in update, entityIdArray[i].EntityId);
                            data.MarkDataClean();
                            componentArray[i] = data;
                        }
                    }
                }

                Profiler.EndSample();
            }
        }
    }
}
