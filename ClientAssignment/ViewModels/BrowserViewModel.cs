using Prism.Mvvm;
using Prism.Regions;

namespace ClientAssignment.ViewModels
{
    public class BrowserViewModel : BindableBase, INavigationAware
    {
        private string renderedHTML;

        public BrowserViewModel()
        {
        }

        public string RenderedHTML
        {
            get => this.renderedHTML;

            set => this.SetProperty(ref this.renderedHTML, value);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.RenderedHTML = navigationContext.Parameters["html"] as string;
        }
    }
}
