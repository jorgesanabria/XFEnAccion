using Prism.Commands;
using Prism.Navigation;
using Surveys.Core.Models;
using Surveys.Core.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Surveys.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Module> modules = null;
        private Module selectedModule = null;
        private INavigationService navigationService = null;
        public ObservableCollection<Module> Modules {
            get
            {
                return modules;
            }
            set
            {
                if (modules == value) return;
                modules = value;
                OnPropertyChanged();
            }
        }
        public Module SelectedModule
        {
            get
            {
                return selectedModule;
            }
            set
            {
                if (selectedModule == value) return;
                selectedModule = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Modules = new ObservableCollection<Module>
            {
                new Module
                {
                    Icon = "survey.png",
                    Title = "Encuestas",
                    LoadModuleCommand = new DelegateCommand
                    ( async () =>
                        await navigationService.NavigateAsync($"{nameof(RootNavigationView)}/{nameof(SurveysView)}")
                    )
                },
                new Module
                {
                    Icon = "about.png",
                    Title = "Acerca de...",
                    LoadModuleCommand = new DelegateCommand
                    ( async () =>
                        await navigationService.NavigateAsync($"{nameof(RootNavigationView)}/{nameof(AboutView)}")
                    )
                },
                new Module
                {
                    Icon = "sync.png",
                    Title = "Sincronización",
                    LoadModuleCommand = new DelegateCommand(
                        async () => await navigationService.NavigateAsync($"{nameof(RootNavigationView)}/{nameof(SyncView)}")
                    )
                }
            };
            PropertyChanged += MainViewPropertyChanged;
        }

        private void MainViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedModule))
            {
                SelectedModule?.LoadModuleCommand.Execute(null);
            }
        }
    }
}
