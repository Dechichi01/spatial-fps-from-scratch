using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fps.Common
{
    public static class PlayerHealthConfig
    {
        public const float MaxHealth = 100.0f;

        public static float RegenAfterDamageCooldown = 5.0f;
        public static float RegenInterval = 0.2f;
        public static float RegenAmount = 2.0f;
        public static float SpatialCooldownSyncInterval = 0.5f;
    }
}
