using Prism.Mvvm;
using Prism.Regions;

namespace ClientAssignment.ViewModels
{
    public class BrowserViewModel : BindableBase, INavigationAware
    {
        private string _renderedHTML;
        public string RenderedHTML
        {
            get => this._renderedHTML;

            set => this.SetProperty(ref this._renderedHTML, value);
        }

        public BrowserViewModel()
        {

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
