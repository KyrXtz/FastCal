using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App3.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(App3.Droid.AppRatiing))]
namespace App3.Droid
{
    public class AppRatiing : IAppRating
    {
        public void RateApp()
        {
            var activity = Android.App.Application.Context;
            var url = $"market://details?id={(activity as Context)?.PackageName}";

            try
            {
                activity.PackageManager.GetPackageInfo("com.android.vending", PackageInfoFlags.Activities);
                Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));
                //intent.SetFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                intent.SetFlags(ActivityFlags.NewTask);

                activity.StartActivity(intent);
            }
            catch (PackageManager.NameNotFoundException ex)
            {
                // this won't happen. But catching just in case the user has downloaded the app without having Google Play installed.

                Console.WriteLine(ex.Message);
            }
            catch (ActivityNotFoundException)
            {
                // if Google Play fails to load, open the App link on the browser 

                var playStoreUrl = "https://play.google.com/store/apps/details?id=com.yourapplicationpackagename"; //Add here the url of your application on the store

                var browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(playStoreUrl));
                browserIntent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ResetTaskIfNeeded);

                activity.StartActivity(browserIntent);
            }
        }
    }
}