using RequestViewer.Domain.Commands;
using RequestViewer.Domain.Models;
using RequestViewer.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RequestViewer.WPF.Stores
{
    public class RequestsStore
    {
        private readonly IGetAllRequestsQuery _getAllRequestsQuery;
        private readonly ICreateRequestCommand _createRequestCommand;
        private readonly IUpdateRequestCommand _updateRequestCommand;
        private readonly IDeleteRequestCommand _deleteRequestCommand;

        private readonly List<Request> _requests;
        public IEnumerable<Request> Requests => _requests;

        public RequestsStore(
            IGetAllRequestsQuery getAllRequestsQuery,
            ICreateRequestCommand createRequestCommand,
            IUpdateRequestCommand updateRequestCommand,
            IDeleteRequestCommand deleteRequestCommand)
        {
            _getAllRequestsQuery = getAllRequestsQuery;
            _createRequestCommand = createRequestCommand;
            _updateRequestCommand = updateRequestCommand;
            _deleteRequestCommand = deleteRequestCommand;

            _requests = new List<Request>();
        }

        public event Action RequestsLoaded;
        public event Action<Request> RequestAdded;
        public event Action<Request> RequestUpdated;
        public event Action<Guid> RequestDeleted;

        public async Task Load()
        {
            IEnumerable<Request> requests = await _getAllRequestsQuery.Execute();

            _requests.Clear();
            _requests.AddRange(requests);

            RequestsLoaded?.Invoke();
        }

        public async Task Add(Request request)
        {
            await _createRequestCommand.Execute(request);

            _requests.Add(request);

            RequestAdded?.Invoke(request);
        }

        public async Task Update(Request request)
        {
            await _updateRequestCommand.Execute(request);

            int currentIndex = _requests.FindIndex(x => x.Id == request.Id);
            if (currentIndex != -1)
            {
                _requests[currentIndex] = request;
            }
            else
            {
                _requests.Add(request);
            }

            RequestUpdated?.Invoke(request);
        }

        public async Task Delete(Guid id)
        {
            await _deleteRequestCommand.Execute(id);

            _requests.RemoveAll(x => x.Id == id);

            RequestDeleted?.Invoke(id);
        }
    }
}
