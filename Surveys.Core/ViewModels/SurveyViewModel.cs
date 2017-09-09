using Prism.Mvvm;
using Surveys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveys.Core.ViewModels
{
    public class SurveyViewModel : BindableBase
    {
        public string Id { get; set; }
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
        private TeamViewModel team;
        public TeamViewModel Team
        {
            get { return team; }
            set
            {
                if (team == value) return;

                team = value;
                OnPropertyChanged();
            }
        }
        private double lat;
        private double lon;
        public double Lat
        {
            get { return lat; }
            set
            {
                if (lat == value) return;

                lat = value;
                OnPropertyChanged();
            }
        }
        public double Lon
        {
            get { return lon; }
            set
            {
                if (lon == value) return;

                lon = value;
                OnPropertyChanged();
            }
        }
        public static SurveyViewModel GetViewModelFromEntity(Survey entity, IEnumerable<Team> teams)
        {
            var entidad = teams.FirstOrDefault(t => t.Id == entity.TeamId);
            var team = entidad == null? null: TeamViewModel.GetViewModelFromEntity(entidad);
            var result =  new SurveyViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                BirthDate = entity.BirthDate,
                Team = team,
                Lat = entity.Lat,
                Lon = entity.Lon
            };
            return result;
        }
        public static Survey GetEntityFromViewModel(SurveyViewModel viewModel)
        {
            return new Survey
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                BirthDate = viewModel.BirthDate,
                TeamId = viewModel.Team != null? viewModel.Team.Id: 0,
                Lat = viewModel.Lat,
                Lon = viewModel.Lon
            };
        }
    }
}
