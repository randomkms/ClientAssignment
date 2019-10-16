using System.Threading;
using System.Threading.Tasks;

namespace ClientAssignment.Interfaces
{
    public interface ISignatureService
    {
        Task<string> GetHtmlById(string id, CancellationToken cancellationToken);
    }
}