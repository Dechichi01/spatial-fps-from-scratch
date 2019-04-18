// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System.Linq;
using Improbable.Gdk.Core;
using UnityEngine;

namespace Improbable.Gdk.Health
{
    
[System.Serializable]
public struct HealthModifier
{
    public uint Type;
    public float Amount;
    public global::Improbable.Gdk.Standardtypes.IntAbsolute AppliedLocation;
    public global::Improbable.Gdk.Standardtypes.IntAbsolute Origin;

    public HealthModifier(uint type, float amount, global::Improbable.Gdk.Standardtypes.IntAbsolute appliedLocation, global::Improbable.Gdk.Standardtypes.IntAbsolute origin)
    {
        Type = type;
        Amount = amount;
        AppliedLocation = appliedLocation;
        Origin = origin;
    }
    public static class Serialization
    {
        public static void Serialize(HealthModifier instance, global::Improbable.Worker.CInterop.SchemaObject obj)
        {
            {
                obj.AddUint32(1, instance.Type);
            }
            {
                obj.AddFloat(2, instance.Amount);
            }
            {
                global::Improbable.Gdk.Standardtypes.IntAbsolute.Serialization.Serialize(instance.AppliedLocation, obj.AddObject(3));
            }
            {
                global::Improbable.Gdk.Standardtypes.IntAbsolute.Serialization.Serialize(instance.Origin, obj.AddObject(4));
            }
        }

        public static HealthModifier Deserialize(global::Improbable.Worker.CInterop.SchemaObject obj)
        {
            var instance = new HealthModifier();
            {
                instance.Type = obj.GetUint32(1);
            }
            {
                instance.Amount = obj.GetFloat(2);
            }
            {
                instance.AppliedLocation = global::Improbable.Gdk.Standardtypes.IntAbsolute.Serialization.Deserialize(obj.GetObject(3));
            }
            {
                instance.Origin = global::Improbable.Gdk.Standardtypes.IntAbsolute.Serialization.Deserialize(obj.GetObject(4));
            }
            return instance;
        }
    }
}

}
