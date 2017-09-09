﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Surveys.Core.ServiceInterface;
using Surveys.Droid.Services;

[assembly:Xamarin.Forms.Dependency(typeof(SqliteService))]

namespace Surveys.Droid.Services
{
    public class SqliteService : ISqliteService
    {
        public SQLiteConnection GetConnection()
        {
            var localDbFile = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "surveys.db");
            return new SQLiteConnection(localDbFile);
        }
    }
}