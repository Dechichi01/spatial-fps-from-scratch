// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System;
using System.Collections.Generic;
using Improbable.Gdk.Core;
using Unity.Entities;

namespace Improbable.Gdk.Health
{
    public partial class HealthComponent
    {
        public class DiffModifyHealthCommandStorage : IComponentCommandDiffStorage
            , IDiffCommandRequestStorage<ModifyHealth.ReceivedRequest>
            , IDiffCommandResponseStorage<ModifyHealth.ReceivedResponse>
        {
            private MessageList<ModifyHealth.ReceivedRequest> requestStorage =
                new MessageList<ModifyHealth.ReceivedRequest>();

            private MessageList<ModifyHealth.ReceivedResponse> responseStorage =
                new MessageList<ModifyHealth.ReceivedResponse>();

            private readonly RequestComparer requestComparer = new RequestComparer();
            private readonly ResponseComparer responseComparer = new ResponseComparer();

            private bool requestsSorted;
            private bool responsesSorted;

            public uint GetComponentId()
            {
                return ComponentId;
            }

            public uint GetCommandId()
            {
                return 1;
            }

            public Type GetRequestType()
            {
                return typeof(ModifyHealth.ReceivedRequest);
            }

            public Type GetResponseType()
            {
                return typeof(ModifyHealth.ReceivedResponse);
            }

            public void Clear()
            {
                requestStorage.Clear();
                responseStorage.Clear();
                requestsSorted = false;
                responsesSorted = false;
            }

            public void RemoveRequests(long entityId)
            {
                requestStorage.RemoveAll(request => request.EntityId.Id == entityId);
            }

            public void AddRequest(ModifyHealth.ReceivedRequest request)
            {
                requestStorage.Add(request);
            }

            public void AddResponse(ModifyHealth.ReceivedResponse response)
            {
                responseStorage.Add(response);
            }

            public ReceivedMessagesSpan<ModifyHealth.ReceivedRequest> GetRequests()
            {
                return new ReceivedMessagesSpan<ModifyHealth.ReceivedRequest>(requestStorage);
            }

            public ReceivedMessagesSpan<ModifyHealth.ReceivedRequest> GetRequests(EntityId targetEntityId)
            {
                if (!requestsSorted)
                {
                    requestStorage.Sort(requestComparer);
                    requestsSorted = true;
                }

                var (firstIndex, count) = requestStorage.GetEntityRange(targetEntityId);

                return new ReceivedMessagesSpan<ModifyHealth.ReceivedRequest>(requestStorage, firstIndex, count);
            }

            public ReceivedMessagesSpan<ModifyHealth.ReceivedResponse> GetResponses()
            {
                return new ReceivedMessagesSpan<ModifyHealth.ReceivedResponse>(responseStorage);
            }

            public ReceivedMessagesSpan<ModifyHealth.ReceivedResponse> GetResponse(long requestId)
            {
                if (!responsesSorted)
                {
                    responseStorage.Sort(responseComparer);
                    responsesSorted = true;
                }

                var responseIndex = responseStorage.GetResponseIndex(requestId);
                if (responseIndex < 0)
                {
                    return ReceivedMessagesSpan<ModifyHealth.ReceivedResponse>.Empty();
                }

                return new ReceivedMessagesSpan<ModifyHealth.ReceivedResponse>(responseStorage, responseIndex, 1);
            }

            private class RequestComparer : IComparer<ModifyHealth.ReceivedRequest>
            {
                public int Compare(ModifyHealth.ReceivedRequest x, ModifyHealth.ReceivedRequest y)
                {
                    return x.EntityId.Id.CompareTo(y.EntityId.Id);
                }
            }

            private class ResponseComparer : IComparer<ModifyHealth.ReceivedResponse>
            {
                public int Compare(ModifyHealth.ReceivedResponse x, ModifyHealth.ReceivedResponse y)
                {
                    return x.RequestId.CompareTo(y.RequestId);
                }
            }
        }

