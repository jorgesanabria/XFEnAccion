using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Surveys.Core.Models;
using Surveys.Core.ServiceInterface;
using Surveys.Core.Views;
using Prism.Navigation;
using Prism.Services;
using Prism.Commands;
using Surveys.Entities;

namespace Surveys.Core.ViewModels
{
    public class SurveyDetailsViewModel : ViewModelBase
    {
        private INavigationService navigationService = null;
        private IPageDialogService pageDialogService = null;
        private ILocalDbService localDbService = null;
        public SurveyDetailsViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILocalDbService localDbService)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            this.localDbService = localDbService;
            //Teams = new ObservableCollection<string>
            //{
            //    "Alianza Lima",
            //    "América",
            //    "River Plate",
            //    "Colo Colo",
            //    "Real Madrid"
            //};
            SelectTeamCommand = new DelegateCommand(SelectCommandExcecute);
            EndSurveyCommand = new DelegateCommand(EndSurveyCommandExcecute, EndSurveyCommandCanExcecute)
                .ObservesProperty(() => Name)
                .ObservesProperty(() => FavoriteTeam);
            //PropertyChanged += SurveyDetailListView_PropertyChanged;
        }
        private IEnumerable<Team> localDbTeams = null;
        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            localDbTeams = await localDbService.GetAllTeamsAsync();
            if (parameters.ContainsKey("id"))
            {
                var result = localDbTeams.FirstOrDefault(t => parameters["id"].ToString().Contains(t.Id.ToString()));
                FavoriteTeam = result != null? result.Name ?? string.Empty: string.Empty;
            }
        }
        private async void SelectCommandExcecute()
        {
            //var team = await pageDialogService.DisplayActionSheetAsync(Literals.FavoriteTeamTitle, null, null, Teams.ToArray());
            //FavoriteTeam = team;
            try
            {
                await navigationService.NavigateAsync(nameof(TeamSelectionView), useModalNavigation: true);
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
        private bool EndSurveyCommandCanExcecute()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrWhiteSpace(FavoriteTeam);
        }
        //private void SurveyDetailListView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == nameof(FavoriteTeam))
        //    {
        //        (EndSurveyCommand as Command)?.ChangeCanExecute();
        //    }
        //}
        private async void EndSurveyCommandExcecute()
        {
            var newSurvey = new Survey
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                BirthDate = BirthDate,
                FavoriteTeam = FavoriteTeam,
                TeamId = localDbTeams.First(t => t.Name == favoriteTeam).Id
            };
            var geoLocationService = Xamarin.Forms.DependencyService.Get<IGeolocationService>();
            if (geoLocationService != null)
            {
                try
                {
                    var currentLocation = await geoLocationService.GetCurrentLocationAsync();
                    newSurvey.Lat = currentLocation.Item1;
                    newSurvey.Lon = currentLocation.Item2;
                }
                catch
                {
                    newSurvey.Lat = 0;
                    newSurvey.Lon = 0;
                }
            }
            //MessagingCenter.Send(this, Messages.NewSurveyComplete, newSurvey);
            await localDbService.InsertSurveyAsync(newSurvey);
            await navigationService.GoBackAsync();
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name == value) return;
                name = value;
                OnPropertyChanged();
            }
        }
        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (birthDate == value) return;
                birthDate = value;
                OnPropertyChanged();
            }
        }
        private string favoriteTeam;
        public string FavoriteTeam
        {
            get { return favoriteTeam; }
            set
            {
                if (favoriteTeam == value) return;
                favoriteTeam = value;
                OnPropertyChanged();
            }
        }
        //private ObservableCollection<string> teams;
        //public ObservableCollection<string> Teams
        //{
        //    get { return teams; }
        //    set
        //    {
        //        if (teams == value) return;
        //        teams = value;
        //        OnPropertyChanged();
        //    }
        //}
        public ICommand SelectTeamCommand { get; set; }
        public ICommand EndSurveyCommand { get; set; }


    }
}
