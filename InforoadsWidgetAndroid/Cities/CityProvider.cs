using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using InforoadsWidgetAndroid.Cities.Models;

namespace InforoadsWidgetAndroid.Cities
{
    internal class CityProvider : ICityProvider
    {
        private IEnumerable<City> _cities;
        private City _userCity;

        private const string USER_CITY_SHARED_FILE_NAME = "City provider shared file";
        private const string SELECTED_CITY_INDEX_FILE_KEY = "City provider shared file";

        public CityProvider()
        {
            var cityNameMinsk = Application.Context.Resources.GetText(Resource.String.city_name_minsk);
            var cityNameBrest = Application.Context.Resources.GetText(Resource.String.city_name_brest);
            var cityNameGrodno = Application.Context.Resources.GetText(Resource.String.city_name_grodno);
            var cityNameVitebsk = Application.Context.Resources.GetText(Resource.String.city_name_vitebsk);
            var cityNameMogilev = Application.Context.Resources.GetText(Resource.String.city_name_mogilev);
            var cityNameGomel = Application.Context.Resources.GetText(Resource.String.city_name_gomel);
            var cityNameMikashevichi = Application.Context.Resources.GetText(Resource.String.city_name_mikashevichi);

            _cities = new List<City>
            {
                new City() { Id = 1, Name = cityNameMinsk, Lat = 53.9, Lon = 27.6 },
                new City() { Id = 2, Name = cityNameBrest, Lat = 52.1, Lon = 23.7 },
                new City() { Id = 3, Name = cityNameGrodno, Lat = 53.7, Lon = 23.8 },
                new City() { Id = 4, Name = cityNameVitebsk, Lat = 55.2, Lon = 30.2 },
                new City() { Id = 5, Name = cityNameMogilev, Lat = 53.9, Lon = 30.3 },
                new City() { Id = 6, Name = cityNameGomel, Lat = 52.4, Lon = 31 },
                new City() { Id = 7, Name = cityNameMikashevichi, Lat = 52.22, Lon = 27.45 }
            };

            var contextPref = Application.Context.GetSharedPreferences(
                USER_CITY_SHARED_FILE_NAME, FileCreationMode.Private);

            if (contextPref.Contains(SELECTED_CITY_INDEX_FILE_KEY))
            {
                var userCityIndex = contextPref.GetInt(SELECTED_CITY_INDEX_FILE_KEY, 0);
                _userCity = _cities.FirstOrDefault(c => c.Id == userCityIndex);
            }
            else
            {
                _userCity = _cities.First();
            }
        }

        public IEnumerable<City> GetCities()
        {
            return _cities;
        }

        public City GetUserCity()
        {
            return _userCity;
        }

        public void SetUserCity(City city)
        {
            _userCity = city;

            var contextPref = Application.Context.GetSharedPreferences(
                USER_CITY_SHARED_FILE_NAME, FileCreationMode.Private);

            var contextEdit = contextPref.Edit();
            contextEdit.PutInt(SELECTED_CITY_INDEX_FILE_KEY, city.Id);
            contextEdit.Commit();
        }
    }
}