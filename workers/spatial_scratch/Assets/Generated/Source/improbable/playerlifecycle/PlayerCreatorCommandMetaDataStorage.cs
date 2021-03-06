// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System.Collections.Generic;
using Improbable.Gdk.Core;

namespace Improbable.PlayerLifecycle
{
    public partial class PlayerCreator
    {
        public class CreatePlayerCommandMetaDataStorage : ICommandMetaDataStorage, ICommandPayloadStorage<global::Improbable.PlayerLifecycle.CreatePlayerRequestType>
        {
            private readonly Dictionary<long, CommandContext<global::Improbable.PlayerLifecycle.CreatePlayerRequestType>> requestIdToRequest =
                new Dictionary<long, CommandContext<global::Improbable.PlayerLifecycle.CreatePlayerRequestType>>();

            private readonly Dictionary<uint, long> internalRequestIdToRequestId = new Dictionary<uint, long>();

            public uint GetComponentId()
            {
                return ComponentId;
            }

            public uint GetCommandId()
            {
                return 1;
            }

            public void RemoveMetaData(uint internalRequestId)
            {
                var requestId = internalRequestIdToRequestId[internalRequestId];
                internalRequestIdToRequestId.Remove(internalRequestId);
                requestIdToRequest.Remove(requestId);
            }

            public void SetInternalRequestId(uint internalRequestId, long requestId)
            {
                internalRequestIdToRequestId.Add(internalRequestId, requestId);
            }

            public void AddRequest(in CommandContext<global::Improbable.PlayerLifecycle.CreatePlayerRequestType> context)
            {
                requestIdToRequest[context.RequestId] = context;
            }

            public CommandContext<global::Improbable.PlayerLifecycle.CreatePlayerRequestType> GetPayload(uint internalRequestId)
            {
                var id = internalRequestIdToRequestId[internalRequestId];
                return requestIdToRequest[id];
            }
        }

    }
}
