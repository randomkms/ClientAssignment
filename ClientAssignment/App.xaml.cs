using System.Windows;
using BackendCommon;
using ClientAssignment.Helpers;
using ClientAssignment.Interfaces;
using ClientAssignment.Services;
using ClientAssignment.Views;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;

namespace ClientAssignment
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return this.Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Search>();
            containerRegistry.RegisterForNavigation<Email>();
            containerRegistry.RegisterForNavigation<Browser>();
            var restService = new RestService("https://webapp-qa.wisestamp.com/api/");
            containerRegistry.RegisterInstance<IRestService>(restService);
            containerRegistry.RegisterInstance<IUserService>(new UserService(restService));
            containerRegistry.RegisterInstance<ISignatureService>(new SignatureService(restService));
            containerRegistry.Register<ILoaderService, LoaderService>();
            containerRegistry.Register<Loader>();
        }

        protected override void OnInitialized()
        {
            var regionManager = this.Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate("SearchRegion", "Search");
            regionManager.RequestNavigate("BrowserRegion", "Browser"); // for browser component initialization
            base.OnInitialized();
        }
    }
}
