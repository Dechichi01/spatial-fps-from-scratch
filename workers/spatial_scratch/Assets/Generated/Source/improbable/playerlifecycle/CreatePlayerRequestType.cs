// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System.Linq;
using Improbable.Gdk.Core;
using UnityEngine;

namespace Improbable.PlayerLifecycle
{
    
[System.Serializable]
public struct CreatePlayerRequestType
{
    public byte[] SerializedArguments;

    public CreatePlayerRequestType(byte[] serializedArguments)
    {
        SerializedArguments = serializedArguments;
    }
    public static class Serialization
    {
        public static void Serialize(CreatePlayerRequestType instance, global::Improbable.Worker.CInterop.SchemaObject obj)
        {
            {
                obj.AddBytes(1, instance.SerializedArguments);
            }
        }

        public static CreatePlayerRequestType Deserialize(global::Improbable.Worker.CInterop.SchemaObject obj)
        {
            var instance = new CreatePlayerRequestType();
            {
                instance.SerializedArguments = obj.GetBytes(1);
            }
            return instance;
        }
    }
}

}
