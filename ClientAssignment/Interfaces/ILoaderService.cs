using System.Threading;

namespace ClientAssignment.Interfaces
{
    public interface ILoaderService
    {
        void Show(CancellationTokenSource cancellationTokenSource = null);

        void Hide();
    }
}