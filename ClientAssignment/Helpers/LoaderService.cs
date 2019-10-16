using System.Threading;
using ClientAssignment.Interfaces;
using ClientAssignment.Views;
using MaterialDesignThemes.Wpf;
using Prism.Ioc;

namespace ClientAssignment.Helpers
{
    public class LoaderService : ILoaderService
    {
        private readonly IContainerExtension _container;

        public LoaderService(IContainerExtension container)
        {
            this._container = container;
        }

        public void Show(CancellationTokenSource cancellationTokenSource = null)
        {
            DialogHost.Show(this._container.Resolve<Loader>(), (sender, args) =>
            {
                if (args.Parameter != null)
                {
                    cancellationTokenSource?.Cancel();
                }

                cancellationTokenSource?.Dispose();
            });
        }

        public void Hide()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
