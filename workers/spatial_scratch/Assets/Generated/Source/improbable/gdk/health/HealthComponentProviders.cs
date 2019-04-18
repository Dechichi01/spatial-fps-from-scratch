// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System;
using System.Collections.Generic;
using System.Linq;
using Improbable.Gdk.Core;

namespace Improbable.Gdk.Health
{
    public partial class HealthComponent
    {
        internal static class ReferenceTypeProviders
        {
            public static class UpdatesProvider 
{
    private static readonly Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.Update>> Storage = new Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.Update>>();
    private static readonly Dictionary<uint, global::Unity.Entities.World> WorldMapping = new Dictionary<uint, Unity.Entities.World>();

    private static uint nextHandle = 0;

    public static uint Allocate(global::Unity.Entities.World world)
    {
        var handle = GetNextHandle();

        Storage.Add(handle, default(List<Improbable.Gdk.Health.HealthComponent.Update>));
        WorldMapping.Add(handle, world);

        return handle;
    }

    public static List<Improbable.Gdk.Health.HealthComponent.Update> Get(uint handle)
    {
        if (!Storage.TryGetValue(handle, out var value))
        {
            throw new ArgumentException($"UpdatesProvider does not contain handle {handle}");
        }

        return value;
    }

    public static void Set(uint handle, List<Improbable.Gdk.Health.HealthComponent.Update> value)
    {
        if (!Storage.ContainsKey(handle))
        {
            throw new ArgumentException($"UpdatesProvider does not contain handle {handle}");
        }

        Storage[handle] = value;
    }

    public static void Free(uint handle)
    {
        Storage.Remove(handle);
        WorldMapping.Remove(handle);
    }

    public static void CleanDataInWorld(global::Unity.Entities.World world)
    {
        var handles = WorldMapping.Where(pair => pair.Value == world).Select(pair => pair.Key).ToList();

        foreach (var handle in handles)
        {
            Free(handle);
        }
    }

    private static uint GetNextHandle() 
    {
        nextHandle++;
        
        while (Storage.ContainsKey(nextHandle))
        {
            nextHandle++;
        }

        return nextHandle;
    }
}


            public static class HealthModifiedProvider 
{
    private static readonly Dictionary<uint, List<global::Improbable.Gdk.Health.HealthModifiedInfo>> Storage = new Dictionary<uint, List<global::Improbable.Gdk.Health.HealthModifiedInfo>>();
    private static readonly Dictionary<uint, global::Unity.Entities.World> WorldMapping = new Dictionary<uint, Unity.Entities.World>();

    private static uint nextHandle = 0;

    public static uint Allocate(global::Unity.Entities.World world)
    {
        var handle = GetNextHandle();

        Storage.Add(handle, default(List<global::Improbable.Gdk.Health.HealthModifiedInfo>));
        WorldMapping.Add(handle, world);

        return handle;
    }

    public static List<global::Improbable.Gdk.Health.HealthModifiedInfo> Get(uint handle)
    {
        if (!Storage.TryGetValue(handle, out var value))
        {
            throw new ArgumentException($"HealthModifiedProvider does not contain handle {handle}");
        }

        return value;
    }

    public static void Set(uint handle, List<global::Improbable.Gdk.Health.HealthModifiedInfo> value)
    {
        if (!Storage.ContainsKey(handle))
        {
            throw new ArgumentException($"HealthModifiedProvider does not contain handle {handle}");
        }

        Storage[handle] = value;
    }

    public static void Free(uint handle)
    {
        Storage.Remove(handle);
        WorldMapping.Remove(handle);
    }

    public static void CleanDataInWorld(global::Unity.Entities.World world)
    {
        var handles = WorldMapping.Where(pair => pair.Value == world).Select(pair => pair.Key).ToList();

        foreach (var handle in handles)
        {
            Free(handle);
        }
    }

    private static uint GetNextHandle() 
    {
        nextHandle++;
        
        while (Storage.ContainsKey(nextHandle))
        {
            nextHandle++;
        }

        return nextHandle;
    }
}

            public static class RespawnProvider 
{
    private static readonly Dictionary<uint, List<global::Improbable.Common.Empty>> Storage = new Dictionary<uint, List<global::Improbable.Common.Empty>>();
    private static readonly Dictionary<uint, global::Unity.Entities.World> WorldMapping = new Dictionary<uint, Unity.Entities.World>();

    private static uint nextHandle = 0;

    public static uint Allocate(global::Unity.Entities.World world)
    {
        var handle = GetNextHandle();

        Storage.Add(handle, default(List<global::Improbable.Common.Empty>));
        WorldMapping.Add(handle, world);

        return handle;
    }

