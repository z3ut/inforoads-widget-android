using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;
using InforoadsWidgetAndroid.Measurings;
using InforoadsWidgetAndroid.Measurings.Models;
using Innovative.SolarCalculator;

namespace InforoadsWidgetAndroid
{
    [Service]
    public class UpdateMesuringService : Service
    {
        private IMeasuringProvider _measuringProvider;

        public UpdateMesuringService()
        {
            _measuringProvider = ServiceLocator.Container.Resolve<IMeasuringProvider>();
        }

        public override void OnStart(Intent intent, int startId)
        {
            Task.Run(async () =>
            {
                RemoteViews updateViews = await BuildUpdate(this);
                ComponentName thisWidget = new ComponentName(this,
                    Java.Lang.Class.FromType(typeof(AppWidget)).Name);
                AppWidgetManager manager = AppWidgetManager.GetInstance(this);
                manager.UpdateAppWidget(thisWidget, updateViews);
            });
        }

        public async Task<RemoteViews> BuildUpdate(Context context)
        {
            var widgetView = new RemoteViews(context.PackageName,
                Resource.Layout.widget_weather);

            try
            {
                var dis = await _measuringProvider.GetCurrentMeasuring();

                widgetView.SetTextViewText(Resource.Id.widget_temperature,
                    Math.Round(dis.CurrentMeasuring.AirTemperature).ToString("+#;-#;0") + "\u00B0");

                var weatherImageId = ConvertDisPrecipitationToImageId(dis);
                widgetView.SetImageViewResource(Resource.Id.widget_image, weatherImageId);
            }
            catch(Exception)
            {
                widgetView.SetTextViewText(Resource.Id.widget_temperature, "%");
            }

            return widgetView;
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        private int ConvertDisPrecipitationToImageId(Dis dis)
        {
            SolarTimes solarTimes = new SolarTimes(DateTime.Now.Date, dis.Lat, dis.Lon);
            DateTime sunrise = TimeZoneInfo.ConvertTimeFromUtc(
                solarTimes.Sunrise.ToUniversalTime(), TimeZoneInfo.Local);
            DateTime sunset = TimeZoneInfo.ConvertTimeFromUtc(
                solarTimes.Sunset.ToUniversalTime(), TimeZoneInfo.Local);

            var isDay = sunrise < DateTime.Now && DateTime.Now < sunset;

            int weatherImageId;
            switch (dis.CurrentMeasuring.Precipitation)
            {
                case "No precipitation":
                    weatherImageId = Resource.Drawable.cloud;
                    break;

                case "Cloudy":
                    weatherImageId = isDay ?
                        Resource.Drawable.day_cloudy :
                        Resource.Drawable.night_cloudy;
                    break;

                case "Clear":
                    weatherImageId = isDay ?
                        Resource.Drawable.day_clear :
                        Resource.Drawable.night_clear;
                    break;

                case "Moderate rain":
                case "Weak rain":
                case "Strong rain":
                    weatherImageId = isDay ?
                        Resource.Drawable.day_rain :
                        Resource.Drawable.night_rain;
                    break;

                case "Moderate snow":
                case "Weak snow":
                case "Strong snow":
                    weatherImageId = isDay ?
                        Resource.Drawable.day_snow :
                        Resource.Drawable.night_snow;
                    break;

                default:
                    weatherImageId = Resource.Drawable.cloud;
                    break;
            }

            return weatherImageId;
        }
    }
}