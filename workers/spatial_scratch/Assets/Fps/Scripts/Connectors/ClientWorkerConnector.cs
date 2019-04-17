using UnityEngine;
using Improbable.Gdk.GameObjectCreation;
using Improbable.Gdk.PlayerLifecycle;

namespace Fps.Common
{
    public class ClientWorkerConnector : WorkerConnectorBase
    {
        protected override string GetWorkerType()
        {
            return WorkerUtils.UnityClient;
        }

        protected override void HandleWorkerConnectionEstablished()
        {
            Debug.Log("Client connected successfully");
            var world = Worker.World;

            PlayerLifecycleHelper.AddClientSystems(world);

            GameObjectCreationHelper.EnableStandardGameObjectCreation(Worker.World);

            base.HandleWorkerConnectionEstablished();
        }

        protected override void HandleWorkerConnectionFailure(string errorMessage)
        {
            Debug.LogError("Client: Failed to connect");
            base.HandleWorkerConnectionFailure(errorMessage);
        }
    }
}
