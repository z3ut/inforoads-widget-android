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

namespace InforoadsWidgetAndroid.Measurings.Models
{
    public class Measuring
    {
        public int DisId { get; set; }
        public DateTime Date { get; set; }
        public double AirTemperature { get; set; }
        public double? Humidity { get; set; }
        public double? DewPoint { get; set; }
        public bool IsRain { get; set; }
        public double? WindSpeed { get; set; }
        public double? WindDirection { get; set; }
        public string WindCardinalDirection { get; set; }
        public double? PrecipitationAmount { get; set; }
        public double? PrecipitationIntensity { get; set; }
        public double? SnowHeight { get; set; }
        public double? Visibility { get; set; }
        public double? Pressure { get; set; }
        public string TemperatureTrend { get; set; }
        public string Precipitation { get; set; }
        public double? RoadTemperature { get; set; }
        public double? SoilTemperature { get; set; }
        public double? FrequencyOfBlackIce { get; set; }
        public double? FreezingPointLiquidus { get; set; }
        public double? FreezingPointSolidus { get; set; }
        public double? ChemicalConcentration { get; set; }
        public double? ChemicalQuantity { get; set; }
        public double? WaterLayerHeight { get; set; }
        public string Warning { get; set; }
        public string Surface { get; set; }
    }
}