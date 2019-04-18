// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System.Linq;
using Improbable.Gdk.Core;
using UnityEngine;

namespace Improbable.Gdk.Health
{
    
[System.Serializable]
public struct HealthModifiedInfo
{
    public global::Improbable.Gdk.Health.HealthModifier Modifier;
    public float HealthBefore;
    public float HealthAfter;
    public BlittableBool Died;

    public HealthModifiedInfo(global::Improbable.Gdk.Health.HealthModifier modifier, float healthBefore, float healthAfter, BlittableBool died)
    {
        Modifier = modifier;
        HealthBefore = healthBefore;
        HealthAfter = healthAfter;
        Died = died;
    }
    public static class Serialization
    {
        public static void Serialize(HealthModifiedInfo instance, global::Improbable.Worker.CInterop.SchemaObject obj)
        {
            {
                global::Improbable.Gdk.Health.HealthModifier.Serialization.Serialize(instance.Modifier, obj.AddObject(1));
            }
            {
                obj.AddFloat(2, instance.HealthBefore);
            }
            {
                obj.AddFloat(3, instance.HealthAfter);
            }
            {
                obj.AddBool(4, instance.Died);
            }
        }

        public static HealthModifiedInfo Deserialize(global::Improbable.Worker.CInterop.SchemaObject obj)
        {
            var instance = new HealthModifiedInfo();
            {
                instance.Modifier = global::Improbable.Gdk.Health.HealthModifier.Serialization.Deserialize(obj.GetObject(1));
            }
            {
                instance.HealthBefore = obj.GetFloat(2);
            }
            {
                instance.HealthAfter = obj.GetFloat(3);
            }
            {
                instance.Died = obj.GetBool(4);
            }
            return instance;
        }
    }
}

}
