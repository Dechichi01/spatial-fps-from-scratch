using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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

            Debug.Log("Server connected successfully");
            base.HandleWorkerConnectionEstablished();
        }
    }
}
