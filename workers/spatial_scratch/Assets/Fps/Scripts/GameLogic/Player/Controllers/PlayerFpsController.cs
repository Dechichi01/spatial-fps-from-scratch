using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fps.Gameplay.Client
{
    [RequireComponent(typeof(ClientMovementController))]
    public class PlayerFpsController : MonoBehaviour
    {
        ClientMovementController movementController;
        IControlProvider controlProvider;

        private void Awake()
        {
            movementController = GetComponent<ClientMovementController>();
            controlProvider = GetComponent<IControlProvider>();
        }

        private void Update()
        {
            movementController.ProcessMovementCommand(new ClientMovementController.MovementInput(
                controlProvider.MovementDirection,
                controlProvider.YawDelta, controlProvider.PitchDelta,
                controlProvider.IsSprinting));
        }
    }
}

