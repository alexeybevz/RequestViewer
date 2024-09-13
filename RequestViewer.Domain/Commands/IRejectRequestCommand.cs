using System.Threading.Tasks;

namespace RequestViewer.Domain.Commands
{
    public interface IRejectRequestCommand
    {
        Task Execute(int id);
    }
}
