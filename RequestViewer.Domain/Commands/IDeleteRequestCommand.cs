using System.Threading.Tasks;

namespace RequestViewer.Domain.Commands
{
    public interface IDeleteRequestCommand
    {
        Task Execute(int id);
    }
}
