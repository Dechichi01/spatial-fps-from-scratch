using UnityEngine;
using Improbable.Gdk.GameObjectCreation;
using Improbable.Gdk.PlayerLifecycle;
using Improbable.Gdk.Health;

namespace Fps.Common
{
    public class GameLogicWorkerConnector : WorkerConnectorBase
    {
        protected override string GetWorkerType()
        {
            return WorkerUtils.UnityGameLogic;
        }

        protected override void HandleWorkerConnectionEstablished()
        {
            //TODO: Setup gameplay systems
            var world = Worker.World;

            ConfigurePlayerLifeCycle(world);
            ConfigureFpsSystems(world);

            Debug.Log("Server connected successfully");
            base.HandleWorkerConnectionEstablished();
        }

        private void ConfigurePlayerLifeCycle(Unity.Entities.World world)
        {
            PlayerLifecycleHelper.AddServerSystems(world);
            GameObjectCreationHelper.EnableStandardGameObjectCreation(Worker.World);
        }

        private void ConfigureFpsSystems(Unity.Entities.World world)
        {
            world.GetOrCreateManager<ServerHealthModifierSystem>();
            world.GetOrCreateManager<HealthRegenSystem>();
        }
    }
}
