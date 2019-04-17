using UnityEngine;
using Improbable.Gdk.Core;
using Improbable;
using Improbable.Gdk.PlayerLifecycle;
using Improbable.PlayerLifecycle;
using Improbable.Gdk.StandardTypes;

namespace Fps.Common
{
    public static class FpsEntitiesTemplates
    {
        public static EntityTemplate PlayerSpawner()
        {
            var template = CreateDefaultEntity("PlayerCreator");

            template.AddComponent(new Persistence.Snapshot(), WorkerUtils.UnityGameLogic);
            template.AddComponent(new PlayerCreator.Snapshot(), WorkerUtils.UnityGameLogic);

            template.SetReadAccess(WorkerUtils.UnityGameLogic);

            return template;
        }

        public static EntityTemplate Player(string workerType, byte[] args)
        {
            //TODO: Add all gameplay components
            var client = $"workerId:{workerType}";

            var template = CreateDefaultEntity(EntityUtils.PlayerEntityType);

            PlayerLifecycleHelper.AddPlayerLifecycleComponents(template, workerType, client, WorkerUtils.UnityGameLogic);

            template.SetReadAccess(WorkerUtils.UnityClient, WorkerUtils.UnityGameLogic);

            return template;
        }

        private static EntityTemplate CreateDefaultEntity(string entityType, Vector3 initialPos = default(Vector3))
        {
            var metadata = new Metadata.Snapshot() { EntityType = entityType };
            var pos = new Position.Snapshot(initialPos.ToSpatialCoordinates());

            var template = new EntityTemplate();
            template.AddComponent(metadata, WorkerUtils.UnityGameLogic);
            template.AddComponent(pos, WorkerUtils.UnityGameLogic);
            template.SetComponentWriteAccess(EntityAcl.ComponentId, WorkerUtils.UnityGameLogic);

            return template;
        }
    }
}
