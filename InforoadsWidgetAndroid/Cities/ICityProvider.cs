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
using InforoadsWidgetAndroid.Cities.Models;

namespace InforoadsWidgetAndroid.Cities
{
    public interface ICityProvider
    {
        IEnumerable<City> GetCities();
        City GetUserCity();
        void SetUserCity(City city);
    }
}