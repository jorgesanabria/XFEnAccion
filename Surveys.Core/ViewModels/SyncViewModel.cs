using Prism.Commands;
using Surveys.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace Surveys.Core.ViewModels
{
    public class SyncViewModel : ViewModelBase
    {
        private IWebApiService webApiService;
        private ILocalDbService localDbService;
        private string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if (status == value) return;
                status = value;
                OnPropertyChanged();
            }
        }
        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                if (isBusy == value) return;
                isBusy = value;
                OnPropertyChanged();
            }
        }
        public ICommand SyncCommand { get; set; }
        public SyncViewModel(IWebApiService webApiService, ILocalDbService localDbService)
        {
            this.webApiService = webApiService;
            this.localDbService = localDbService;
            SyncCommand = new DelegateCommand(SyncCommandExcecute);
        }

        private async void SyncCommandExcecute()
        {
            IsBusy = true;
            var allSurveys = await localDbService.GetAllSurveysAsync();
            if (allSurveys != null && allSurveys.Any())
            {
                await webApiService.SaveSurveysAsync(allSurveys);
            }
            var allTeams = await webApiService.GetTeamsAsync();
            if (allTeams != null && allTeams.Any())
            {
                await localDbService.DeleteAllTeamsAync();
                await localDbService.InsertTeamsAsync(allTeams);
            }
            Application.Current.Properties["lastSync"] = DateTime.Now;
            await Application.Current.SavePropertiesAsync();
            Status = $"Se enviaron {allSurveys.Count()} encuestas y se obtuvieron {allTeams.Count()} equipos";
            IsBusy = false;
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Status = Application.Current.Properties.ContainsKey("lastSync") ? $"Última actualización: {(DateTime)Application.Current.Properties["lastSync"]}" : "No se a podido sincronizar los datos";
        }
    }
}
