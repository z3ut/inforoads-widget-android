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
    public class Dis
    {
        public int DisId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public bool IsMeteo { get; set; }
        public bool IsPhoto { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public Measuring CurrentMeasuring { get; set; }
    }
}