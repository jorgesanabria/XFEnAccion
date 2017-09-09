using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Surveys.Core.ServiceInterface;
using Surveys.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surveys.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private INavigationService navigationService = null;
        private IWebApiService webApiService;
        private IPageDialogService pageDialogService;
        private string username;
        private string password;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (username == value) return;
                username = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password == value) return;
                password = value;
                OnPropertyChanged();
            }
        }
        public ICommand LoginCommand { get; set; }
        public LoginViewModel(INavigationService navigationService, IWebApiService webApiService, IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;
            this.webApiService = webApiService;
            this.pageDialogService = pageDialogService;
            LoginCommand = new DelegateCommand
                (
                    LoginCommandExcecute, LoginCommandCanExcecute
                ).ObservesProperty(() => Username).ObservesProperty(() => Password);
        }
        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy == value) return;

                isBusy = value;
                OnPropertyChanged();
            }
        }
        private bool LoginCommandCanExcecute()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        private async void LoginCommandExcecute()
        {
            IsBusy = true;
            try
            {
                var loginResult = await webApiService.LoginAsync(Username, Password);
                if (loginResult)
                {
                    await navigationService.NavigateAsync($"app:///{nameof(MainView)}/{nameof(RootNavigationView)}/{nameof(SurveysView)}");
                }
                else
                {
                    await pageDialogService.DisplayAlertAsync(Literals.LoginTitle, Literals.AccesDenied, Literals.Ok);
                }
            }
            catch (Exception e)
            {
                await pageDialogService.DisplayAlertAsync(Literals.LoginTitle, e.Message, Literals.Ok);
            }
            IsBusy = false;
        }
    }
}
