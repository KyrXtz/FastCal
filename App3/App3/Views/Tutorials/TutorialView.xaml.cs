using FFImageLoading;
using FFImageLoading.Forms;
using FFImageLoading.Work;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorialView : ContentPage
    {
        public List<string> names { get; set; }
        public List<CachedImage> cachedImages { get; set; }

        //private int _currentIndex;
        //public int CurrentIndex
        //{
        //    get => _currentIndex;
        //    set
        //    {
        //        _currentIndex = value;
        //        OnPropertyChanged();
        //    }
        //}
        public TutorialView()
        {
            // preloadImage("tut1.gif");
            //preloadImage("tut2.gif");
            //preloadImage("tut3.gif");
            //preloadImage("tut4.gif");
            //preloadImage("tut5.gif");
            //preloadImage("tut6.gif");




            BindingContext = this;
            InitializeComponent();
            names = new List<string>
                {
                  //  "loader.png",
                
                  //"tut1.gif",
                  //"tut2.gif",
                  // "tut3.gif",
                  //   "tut4.gif",
                  //  "tut5.gif",
                  //  "tut6.gif",
                  //  "splashscreen.png",




                };
            MainCarouselView.ItemsSource = names;
        }

        private void MainCarouselView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == "SelectedIndex")
            //{
            //    switch (MainCarouselView.SelectedIndex)
            //    {
            //        case 0:
            //            IndexLabel.Text = "Welcome to Fast Cal!\nThis is a short tutorial to get you started.";
            //            break;
            //        case 1:
            //            IndexLabel.Text = "First, select a time for when your day starts.\n At that time your daily calories will be reset.";
            //            break;
            //        case 2:
            //            IndexLabel.Text = "Then, calculate your calorie goals in the settings menu.\nYou can set your goals manually, or you can calculate them based on your metabolic rate.";

            //            break;
            //        case 3:
            //            IndexLabel.Text = "Simply clicking on button will register it as consumed.\nLong click on a button to see info about the food.";
            //            break;
            //        case 4:
            //            IndexLabel.Text ="Tap the + sign to add buttons; either manually or by searching the database.";
            //            break;
            //        case 5:
            //            IndexLabel.Text = "Enter a food name to search the database. Choose the result that best suits you and the select the portion.\nYou can also change the name of the food after the search is done.";
            //            break;
            //        case 6:
            //            IndexLabel.Text = "Finally, Overview displays todays progress towards your goals; and all data collected from previous dates.\nDouble tap on a date on the calendar to see all consumed food of that day.  ";
            //            break;
            //        case 7:
            //            IndexLabel.Text = "That's it! Get busy counting!\nAnd dont forget to give 5 stars <3 <3 <3";
            //            Application.Current.Properties["viewedtutorial"] = "ok";
            //            break;
            //    }
            //}
        }
        //private  void preloadImage(string imageFile)
        //{
        //    TaskParameter imageTask = ImageService.Instance.LoadCompiledResource(imageFile);
        //    CachedImage image = new CachedImage();
        //    imageTask.Error((Exception obj) =>
        //    {
        //        Debug.WriteLine("LoadFile Error: {0}", obj);
        //    });
        //    imageTask.Success((ImageInformation info, LoadingResult result) =>
        //    {
        //        Debug.WriteLine("LoadFile Success: {0} : {1}", info, result);

        //        Device.BeginInvokeOnMainThread(() =>
        //        {
        //             image = new CachedImage
        //            {
        //                 Aspect = Aspect.AspectFit,
        //                Source = imageFile
        //            };
                    


        //        });
        //    });
        //    imageTask.Preload();

         

        //}
    }
}