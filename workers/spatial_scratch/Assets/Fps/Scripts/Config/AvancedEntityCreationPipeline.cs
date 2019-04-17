using System;
using System.Collections.Generic;
using Improbable.Gdk.Core;
using Improbable.Gdk.GameObjectCreation;
using Improbable.Gdk.Subscriptions;
using Improbable;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Fps.Common
{
    public class AdvancedEntityCreationPipeline : IEntityGameObjectCreator
    {
        private Worker worker;
        private string workerIdAttribute;
        IEntityGameObjectCreator fallback;
        private GameObject cachedAuthPlayerPrefab;
        private GameObject cachedNonAuthPlayerPrefab;
        private string playerEntityType;

        private readonly Dictionary<EntityId, GameObject> gameObjectsCreated = new Dictionary<EntityId, GameObject>();

        private readonly Type[] componentsToAdd =
        {
            typeof(Transform),
            typeof(Rigidbody)
        };

        public AdvancedEntityCreationPipeline(
            Worker worker,
            string playerEntityType, string authPlayerPrefabPath, string nonAuthPlayerPrefabPath, 
            IEntityGameObjectCreator fallback)
        {
            this.worker = worker;
            this.fallback = fallback;
            this.playerEntityType = playerEntityType;
            workerIdAttribute = string.Format("workerId:{0}", worker.WorkerId);

            cachedAuthPlayerPrefab = Resources.Load<GameObject>(authPlayerPrefabPath);
            cachedNonAuthPlayerPrefab = Resources.Load<GameObject>(nonAuthPlayerPrefabPath);

            if (!cachedAuthPlayerPrefab || !cachedNonAuthPlayerPrefab)
            {
                throw new Exception(string.Format("Couldn't find player prefabs on some of the paths: {0}, {1}", 
                    authPlayerPrefabPath, nonAuthPlayerPrefabPath));
            }
        }

        public void OnEntityCreated(SpatialOSEntity entity, EntityGameObjectLinker linker)
        {
            if (!entity.HasComponent<Metadata.Component>())
            {
                return;
            }

            var isPlayer = entity.GetComponent<Metadata.Component>().EntityType == playerEntityType;
            if (isPlayer)
            {
                var go = NewGameObjectFromPrefab(cachedNonAuthPlayerPrefab, entity, worker);
                linker.LinkGameObjectToSpatialOSEntity(entity.SpatialOSEntityId, go, componentsToAdd);
            }
            else
            {
                fallback.OnEntityCreated(entity, linker);
            }
        }

        public void OnEntityRemoved(EntityId entityId)
        {
            if (!gameObjectsCreated.TryGetValue(entityId, out var go))
            {
                fallback.OnEntityRemoved(entityId);
                return;
            }

            gameObjectsCreated.Remove(entityId);
            Object.Destroy(go);
        }

        private GameObject NewGameObjectFromPrefab(GameObject prefab, SpatialOSEntity entity, Worker worker,
            Vector3 pos = default(Vector3), Quaternion rot = default(Quaternion))
        {
            var go = Object.Instantiate(prefab, pos, rot);
            go.name = GetGameObjectName(prefab, entity, worker);
            gameObjectsCreated.Add(entity.SpatialOSEntityId, go);
            return go;
        }

        private static string GetGameObjectName(GameObject prefab, SpatialOSEntity entity, Worker worker)
        {
            return string.Format("{0}(SpatialOS {1}, Worker: {2})", prefab.name, entity.SpatialOSEntityId, worker.WorkerType);
        }
    }
}
