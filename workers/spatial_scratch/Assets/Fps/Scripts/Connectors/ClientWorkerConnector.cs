using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
            base.HandleWorkerConnectionEstablished();
        }

        protected override void HandleWorkerConnectionFailure(string errorMessage)
        {
            Debug.LogError("Client: Failed to connect");
            base.HandleWorkerConnectionFailure(errorMessage);
        }
    }
}
