﻿using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Surveys.Core;

namespace Surveys.Droid
{
    [Activity(Label = "Surveys.Droid", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MainTheme")]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

