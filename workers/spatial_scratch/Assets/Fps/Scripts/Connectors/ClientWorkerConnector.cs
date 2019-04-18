using UnityEngine;
using Improbable.Gdk.GameObjectCreation;
using Improbable.Gdk.PlayerLifecycle;

namespace Fps.Common
{
    public class ClientWorkerConnector : WorkerConnectorBase
    {
        private const string AuthPlayer = "Prefabs/UnityClient/Authoritative/Player";
        private const string NonAuthPlayer = "Prefabs/UnityClient/NonAuthoritative/Player";

        protected override string GetWorkerType()
        {
            return WorkerUtils.UnityClient;
        }

        protected override void HandleWorkerConnectionEstablished()
        {
            var world = Worker.World;

            ConfigurePlayerLifeCycle(world);

            base.HandleWorkerConnectionEstablished();
        }

        protected override void HandleWorkerConnectionFailure(string errorMessage)
        {
            Debug.LogError("Client: Failed to connect");
            base.HandleWorkerConnectionFailure(errorMessage);
        }

        private void ConfigurePlayerLifeCycle(Unity.Entities.World world)
        {
            PlayerLifecycleHelper.AddClientSystems(world);
            world.GetOrCreateManager<HandlePlayerHeartbeatRequestSystem>();

            var fallback = new GameObjectCreatorFromMetadata(Worker.WorkerId, Worker.Origin, Worker.LogDispatcher);

            GameObjectCreationHelper.EnableStandardGameObjectCreation(
                world,
                new AdvancedEntityCreationPipeline(
                    Worker, EntityUtils.PlayerEntityType, AuthPlayer, NonAuthPlayer, fallback),
                gameObject);
        }
    }
}
