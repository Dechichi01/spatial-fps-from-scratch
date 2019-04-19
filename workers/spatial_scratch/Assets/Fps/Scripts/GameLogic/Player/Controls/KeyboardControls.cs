using UnityEngine;

namespace Fps.Gameplay.Client
{
    public class KeyboardControls : MonoBehaviour, IControlProvider
    {
        private const int ForwardIndex = 1;
        private const int BackwardIndex = 2;
        private const int RightIndex = 4;
        private const int LeftIndex = 8;

        public Vector3 MovementDirection
        {
            get
            {
                var index = Forward && !Backward ? ForwardIndex : 0;
                index += Backward && !Forward ? BackwardIndex : 0;
                index += Right && !Left ? RightIndex : 0;
                index += Left && !Right ? LeftIndex : 0;

                return cachedDirectionVectors[index];
            }
        }
        public float YawDelta => Input.GetAxis("Mouse X");
        public float PitchDelta => Input.GetAxis("Mouse Y");
        public bool IsSprinting => Input.GetKey(KeyCode.LeftShift) && Forward && !Backward;

        private static bool Forward => Input.GetKey(KeyCode.W);
        private static bool Backward => Input.GetKey(KeyCode.S);
        private static bool Left => Input.GetKey(KeyCode.A);
        private static bool Right => Input.GetKey(KeyCode.D);

        private readonly Vector3[] cachedDirectionVectors = new Vector3[16];

        private void CreateCachedDirections()
        {
            for (int i = 0; i < cachedDirectionVectors.Length; i++)
            {
                cachedDirectionVectors[i] = Vector3.zero;
            }

            cachedDirectionVectors[ForwardIndex] = Vector3.forward;
            cachedDirectionVectors[BackwardIndex] = Vector3.back;
            cachedDirectionVectors[RightIndex] = Vector3.right;
            cachedDirectionVectors[LeftIndex] = Vector3.left;

            cachedDirectionVectors[ForwardIndex + RightIndex] =
                (Vector3.forward + Vector3.right).normalized;

            cachedDirectionVectors[ForwardIndex + LeftIndex] =
                (Vector3.forward + Vector3.left).normalized;

            cachedDirectionVectors[BackwardIndex + RightIndex] =
                (Vector3.back + Vector3.right).normalized;

            cachedDirectionVectors[BackwardIndex + LeftIndex] =
                (Vector3.back + Vector3.left).normalized;
        }
    }

}
