using Prism.Navigation;
using Surveys.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Surveys.Core.ViewModels
{
    public class TeamSelectionViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private ILocalDbService localDbService;
        private ObservableCollection<TeamViewModel> teams;
        public ObservableCollection<TeamViewModel> Teams
        {
            get { return teams; }
            set
            {
                if (teams == value) return;

                teams = value;
                OnPropertyChanged();
            }
        }
        private TeamViewModel selectedTeam;
        public TeamViewModel SelectedTeam
        {
            get { return selectedTeam; }
            set
            {
                if (selectedTeam == value) return;

                selectedTeam = value;
                OnPropertyChanged();
            }
        }
        public TeamSelectionViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            this.navigationService = navigationService;
            this.localDbService = localDbService;
            PropertyChanged += TeamSelectionViewModel_PropertyChanged;
        }

        private async void TeamSelectionViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedTeam))
            {
                if (SelectedTeam == null) return;

                var param = new NavigationParameters { { "id", SelectedTeam.Id } };
                await navigationService.GoBackAsync(param, true);
            }
        }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            var allTeams = await localDbService.GetAllTeamsAsync();
            if (allTeams != null)
            {
                Teams = new ObservableCollection<TeamViewModel>(allTeams.Select(TeamViewModel.GetViewModelFromEntity));
            }
        }
    }
}