    public static List<global::Improbable.Common.Empty> Get(uint handle)
    {
        if (!Storage.TryGetValue(handle, out var value))
        {
            throw new ArgumentException($"RespawnProvider does not contain handle {handle}");
        }

        return value;
    }

    public static void Set(uint handle, List<global::Improbable.Common.Empty> value)
    {
        if (!Storage.ContainsKey(handle))
        {
            throw new ArgumentException($"RespawnProvider does not contain handle {handle}");
        }

        Storage[handle] = value;
    }

    public static void Free(uint handle)
    {
        Storage.Remove(handle);
        WorldMapping.Remove(handle);
    }

    public static void CleanDataInWorld(global::Unity.Entities.World world)
    {
        var handles = WorldMapping.Where(pair => pair.Value == world).Select(pair => pair.Key).ToList();

        foreach (var handle in handles)
        {
            Free(handle);
        }
    }

    private static uint GetNextHandle() 
    {
        nextHandle++;
        
        while (Storage.ContainsKey(nextHandle))
        {
            nextHandle++;
        }

        return nextHandle;
    }
}

            public static class ModifyHealthSenderProvider 
{
    private static readonly Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.Request>> Storage = new Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.Request>>();
    private static readonly Dictionary<uint, global::Unity.Entities.World> WorldMapping = new Dictionary<uint, Unity.Entities.World>();

    private static uint nextHandle = 0;

    public static uint Allocate(global::Unity.Entities.World world)
    {
        var handle = GetNextHandle();

        Storage.Add(handle, default(List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.Request>));
        WorldMapping.Add(handle, world);

        return handle;
    }

    public static List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.Request> Get(uint handle)
    {
        if (!Storage.TryGetValue(handle, out var value))
        {
            throw new ArgumentException($"ModifyHealthSenderProvider does not contain handle {handle}");
        }

        return value;
    }

    public static void Set(uint handle, List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.Request> value)
    {
        if (!Storage.ContainsKey(handle))
        {
            throw new ArgumentException($"ModifyHealthSenderProvider does not contain handle {handle}");
        }

        Storage[handle] = value;
    }

    public static void Free(uint handle)
    {
        Storage.Remove(handle);
        WorldMapping.Remove(handle);
    }

    public static void CleanDataInWorld(global::Unity.Entities.World world)
    {
        var handles = WorldMapping.Where(pair => pair.Value == world).Select(pair => pair.Key).ToList();

        foreach (var handle in handles)
        {
            Free(handle);
        }
    }

    private static uint GetNextHandle() 
    {
        nextHandle++;
        
        while (Storage.ContainsKey(nextHandle))
        {
            nextHandle++;
        }

        return nextHandle;
    }
}

            public static class ModifyHealthRequestsProvider 
{
    private static readonly Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedRequest>> Storage = new Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedRequest>>();
    private static readonly Dictionary<uint, global::Unity.Entities.World> WorldMapping = new Dictionary<uint, Unity.Entities.World>();

    private static uint nextHandle = 0;

    public static uint Allocate(global::Unity.Entities.World world)
    {
        var handle = GetNextHandle();

        Storage.Add(handle, default(List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedRequest>));
        WorldMapping.Add(handle, world);

        return handle;
    }

    public static List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedRequest> Get(uint handle)
    {
        if (!Storage.TryGetValue(handle, out var value))
        {
            throw new ArgumentException($"ModifyHealthRequestsProvider does not contain handle {handle}");
        }

        return value;
    }

    public static void Set(uint handle, List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedRequest> value)
    {
        if (!Storage.ContainsKey(handle))
        {
            throw new ArgumentException($"ModifyHealthRequestsProvider does not contain handle {handle}");
        }

        Storage[handle] = value;
    }

    public static void Free(uint handle)
    {
        Storage.Remove(handle);
        WorldMapping.Remove(handle);
    }

    public static void CleanDataInWorld(global::Unity.Entities.World world)
    {
        var handles = WorldMapping.Where(pair => pair.Value == world).Select(pair => pair.Key).ToList();

        foreach (var handle in handles)
        {
            Free(handle);
        }
    }

    private static uint GetNextHandle() 
    {
        nextHandle++;
        
        while (Storage.ContainsKey(nextHandle))
        {
            nextHandle++;
        }

        return nextHandle;
    }
}

            public static class ModifyHealthResponderProvider 
{
    private static readonly Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.Response>> Storage = new Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.Response>>();
    private static readonly Dictionary<uint, global::Unity.Entities.World> WorldMapping = new Dictionary<uint, Unity.Entities.World>();

    private static uint nextHandle = 0;

