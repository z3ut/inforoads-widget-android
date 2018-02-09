using Android.App;
using Android.Widget;
using Android.OS;
using System.Linq;
using InforoadsWidgetAndroid.Cities.Models;
using InforoadsWidgetAndroid.Cities;
using Autofac;
using System;

namespace InforoadsWidgetAndroid
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private ICityProvider _cityProvider;
        private City[] _cities;
        private City _selectedCity;

        public MainActivity()
        {
            _cityProvider = ServiceLocator.Container.Resolve<ICityProvider>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            ActionBar.Hide();

            InitializeCitySpinner();
            InitializaRefreshButton();
        }

        private void InitializaRefreshButton()
        {
            Button refreshButton = FindViewById<Button>(Resource.Id.refresh_measuring_button);
            refreshButton.Click += delegate
            {
                RefreshMeasuring();
            };
        }

        private void InitializeCitySpinner()
        {
            Spinner spinner = FindViewById<Spinner>(Resource.Id.user_city_spinner);

            spinner.ItemSelected += Spinner_ItemSelected;

            _cities = _cityProvider.GetCities().ToArray();
            _selectedCity = _cityProvider.GetUserCity();

            var cityNames = _cities.Select(c => c.Name);

            var adapter = new ArrayAdapter<string>(this,
                 Android.Resource.Layout.SimpleSpinnerItem, cityNames.ToList());

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            spinner.SetSelection(Array.IndexOf(_cities, _selectedCity));
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            _selectedCity = _cities[e.Position];
            _cityProvider.SetUserCity(_selectedCity);
            RefreshMeasuring();
        }

        private void RefreshMeasuring()
        {
            StartService(new Android.Content.Intent(this, typeof(UpdateMesuringService)));
        }
    }
}

