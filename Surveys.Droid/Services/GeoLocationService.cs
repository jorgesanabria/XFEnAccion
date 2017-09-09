using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Surveys.Droid.Services;
using Surveys.Core.ServiceInterface;
using System.Threading.Tasks;
using Android.Locations;

[assembly:Dependency(typeof(GeoLocationService))]

namespace Surveys.Droid.Services
{
    public class GeoLocationService : IGeolocationService
    {
        private readonly LocationManager locationManager = null;
        public GeoLocationService()
        {
            locationManager = Xamarin.Forms.Forms.Context.GetSystemService(Context.LocationService) as LocationManager;
        }
        public Task<Tuple<double, double>> GetCurrentLocationAsync()
        {
            var provider = locationManager.GetBestProvider(new Criteria { Accuracy = Accuracy.Fine }, true);
            var location = locationManager.GetLastKnownLocation(provider);
            var result = new Tuple<double, double>(location.Latitude, location.Longitude);
            return Task.FromResult(result);
        }
    }
}