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
    public class EmailViewModel : BindableBase, INavigationAware
    {
        private readonly ISignatureService signatureService;
        private readonly IRegionManager regionManager;
        private readonly ILoaderService loaderService;

        private User user;
        private Signature selectedSignature;

        public EmailViewModel(ISignatureService signatureService, IRegionManager regionManager, ILoaderService loaderService)
        {
            this.signatureService = signatureService;
            this.regionManager = regionManager;
            this.loaderService = loaderService;
            this.RenderCommand = new DelegateCommand<string>(this.RenderClicked, this.CanRender).ObservesProperty(() => this.SelectedSignature);
        }

        public User CurrentUser
        {
            get => this.user;

            set => this.SetProperty(ref this.user, value);
        }

        public Signature SelectedSignature
        {
            get => this.selectedSignature;

            set => this.SetProperty(ref this.selectedSignature, value);
        }

        public DelegateCommand<string> RenderCommand { get; set; }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.CurrentUser = navigationContext.Parameters["user"] as User;
        }

        private bool CanRender(string signatureId)
        {
            return !string.IsNullOrWhiteSpace(signatureId);
        }

        private async void RenderClicked(string signatureId)
        {
            var cancelTokenSource = new CancellationTokenSource();
            this.loaderService.Show(cancelTokenSource);
            string html = null;
            try
            {
                html = await this.signatureService.GetHtmlById(signatureId, cancelTokenSource.Token);
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
                { "html", html }
            };
            this.regionManager.RequestNavigate("BrowserRegion", "Browser", parameters);
        }
    }
}
