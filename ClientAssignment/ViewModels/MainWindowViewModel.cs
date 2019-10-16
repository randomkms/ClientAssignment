using Prism.Mvvm;

namespace ClientAssignment.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "User checker";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