    public static uint Allocate(global::Unity.Entities.World world)
    {
        var handle = GetNextHandle();

        Storage.Add(handle, default(List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.Response>));
        WorldMapping.Add(handle, world);

        return handle;
    }

    public static List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.Response> Get(uint handle)
    {
        if (!Storage.TryGetValue(handle, out var value))
        {
            throw new ArgumentException($"ModifyHealthResponderProvider does not contain handle {handle}");
        }

        return value;
    }

    public static void Set(uint handle, List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.Response> value)
    {
        if (!Storage.ContainsKey(handle))
        {
            throw new ArgumentException($"ModifyHealthResponderProvider does not contain handle {handle}");
        }

        Storage[handle] = value;
    }

    public static void Free(uint handle)
    {
        Storage.Remove(handle);
        WorldMapping.Remove(handle);
    }

    public static void CleanDataInWorld(global::Unity.Entities.World world)
    {
        var handles = WorldMapping.Where(pair => pair.Value == world).Select(pair => pair.Key).ToList();

        foreach (var handle in handles)
        {
            Free(handle);
        }
    }

    private static uint GetNextHandle() 
    {
        nextHandle++;
        
        while (Storage.ContainsKey(nextHandle))
        {
            nextHandle++;
        }

        return nextHandle;
    }
}

            public static class ModifyHealthResponsesProvider 
{
    private static readonly Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedResponse>> Storage = new Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedResponse>>();
    private static readonly Dictionary<uint, global::Unity.Entities.World> WorldMapping = new Dictionary<uint, Unity.Entities.World>();

    private static uint nextHandle = 0;

    public static uint Allocate(global::Unity.Entities.World world)
    {
        var handle = GetNextHandle();

        Storage.Add(handle, default(List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedResponse>));
        WorldMapping.Add(handle, world);

        return handle;
    }

    public static List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedResponse> Get(uint handle)
    {
        if (!Storage.TryGetValue(handle, out var value))
        {
            throw new ArgumentException($"ModifyHealthResponsesProvider does not contain handle {handle}");
        }

        return value;
    }

    public static void Set(uint handle, List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedResponse> value)
    {
        if (!Storage.ContainsKey(handle))
        {
            throw new ArgumentException($"ModifyHealthResponsesProvider does not contain handle {handle}");
        }

        Storage[handle] = value;
    }

    public static void Free(uint handle)
    {
        Storage.Remove(handle);
        WorldMapping.Remove(handle);
    }

    public static void CleanDataInWorld(global::Unity.Entities.World world)
    {
        var handles = WorldMapping.Where(pair => pair.Value == world).Select(pair => pair.Key).ToList();

        foreach (var handle in handles)
        {
            Free(handle);
        }
    }

    private static uint GetNextHandle() 
    {
        nextHandle++;
        
        while (Storage.ContainsKey(nextHandle))
        {
            nextHandle++;
        }

        return nextHandle;
    }
}


            public static class RequestRespawnSenderProvider 
{
    private static readonly Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.Request>> Storage = new Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.Request>>();
    private static readonly Dictionary<uint, global::Unity.Entities.World> WorldMapping = new Dictionary<uint, Unity.Entities.World>();

    private static uint nextHandle = 0;

    public static uint Allocate(global::Unity.Entities.World world)
    {
        var handle = GetNextHandle();

        Storage.Add(handle, default(List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.Request>));
        WorldMapping.Add(handle, world);

        return handle;
    }

    public static List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.Request> Get(uint handle)
    {
        if (!Storage.TryGetValue(handle, out var value))
        {
            throw new ArgumentException($"RequestRespawnSenderProvider does not contain handle {handle}");
        }

        return value;
    }

    public static void Set(uint handle, List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.Request> value)
    {
        if (!Storage.ContainsKey(handle))
        {
            throw new ArgumentException($"RequestRespawnSenderProvider does not contain handle {handle}");
        }

        Storage[handle] = value;
    }

    public static void Free(uint handle)
    {
        Storage.Remove(handle);
        WorldMapping.Remove(handle);
    }

    public static void CleanDataInWorld(global::Unity.Entities.World world)
    {
        var handles = WorldMapping.Where(pair => pair.Value == world).Select(pair => pair.Key).ToList();

        foreach (var handle in handles)
        {
            Free(handle);
        }
    }

    private static uint GetNextHandle() 
    {
        nextHandle++;
        
        while (Storage.ContainsKey(nextHandle))
        {
            nextHandle++;
        }

        return nextHandle;
    }
}

            public static class RequestRespawnRequestsProvider 
{
    private static readonly Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedRequest>> Storage = new Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedRequest>>();
    private static readonly Dictionary<uint, global::Unity.Entities.World> WorldMapping = new Dictionary<uint, Unity.Entities.World>();