        public class DiffRequestRespawnCommandStorage : IComponentCommandDiffStorage
            , IDiffCommandRequestStorage<RequestRespawn.ReceivedRequest>
            , IDiffCommandResponseStorage<RequestRespawn.ReceivedResponse>
        {
            private MessageList<RequestRespawn.ReceivedRequest> requestStorage =
                new MessageList<RequestRespawn.ReceivedRequest>();

            private MessageList<RequestRespawn.ReceivedResponse> responseStorage =
                new MessageList<RequestRespawn.ReceivedResponse>();

            private readonly RequestComparer requestComparer = new RequestComparer();
            private readonly ResponseComparer responseComparer = new ResponseComparer();

            private bool requestsSorted;
            private bool responsesSorted;

            public uint GetComponentId()
            {
                return ComponentId;
            }

            public uint GetCommandId()
            {
                return 2;
            }

            public Type GetRequestType()
            {
                return typeof(RequestRespawn.ReceivedRequest);
            }

            public Type GetResponseType()
            {
                return typeof(RequestRespawn.ReceivedResponse);
            }

            public void Clear()
            {
                requestStorage.Clear();
                responseStorage.Clear();
                requestsSorted = false;
                responsesSorted = false;
            }

            public void RemoveRequests(long entityId)
            {
                requestStorage.RemoveAll(request => request.EntityId.Id == entityId);
            }

            public void AddRequest(RequestRespawn.ReceivedRequest request)
            {
                requestStorage.Add(request);
            }

            public void AddResponse(RequestRespawn.ReceivedResponse response)
            {
                responseStorage.Add(response);
            }

            public ReceivedMessagesSpan<RequestRespawn.ReceivedRequest> GetRequests()
            {
                return new ReceivedMessagesSpan<RequestRespawn.ReceivedRequest>(requestStorage);
            }

            public ReceivedMessagesSpan<RequestRespawn.ReceivedRequest> GetRequests(EntityId targetEntityId)
            {
                if (!requestsSorted)
                {
                    requestStorage.Sort(requestComparer);
                    requestsSorted = true;
                }

                var (firstIndex, count) = requestStorage.GetEntityRange(targetEntityId);

                return new ReceivedMessagesSpan<RequestRespawn.ReceivedRequest>(requestStorage, firstIndex, count);
            }

            public ReceivedMessagesSpan<RequestRespawn.ReceivedResponse> GetResponses()
            {
                return new ReceivedMessagesSpan<RequestRespawn.ReceivedResponse>(responseStorage);
            }

            public ReceivedMessagesSpan<RequestRespawn.ReceivedResponse> GetResponse(long requestId)
            {
                if (!responsesSorted)
                {
                    responseStorage.Sort(responseComparer);
                    responsesSorted = true;
                }

                var responseIndex = responseStorage.GetResponseIndex(requestId);
                if (responseIndex < 0)
                {
                    return ReceivedMessagesSpan<RequestRespawn.ReceivedResponse>.Empty();
                }

                return new ReceivedMessagesSpan<RequestRespawn.ReceivedResponse>(responseStorage, responseIndex, 1);
            }

            private class RequestComparer : IComparer<RequestRespawn.ReceivedRequest>
            {
                public int Compare(RequestRespawn.ReceivedRequest x, RequestRespawn.ReceivedRequest y)
                {
                    return x.EntityId.Id.CompareTo(y.EntityId.Id);
                }
            }

            private class ResponseComparer : IComparer<RequestRespawn.ReceivedResponse>
            {
                public int Compare(RequestRespawn.ReceivedResponse x, RequestRespawn.ReceivedResponse y)
                {
                    return x.RequestId.CompareTo(y.RequestId);
                }
            }
        }


        public class ModifyHealthCommandsToSendStorage : ICommandSendStorage, IComponentCommandSendStorage
            , ICommandRequestSendStorage<ModifyHealth.Request>
            , ICommandResponseSendStorage<ModifyHealth.Response>
        {
            private MessageList<CommandRequestWithMetaData<ModifyHealth.Request>> requestStorage =
                new MessageList<CommandRequestWithMetaData<ModifyHealth.Request>>();

            private MessageList<ModifyHealth.Response> responseStorage =
                new MessageList<ModifyHealth.Response>();

            public uint GetComponentId()
            {
                return ComponentId;
            }

            public uint GetCommandId()
            {
                return 1;
            }

            public Type GetRequestType()
            {
                return typeof(ModifyHealth.Request);
            }

            public Type GetResponseType()
            {
                return typeof(ModifyHealth.Response);
            }

            public void Clear()
            {
                requestStorage.Clear();
                responseStorage.Clear();
            }

            public void AddRequest(ModifyHealth.Request request, Entity entity, long requestId)
            {
                requestStorage.Add(new CommandRequestWithMetaData<ModifyHealth.Request>(request, entity, requestId));
            }

            public void AddResponse(ModifyHealth.Response response)
            {
                responseStorage.Add(response);
            }

            internal MessageList<CommandRequestWithMetaData<ModifyHealth.Request>> GetRequests()
            {
                return requestStorage;
            }

            internal MessageList<ModifyHealth.Response> GetResponses()
            {
                return responseStorage;
            }
        }

        public class RequestRespawnCommandsToSendStorage : ICommandSendStorage, IComponentCommandSendStorage
            , ICommandRequestSendStorage<RequestRespawn.Request>
            , ICommandResponseSendStorage<RequestRespawn.Response>
        {
            private MessageList<CommandRequestWithMetaData<RequestRespawn.Request>> requestStorage =
                new MessageList<CommandRequestWithMetaData<RequestRespawn.Request>>();

            private MessageList<RequestRespawn.Response> responseStorage =
                new MessageList<RequestRespawn.Response>();

            public uint GetComponentId()
            {
                return ComponentId;
            }

            public uint GetCommandId()
            {
                return 2;
            }

            public Type GetRequestType()
            {
                return typeof(RequestRespawn.Request);
            }

            public Type GetResponseType()
            {
                return typeof(RequestRespawn.Response);
            }

            public void Clear()
            {
                requestStorage.Clear();
                responseStorage.Clear();
            }

            public void AddRequest(RequestRespawn.Request request, Entity entity, long requestId)
            {
                requestStorage.Add(new CommandRequestWithMetaData<RequestRespawn.Request>(request, entity, requestId));
            }

            public void AddResponse(RequestRespawn.Response response)
            {
                responseStorage.Add(response);
            }

            internal MessageList<CommandRequestWithMetaData<RequestRespawn.Request>> GetRequests()
            {
                return requestStorage;
            }

            internal MessageList<RequestRespawn.Response> GetResponses()
            {
                return responseStorage;
            }
        }

    }
}
