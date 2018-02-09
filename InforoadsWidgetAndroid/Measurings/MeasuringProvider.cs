using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using InforoadsWidgetAndroid.Cities;
using InforoadsWidgetAndroid.Measurings.Models;
using Newtonsoft.Json;

namespace InforoadsWidgetAndroid.Measurings
{
    internal class MeasuringProvider : IMeasuringProvider
    {
        private ICityProvider _cityProvider;

        public MeasuringProvider(ICityProvider cityProvider)
        {
            //_cityProvider = new CityProvider();
            _cityProvider = cityProvider;
        }

        public async Task<Dis> GetCurrentMeasuring()
        {
            var request = HttpWebRequest.Create("http://i.centr.by/inforoads/api/v3/dises?currentMeasuring=true");
            request.ContentType = "application/json";
            request.Method = "GET";
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en");

            using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpListenerException();
                }
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    var dises = JsonConvert.DeserializeObject<Dis[]>(content);

                    var userCity = _cityProvider.GetUserCity();

                    return dises
                        .ToList()
                        .OrderBy(dis => Math.Sqrt(Math.Pow(userCity.Lat - dis.Lat, 2) + Math.Pow(userCity.Lon - dis.Lon, 2)))
                        .First();
                }
            }
        }
    }
}