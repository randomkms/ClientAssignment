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
        private readonly IUserService userService;
        private readonly IRegionManager regionManager;
        private readonly ILoaderService loaderService;

        private string userId = "5059537892278272";

        public SearchViewModel(IUserService userService, IRegionManager regionManager, ILoaderService loaderService)
        {
            this.userService = userService;
            this.regionManager = regionManager;
            this.loaderService = loaderService;
            this.SearchCommand = new DelegateCommand<string>(this.SearchClicked, this.CanSearch).ObservesProperty(() => this.UserId);
        }

        public string UserId
        {
            get => this.userId;

            set => this.SetProperty(ref this.userId, value);
        }

        public DelegateCommand<string> SearchCommand { get; set; }

        private bool CanSearch(string userId)
        {
            return !string.IsNullOrWhiteSpace(this.UserId); //change
        }

        private async void SearchClicked(string userId)
        {
            this.regionManager.Regions["EmailRegion"].RemoveAll();
            this.regionManager.Regions["BrowserRegion"].RemoveAll();

            var cancelTokenSource = new CancellationTokenSource();
            this.loaderService.Show(cancelTokenSource);
            User user = null;
            try
            {
                user = await this.userService.GetById(userId, cancelTokenSource.Token);
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
                this.loaderService.Hide();
            }

            var parameters = new NavigationParameters
            {
                { "user", user }
            };
            this.regionManager.RequestNavigate("EmailRegion", "Email", parameters);
        }
    }
}
