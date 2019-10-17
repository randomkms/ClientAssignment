using Prism.Mvvm;

namespace ClientAssignment.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string title = "User checker";

        public MainWindowViewModel()
        {
        }

        public string Title
        {
            get => this.title;

            set => this.SetProperty(ref this.title, value);
        }
    }
}
