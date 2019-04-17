using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Improbable.Gdk.PlayerLifecycle;

namespace Fps.Common
{
    public static class OneTimeInitialization
    {
        private static bool initialized;

        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            if (initialized) { return; }

            PlayerLifecycleConfig.CreatePlayerEntityTemplate = FpsEntitiesTemplates.Player;

            initialized = true;
        }
    }
}

