using System.Threading;
using System.Threading.Tasks;
using ClientAssignment.Models;

namespace ClientAssignment.Interfaces
{
    public interface IUserService
    {
        Task<User> GetById(string id, CancellationToken cancellationToken);
    }
}