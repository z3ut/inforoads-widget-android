using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;
using InforoadsWidgetAndroid.Cities;
using InforoadsWidgetAndroid.Measurings;

namespace InforoadsWidgetAndroid
{
    public static class ServiceLocator
    {
        public static Autofac.IContainer Container { get; private set; }

        static ServiceLocator()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CityProvider>()
                .As<ICityProvider>()
                .SingleInstance();

            builder.RegisterType<MeasuringProvider>()
                .As<IMeasuringProvider>()
                .SingleInstance();

            Container = builder.Build();
        }

    }
}