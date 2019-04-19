using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fps.Gameplay.Client
{
    public interface IControlProvider
    {
        Vector3 MovementDirection { get; }
        float YawDelta { get; }
        float PitchDelta { get; }
        bool IsSprinting { get; }
    }
}