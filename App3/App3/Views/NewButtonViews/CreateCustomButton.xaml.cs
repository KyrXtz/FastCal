using App3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateCustomButton : ContentPage
    {
        public List<ContentView> names { get; set; }

        public CreateCustomButton(FoodModel.TimeOfDayEmum timeofday)
        {
            BindingContext = this;

            InitializeComponent();

            names = new List<ContentView>();
            //var zx = new ContentView
            //{
            //    Content = new Label
            //    {
            //        Text = "<-SEARCH    MANUAL->",
            //        TextColor = Color.Black,
            //        FontSize = 40,
            //        FontFamily= "NexaRustSlab-BlackShadow01-Free.otf#Nexa Rust Slab Black Shadow 01",
            //        HorizontalOptions = LayoutOptions.Center,
            //        VerticalOptions = LayoutOptions.Center
            //    },
            //};
            //names.Add(zx);
            var xyz = new  CreateSearch(timeofday);
           
            var xy = xyz.Content;
            
            var x = new ContentView
            {
                Content = xy,
            };
            names.Add(x);
            var xyq = new CreateManual(timeofday);
            xy = xyq.Content;
            x = new ContentView
            {
                Content = xy,
            };
            names.Add(x);
            //x = new ContentView
            //{
            //    Content = new Label
            //    {
            //        Text = "OHAI22",
            //        TextColor = Color.Black,
            //        FontSize = 40,
            //        HorizontalOptions = LayoutOptions.Center,
            //        VerticalOptions = LayoutOptions.Center
            //    },
            //};
            //names.Add(x);
            //x = new ContentView
            //{
            //    Content = new Label
            //    {
            //        Text = "OHAI333",
            //        TextColor = Color.Black,
            //        FontSize = 40,
            //        HorizontalOptions = LayoutOptions.Center,
            //        VerticalOptions = LayoutOptions.Center
            //    },
            //};
            //names.Add(x);
            //x = new ContentView
            //{
            //    Content = new Label
            //    {
            //        Text = "OHAI22",
            //        TextColor = Color.Black,
            //        FontSize = 40,
            //        HorizontalOptions = LayoutOptions.Center,
            //        VerticalOptions = LayoutOptions.Center
            //    },
            //};
            //names.Add(x);
            //MainCardsView.ItemsSource = names;
            slideView.ItemsSource = names;
            createSearchRef = xyz;
            createManualRef = xyq;
            ApplyShadows(xyz);
        }
        CreateSearch createSearchRef;
        CreateManual createManualRef;
        private async void ApplyShadows(CreateSearch page)
        {
            await Task.Delay(50);
            page.ApplyShadowLocal();
        }

        private async void ApplyShadows(CreateManual page)
        {
            await Task.Delay(50);
            page.ApplyShadowLocal();
        }

        private void SlideView_SlidedToIndex(object sender, Telerik.XamarinForms.Primitives.SlideView.SlideViewSlidedToIndexEventArgs e)
        {
            if (e.Index == 0)
            {
                ApplyShadows(createSearchRef);

            }
            if (e.Index == 1)
            {
                ApplyShadows(createManualRef);

            }
        }
        //        protected async override void OnDisappearing()
        //#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        //        {
        //            var data = Application.Current.Properties;

        //            if (data["mealviewedtutorial9"].ToString() != "ok" && data["mealviewedtutorial8"].ToString() == "ok")
        //            {
        //                MealView.Invoker.StartTut8();
        //            }
        //            base.OnDisappearing();

        //        } 
        //protected override bool OnBackButtonPressed()
        //        {



        //            var data = Application.Current.Properties;

        //            if (data["mealviewedtutorial9"].ToString() != "ok" && data["mealviewedtutorial8"].ToString() == "ok")
        //            {
        //                MealView.Invoker.StartTut8();
        //            }
        //            return false;

        //    }

    }
}