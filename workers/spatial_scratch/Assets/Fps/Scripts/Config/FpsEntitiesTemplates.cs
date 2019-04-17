using UnityEngine;
using Improbable.Gdk.Core;
using Improbable;
using Improbable.Gdk.PlayerLifecycle;
using Improbable.PlayerLifecycle;

namespace Fps.Common
{
    public static class FpsEntitiesTemplates
    {
        public static EntityTemplate PlayerSpawner()
        {
            var template = CreateDefaultEntity("PlayerSpawner");

            template.AddComponent(new Persistence.Snapshot(), WorkerUtils.UnityGameLogic);
            template.AddComponent(new PlayerCreator.Snapshot(), WorkerUtils.UnityGameLogic);

            template.SetReadAccess(WorkerUtils.UnityGameLogic);

            return template;
        }

        public static EntityTemplate Player(string workerType, byte[] args)
        {
            var client = $"workerId:{workerType}";
            var template = new EntityTemplate();

            var metadata = new Metadata.Snapshot() { EntityType = EntityUtils.PlayerEntityType };
            var pos = new Position.Snapshot() { Coords = new Coordinates(0,0,0)};

            template.AddComponent(metadata, WorkerUtils.UnityGameLogic);
            template.AddComponent(pos, WorkerUtils.UnityGameLogic);
            PlayerLifecycleHelper.AddPlayerLifecycleComponents(template, workerType, client, WorkerUtils.UnityGameLogic);

            template.SetReadAccess(WorkerUtils.UnityClient, WorkerUtils.UnityGameLogic);
            template.SetComponentWriteAccess(EntityAcl.ComponentId, WorkerUtils.UnityGameLogic);

            Debug.Log("REturning my entity!!");
            return template;
        }

        private static EntityTemplate CreateDefaultEntity(string entityType)
        {
            var metadata = new Metadata.Snapshot() { EntityType = entityType };
            var pos = new Position.Snapshot();

            var template = new EntityTemplate();
            template.AddComponent(metadata, WorkerUtils.UnityGameLogic);
            template.AddComponent(pos, WorkerUtils.UnityGameLogic);
            template.SetComponentWriteAccess(EntityAcl.ComponentId, WorkerUtils.UnityGameLogic);

            return template;
        }
    }
}
