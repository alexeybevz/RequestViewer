using System;
using System.Threading.Tasks;

namespace RequestViewer.Domain.Commands
{
    public interface IDeleteRequestCommand
    {
        Task Execute(Guid id);
    }
}