    private static uint nextHandle = 0;

    public static uint Allocate(global::Unity.Entities.World world)
    {
        var handle = GetNextHandle();

        Storage.Add(handle, default(List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedRequest>));
        WorldMapping.Add(handle, world);

        return handle;
    }

    public static List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedRequest> Get(uint handle)
    {
        if (!Storage.TryGetValue(handle, out var value))
        {
            throw new ArgumentException($"RequestRespawnRequestsProvider does not contain handle {handle}");
        }

        return value;
    }

    public static void Set(uint handle, List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedRequest> value)
    {
        if (!Storage.ContainsKey(handle))
        {
            throw new ArgumentException($"RequestRespawnRequestsProvider does not contain handle {handle}");
        }

        Storage[handle] = value;
    }

    public static void Free(uint handle)
    {
        Storage.Remove(handle);
        WorldMapping.Remove(handle);
    }

    public static void CleanDataInWorld(global::Unity.Entities.World world)
    {
        var handles = WorldMapping.Where(pair => pair.Value == world).Select(pair => pair.Key).ToList();

        foreach (var handle in handles)
        {
            Free(handle);
        }
    }

    private static uint GetNextHandle() 
    {
        nextHandle++;
        
        while (Storage.ContainsKey(nextHandle))
        {
            nextHandle++;
        }

        return nextHandle;
    }
}

            public static class RequestRespawnResponderProvider 
{
    private static readonly Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.Response>> Storage = new Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.Response>>();
    private static readonly Dictionary<uint, global::Unity.Entities.World> WorldMapping = new Dictionary<uint, Unity.Entities.World>();

    private static uint nextHandle = 0;

    public static uint Allocate(global::Unity.Entities.World world)
    {
        var handle = GetNextHandle();

        Storage.Add(handle, default(List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.Response>));
        WorldMapping.Add(handle, world);

        return handle;
    }

    public static List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.Response> Get(uint handle)
    {
        if (!Storage.TryGetValue(handle, out var value))
        {
            throw new ArgumentException($"RequestRespawnResponderProvider does not contain handle {handle}");
        }

        return value;
    }

    public static void Set(uint handle, List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.Response> value)
    {
        if (!Storage.ContainsKey(handle))
        {
            throw new ArgumentException($"RequestRespawnResponderProvider does not contain handle {handle}");
        }

        Storage[handle] = value;
    }

    public static void Free(uint handle)
    {
        Storage.Remove(handle);
        WorldMapping.Remove(handle);
    }

    public static void CleanDataInWorld(global::Unity.Entities.World world)
    {
        var handles = WorldMapping.Where(pair => pair.Value == world).Select(pair => pair.Key).ToList();

        foreach (var handle in handles)
        {
            Free(handle);
        }
    }

    private static uint GetNextHandle() 
    {
        nextHandle++;
        
        while (Storage.ContainsKey(nextHandle))
        {
            nextHandle++;
        }

        return nextHandle;
    }
}

            public static class RequestRespawnResponsesProvider 
{
    private static readonly Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedResponse>> Storage = new Dictionary<uint, List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedResponse>>();
    private static readonly Dictionary<uint, global::Unity.Entities.World> WorldMapping = new Dictionary<uint, Unity.Entities.World>();

    private static uint nextHandle = 0;

    public static uint Allocate(global::Unity.Entities.World world)
    {
        var handle = GetNextHandle();

        Storage.Add(handle, default(List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedResponse>));
        WorldMapping.Add(handle, world);

        return handle;
    }

    public static List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedResponse> Get(uint handle)
    {
        if (!Storage.TryGetValue(handle, out var value))
        {
            throw new ArgumentException($"RequestRespawnResponsesProvider does not contain handle {handle}");
        }

        return value;
    }

    public static void Set(uint handle, List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedResponse> value)
    {
        if (!Storage.ContainsKey(handle))
        {
            throw new ArgumentException($"RequestRespawnResponsesProvider does not contain handle {handle}");
        }

        Storage[handle] = value;
    }

    public static void Free(uint handle)
    {
        Storage.Remove(handle);
        WorldMapping.Remove(handle);
    }

    public static void CleanDataInWorld(global::Unity.Entities.World world)
    {
        var handles = WorldMapping.Where(pair => pair.Value == world).Select(pair => pair.Key).ToList();

        foreach (var handle in handles)
        {
            Free(handle);
        }
    }

    private static uint GetNextHandle() 
    {
        nextHandle++;
        
        while (Storage.ContainsKey(nextHandle))
        {
            nextHandle++;
        }

        return nextHandle;
    }
}


        }
    }
}
