using ClientAssignment.Interfaces;
using ClientAssignment.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TemplatesWPF.CustomMessages;

namespace ClientAssignment.ViewModels
{
    public class EmailViewModel : BindableBase, INavigationAware
    {
        private readonly ISignatureService _signatureService;
        private readonly IRegionManager _regionManager;
        private readonly ILoaderService _loaderService;

        private User _user;
        private Signature _selectedSignature;

        public User CurrentUser
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public Signature SelectedSignature
        {
            get { return _selectedSignature; }
            set { SetProperty(ref _selectedSignature, value); }
        }

        public DelegateCommand<string> RenderCommand { get; set; }

        public EmailViewModel(ISignatureService signatureService, IRegionManager regionManager, ILoaderService loaderService)
        {
            this._signatureService = signatureService;
            this._regionManager = regionManager;
            this._loaderService = loaderService;
            RenderCommand = new DelegateCommand<string>(RenderClicked, CanRender).ObservesProperty(() => SelectedSignature);
        }

        private bool CanRender(string signatureId)
        {
            return !string.IsNullOrWhiteSpace(signatureId);
        }

        private async void RenderClicked(string signatureId)
        {
            var cancelTokenSource = new CancellationTokenSource();
            this._loaderService.Show(cancelTokenSource);
            string html = null;
            try
            {
                html = await this._signatureService.GetHtmlById(signatureId, cancelTokenSource.Token);
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
                { "html", html }
            };
            this._regionManager.RequestNavigate("BrowserRegion", "Browser", parameters);
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
            this.CurrentUser = navigationContext.Parameters["user"] as User;
        }
    }
}
