using UnityEngine;
using Improbable.Gdk.GameObjectCreation;
using Improbable.Gdk.PlayerLifecycle;

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

            PlayerLifecycleHelper.AddServerSystems(world);

            GameObjectCreationHelper.EnableStandardGameObjectCreation(Worker.World);

            Debug.Log("Server connected successfully");
            base.HandleWorkerConnectionEstablished();
        }
    }
}
