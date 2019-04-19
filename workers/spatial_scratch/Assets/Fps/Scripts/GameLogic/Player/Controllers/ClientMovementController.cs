using UnityEngine;

namespace Fps.Gameplay.Client
{
    public class ClientMovementController : MonoBehaviour
    {
        [SerializeField] private Transform pitchTransform;
        [SerializeField] private CameraSettings cameraSettings = CameraSettings.Default;

        public void ProcessMovementCommand(MovementInput movInput)
        {
            var moveDir = transform.rotation * movInput.MovementDir;
            //todo: Movement speed
            //todo: jump
            var newRot = BuildRotationFromInput(movInput);
            pitchTransform.localRotation = Quaternion.Euler(newRot.eulerAngles.x, 0, 0);
        }

        private Quaternion BuildRotationFromInput(MovementInput input)
        {
            var pitchDelta = input.Pitch * cameraSettings.PitchSpeed;
            var newPitch = pitchTransform.rotation.eulerAngles.x + pitchDelta;
            var pitchAdjust = newPitch > 180 ? -360 : 0;
            newPitch = Mathf.Clamp(newPitch + pitchAdjust, -cameraSettings.MaxPitch, -cameraSettings.MinPitch);

            var yawDelta = input.Yaw * cameraSettings.YawSpeed;
            var newYaw = transform.rotation.eulerAngles.y + yawDelta;

            return Quaternion.Euler(newPitch, newYaw, 0);
        }

        public struct MovementInput
        {
            public readonly Vector3 MovementDir;
            public readonly float Yaw;
            public readonly float Pitch;
            public readonly bool IsSprinting;
                   
            public MovementInput(Vector3 movDir, float yaw, float pitch, bool isSprinting)
            {
                MovementDir = movDir;
                Yaw = yaw;
                Pitch = pitch;
                IsSprinting = isSprinting;
            }
        }

        [System.Serializable]
        private struct CameraSettings
        {
            public float PitchSpeed;
            public float YawSpeed;
            public float MinPitch;
            public float MaxPitch;

            public static CameraSettings Default = new CameraSettings
            {
                PitchSpeed = 1.0f,
                YawSpeed = 1.0f,
                MinPitch = -80.0f,
                MaxPitch = 60.0f
            };
        }
    }
}

