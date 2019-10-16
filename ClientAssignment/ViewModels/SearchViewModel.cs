using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ClientAssignment.Interfaces;
using ClientAssignment.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using TemplatesWPF.CustomMessages;

namespace ClientAssignment.ViewModels
{
    public class SearchViewModel : BindableBase
    {
        private readonly IUserService _userService;
        private readonly IRegionManager _regionManager;
        private readonly ILoaderService _loaderService;

        private string _userId = "5059537892278272";
        public string UserId
        {
            get => this._userId;

            set => this.SetProperty(ref this._userId, value);
        }

        public DelegateCommand<string> SearchCommand { get; set; }

        public SearchViewModel(IUserService userService, IRegionManager regionManager, ILoaderService loaderService)
        {
            this._userService = userService;
            this._regionManager = regionManager;
            this._loaderService = loaderService;
            this.SearchCommand = new DelegateCommand<string>(this.SearchClicked, this.CanSearch).ObservesProperty(() => this.UserId);
        }

        private bool CanSearch(string userId)
        {
            return !string.IsNullOrWhiteSpace(this.UserId); //change
        }

        private async void SearchClicked(string userId)
        {
            this._regionManager.Regions["EmailRegion"].RemoveAll();
            this._regionManager.Regions["BrowserRegion"].RemoveAll();

            var cancelTokenSource = new CancellationTokenSource();
            this._loaderService.Show(cancelTokenSource);
            User user = null;
            try
            {
                user = await this._userService.GetById(userId, cancelTokenSource.Token);
            }
            catch (ApiException apiEx)
            {
                CustomMessage.Show(apiEx.Message, null, MessageType.Error);
                return;
            }
            catch (TaskCanceledException)
            {
                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                CustomMessage.Show("Something went wrong", null, MessageType.Error);
                return;
            }
            finally
            {
                this._loaderService.Hide();
            }

            var parameters = new NavigationParameters
            {
                { "user", user }
            };
            this._regionManager.RequestNavigate("EmailRegion", "Email", parameters);
        }
    }
}
