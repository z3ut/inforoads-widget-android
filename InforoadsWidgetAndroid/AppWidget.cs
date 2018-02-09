using System;
using Android;
using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;
using InforoadsWidgetAndroid;

[BroadcastReceiver(Label = "@string/app_name")]
[IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
[MetaData("android.appwidget.provider", Resource = "@xml/appwidgetprovider")]
public class AppWidget : AppWidgetProvider
{
    public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
    {
        context.StartService(new Intent(context, typeof(UpdateMesuringService)));
    }
}