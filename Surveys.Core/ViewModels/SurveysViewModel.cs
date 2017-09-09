using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Surveys.Core.Models;
using Surveys.Core.Views;
using Prism.Navigation;
using Prism.Commands;
using Surveys.Core.ServiceInterface;
using Prism.Services;
using Surveys.Entities;

namespace Surveys.Core.ViewModels
{
    public class SurveysViewModel : ViewModelBase
    {
        private INavigationService navigationService = null;
        private ILocalDbService localDbService = null;
        private IPageDialogService pageDialogService = null;
        public ICommand NewSurveyCommand { get; set; }
        public ICommand DeleteSurveyCommand { get; set; }
        public bool IsEmpty => Surveys == null || !Surveys.Any();
        private ObservableCollection<SurveyViewModel> surveys;
        public ObservableCollection<SurveyViewModel> Surveys
        {
            get
            {
                return surveys;
            }
            set
            {
                if (surveys == value)
                {
                    return;
                }
                surveys = value;
                OnPropertyChanged();
            }
        }
        private SurveyViewModel selectedSurvey;
        public SurveyViewModel SelectedSurvey
        {
            get
            {
                return selectedSurvey;
            }
            set
            {
                if (selectedSurvey == value)
                {
                    return;
                }
                selectedSurvey = value;
                OnPropertyChanged();
            }
        }
        public SurveysViewModel(INavigationService navigationService, ILocalDbService localDbService, IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;
            this.localDbService = localDbService;
            this.pageDialogService = pageDialogService;
            surveys = new ObservableCollection<SurveyViewModel>();
            NewSurveyCommand = new DelegateCommand(NewSurveyCommandExcecute);
            DeleteSurveyCommand = new DelegateCommand(DeleteSurveyCommandExcecute, DeleteSurveyCommandCanExcecute).ObservesProperty(() => SelectedSurvey);
        }

        private bool DeleteSurveyCommandCanExcecute()
        {
            return SelectedSurvey != null;
        }

        private async void DeleteSurveyCommandExcecute()
        {
            if (selectedSurvey == null) return;

            var result = await pageDialogService.DisplayAlertAsync(Literals.DeleteSurveyTitle, Literals.DeleteSurveyConfirmation, Literals.Ok, Literals.Cancel);
            if (result)
            {
                await localDbService.DeleteSurveyAsync(SurveyViewModel.GetEntityFromViewModel(SelectedSurvey));
            }
            await LoadSurveysAsync();
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            try
            {
                await LoadSurveysAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        private async Task LoadSurveysAsync()
        {
            try
            {

                var allTeams = await localDbService.GetAllTeamsAsync();
                var allSurveys = await localDbService.GetAllSurveysAsync();
                if (allSurveys != null)
                {
                    Surveys = new ObservableCollection<SurveyViewModel>(allSurveys.Select(s => SurveyViewModel.GetViewModelFromEntity(s, allTeams)));
                }
                OnPropertyChanged(nameof(IsEmpty));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private async void NewSurveyCommandExcecute()
        {
            await navigationService.NavigateAsync(nameof(SurveyDetailsView));
        }
    }
}
