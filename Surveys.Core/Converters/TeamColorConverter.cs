using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Xamarin.Forms;

namespace Surveys.Core.Converters
{
    public class TeamColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var team = (string)value;
            var color = Color.Transparent;
            switch (team)
            {
                case "River Plate":
                    color = Color.Red;
                    break;
                case "América":
                    color = Color.Blue;
                    break;
                case "Colo Colo":
                    color = Color.Green;
                    break;
                default:
                    color = Color.Orange;
                    break;
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
