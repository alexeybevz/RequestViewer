using RequestViewer.Domain.Models;
using System.Threading.Tasks;

namespace RequestViewer.Domain.Commands
{
    public interface IApproveRequestCommand
    {
        Task Execute(Request request);
    }
}
