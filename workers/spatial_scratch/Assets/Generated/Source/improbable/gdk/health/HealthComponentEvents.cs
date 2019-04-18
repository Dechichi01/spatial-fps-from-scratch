// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System.Collections.Generic;
using Improbable.Gdk.Core;
using Improbable.Worker;
using Unity.Entities;

namespace Improbable.Gdk.Health
{
    public partial class HealthComponent
    {
        public static class HealthModified
        {
            public readonly struct Event : IEvent
            {
                public readonly global::Improbable.Gdk.Health.HealthModifiedInfo Payload;

                public Event(global::Improbable.Gdk.Health.HealthModifiedInfo payload)
                {
                    Payload = payload;
                }
            }
        }

        public static class Respawn
        {
            public readonly struct Event : IEvent
            {
                public readonly global::Improbable.Common.Empty Payload;

                public Event(global::Improbable.Common.Empty payload)
                {
                    Payload = payload;
                }
            }
        }

        public static class ReceivedEvents
        {
            public struct HealthModified : IComponentData
            {
                internal uint handle;

                public List<global::Improbable.Gdk.Health.HealthModifiedInfo> Events
                {
                    get => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.HealthModifiedProvider.Get(handle);
                    internal set => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.HealthModifiedProvider.Set(handle, value);
                }
            }

            public struct Respawn : IComponentData
            {
                internal uint handle;

                public List<global::Improbable.Common.Empty> Events
                {
                    get => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.RespawnProvider.Get(handle);
                    internal set => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.RespawnProvider.Set(handle, value);
                }
            }

        }

        public static class EventSender
        {
            public struct HealthModified : IComponentData
            {
                internal uint handle;

                public List<global::Improbable.Gdk.Health.HealthModifiedInfo> Events
                {
                    get => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.HealthModifiedProvider.Get(handle);
                    internal set => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.HealthModifiedProvider.Set(handle, value);
                }
            }

            public struct Respawn : IComponentData
            {
                internal uint handle;

                public List<global::Improbable.Common.Empty> Events
                {
                    get => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.RespawnProvider.Get(handle);
                    internal set => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.RespawnProvider.Set(handle, value);
                }
            }

        }
    }
}
