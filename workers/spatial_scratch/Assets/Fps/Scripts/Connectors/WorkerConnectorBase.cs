using UnityEngine;
using Improbable.Gdk.Core;
using System.Threading.Tasks;
using Improbable.Worker.CInterop;

namespace Fps.Common
{
    public abstract class WorkerConnectorBase : DefaultWorkerConnector
    {
        public int TargetFrameRate = 60;

        private async void Start()
        {
            Application.targetFrameRate = 60;
            await AttemptConnection();
        }

        private async Task AttemptConnection()
        {
            await Connect(GetWorkerType(), new ForwardingDispatcher()).ConfigureAwait(false);
        }

        protected override string SelectDeploymentName(DeploymentList deployments)
        {
            return deployments.Deployments[0].DeploymentName;
        }

        protected abstract string GetWorkerType();

        protected override void HandleWorkerConnectionEstablished()
        {
            //TODO: Load level
        }

        public override void Dispose()
        {
            //TODO: Destroy level
            base.Dispose();
        }
    }
}