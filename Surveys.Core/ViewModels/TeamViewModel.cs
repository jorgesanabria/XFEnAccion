using Prism.Mvvm;
using Surveys.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Surveys.Core.ViewModels
{
    public class TeamViewModel : BindableBase
    {
        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id == value) return;

                id = value;
                OnPropertyChanged();
            }
        }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name == value) return;

                name = value;
                OnPropertyChanged();
            }
        }
        private string color;
        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                if (color == value) return;

                color = value;
                OnPropertyChanged();
            }
        }
        private ImageSource logo;
        public ImageSource Logo
        {
            get
            {
                return logo;
            }
            set
            {
                if (logo == value) return;

                logo = value;
                OnPropertyChanged();
            }
        }
        public static TeamViewModel GetViewModelFromEntity(Team team)
        {
            var logo = team.Logo != null ? ImageSource.FromStream(() => new MemoryStream(team.Logo)) : null;
            return new TeamViewModel
            {
                Id = team.Id,
                Name = team.Name,
                Color = team.Color,
                Logo = logo
            };
        }
    }
}
