using RequestViewer.Domain.Models;
using System.Threading.Tasks;

namespace RequestViewer.Domain.Commands
{
    public interface ICreateRequestCommand
    {
        Task Execute(Request request);
    }
}
