// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System.Collections.Generic;
using Unity.Entities;

namespace Improbable.Gdk.Health
{
    public partial class HealthComponent
    {
        public class CommandSenders
        {
            public struct ModifyHealth : IComponentData
            {
                internal uint CommandListHandle;
                public List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.Request> RequestsToSend
                {
                    get => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.ModifyHealthSenderProvider.Get(CommandListHandle);
                    set => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.ModifyHealthSenderProvider.Set(CommandListHandle, value);
                }
            }
            public struct RequestRespawn : IComponentData
            {
                internal uint CommandListHandle;
                public List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.Request> RequestsToSend
                {
                    get => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.RequestRespawnSenderProvider.Get(CommandListHandle);
                    set => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.RequestRespawnSenderProvider.Set(CommandListHandle, value);
                }
            }
        }

        public class CommandRequests
        {
            public struct ModifyHealth : IComponentData
            {
                internal uint CommandListHandle;
                public List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedRequest> Requests
                {
                    get => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.ModifyHealthRequestsProvider.Get(CommandListHandle);
                    set => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.ModifyHealthRequestsProvider.Set(CommandListHandle, value);
                }
            }
            public struct RequestRespawn : IComponentData
            {
                internal uint CommandListHandle;
                public List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedRequest> Requests
                {
                    get => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.RequestRespawnRequestsProvider.Get(CommandListHandle);
                    set => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.RequestRespawnRequestsProvider.Set(CommandListHandle, value);
                }
            }
        }

        public class CommandResponders
        {
            public struct ModifyHealth : IComponentData
            {
                internal uint CommandListHandle;
                public List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.Response> ResponsesToSend
                {
                    get => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.ModifyHealthResponderProvider.Get(CommandListHandle);
                    set => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.ModifyHealthResponderProvider.Set(CommandListHandle, value);
                }
            }
            public struct RequestRespawn : IComponentData
            {
                internal uint CommandListHandle;
                public List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.Response> ResponsesToSend
                {
                    get => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.RequestRespawnResponderProvider.Get(CommandListHandle);
                    set => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.RequestRespawnResponderProvider.Set(CommandListHandle, value);
                }
            }
        }

        public class CommandResponses
        {
            public struct ModifyHealth : IComponentData
            {
                internal uint CommandListHandle;
                public List<Improbable.Gdk.Health.HealthComponent.ModifyHealth.ReceivedResponse> Responses
                {
                    get => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.ModifyHealthResponsesProvider.Get(CommandListHandle);
                    set => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.ModifyHealthResponsesProvider.Set(CommandListHandle, value);
                }
            }
            public struct RequestRespawn : IComponentData
            {
                internal uint CommandListHandle;
                public List<Improbable.Gdk.Health.HealthComponent.RequestRespawn.ReceivedResponse> Responses
                {
                    get => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.RequestRespawnResponsesProvider.Get(CommandListHandle);
                    set => Improbable.Gdk.Health.HealthComponent.ReferenceTypeProviders.RequestRespawnResponsesProvider.Set(CommandListHandle, value);
                }
            }
        }
    }
}
