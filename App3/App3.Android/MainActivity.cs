using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PanCardView.Droid;
using System.Linq;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Animation;
using FFImageLoading.Forms.Platform;
using App3.Views;

namespace App3.Droid
{

    [Activity(Label = "FastCal", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
    
        public static MainActivity Invoker;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Invoker = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            //base.Window.RequestFeature(WindowFeatures.ActionBar);
            //base.SetTheme(Resource.Style.MainTheme);
            base.Window.RequestFeature(WindowFeatures.ActionBar);
            // Name of the MainActivity theme you had there before.
            // Or you can use global::Android.Resource.Style.ThemeHoloLight
            base.SetTheme(Resource.Style.MainTheme);
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental");
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CardsViewRenderer.Preserve();
            CachedImageRenderer.Init(true);

            LoadApplication(new App());
            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0)); //here
        }
        
        
        public override void OnBackPressed()
        {
            bool exists = Xamarin.Forms.Application.Current.Properties.Any(p => p.Key == "setdate");
            if (!exists)
                return;
            exists = Xamarin.Forms.Application.Current.Properties.Any(p => p.Key == "viewedtutorial");
            if (!exists)
                return;
            //exists = Xamarin.Forms.Application.Current.Properties.Any(p => p.Key == "viewedtutorial");
            //if (!exists)
            //    return;
            var data = Xamarin.Forms.Application.Current.Properties;

            if (data["mealviewedtutorial8"].ToString() != "ok" && data["mealviewedtutorial7"].ToString() == "ok")
            {
                if (MealView.Invoker != null)
                {
                    MealView.Invoker.StartTut8();
                }
            }
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }
        //public void Button1_LongClick(object sender, View.LongClickEventArgs e)
        //{
        //    // Generate clip data package to attach it to the drag
        //    var data = ClipData.NewPlainText("name", "Element 1");

        //    // Start dragging and pass data
        //    ((sender) as Button).StartDrag(data, new View.DragShadowBuilder(((sender) as Button)), null, 0);
        //}
    }
   
}