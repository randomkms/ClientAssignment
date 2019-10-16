using Prism.Mvvm;

namespace ClientAssignment.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "User checker";
        public string Title
        {
            get => this._title;

            set => this.SetProperty(ref this._title, value);
        }

        public MainWindowViewModel()
        {

        }
    }
}
