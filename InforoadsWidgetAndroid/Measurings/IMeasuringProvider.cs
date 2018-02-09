﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using InforoadsWidgetAndroid.Measurings.Models;

namespace InforoadsWidgetAndroid.Measurings
{
    interface IMeasuringProvider
    {
        Task<Dis> GetCurrentMeasuring();
    }
}