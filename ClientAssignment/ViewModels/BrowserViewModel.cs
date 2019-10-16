using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientAssignment.ViewModels
{
    public class BrowserViewModel : BindableBase, INavigationAware
    {
        private string _renderedHTML;
        public string RenderedHTML
        {
            get { return _renderedHTML; }
            set { SetProperty(ref _renderedHTML, value); }
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
