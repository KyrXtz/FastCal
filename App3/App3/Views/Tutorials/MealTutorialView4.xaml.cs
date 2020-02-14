using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views.Tutorials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealTutorialView4 : PopupPage
    {
        public MealTutorialView4()
        {
            
            InitializeComponent();
            TutLabel.Text = "You can either search for a food in the dabatase, or swipe left and enter the information manually if you already know it.";
            TutSearchButton.IsVisible = false;
        }

        protected override void OnAppearing()
        {
          
            base.OnAppearing();
            FadeIn();
        }
        private async void FadeIn()
        {
            await TutSearchButton.FadeTo(0.5, 350, Easing.SpringIn);
            FadeOut();
        }
        private async void FadeOut()
        {
            await TutSearchButton.FadeTo(0, 550, Easing.SpringIn);
            await Task.Delay(400);
            FadeIn();
        }

      

        private async void TutSearchButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["mealviewedtutorial4"] = "ok";
            await Application.Current.SavePropertiesAsync();

            await this.Navigation.RemovePopupPageAsync(this);
        }
        public bool tapped = false;
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (!tapped)
            {
                tapped = true;
                TutSearchButton.IsVisible = true;
                TutLabel.Text = "For now, let's search the database.\nType something in the field (e.g. bread) and then either tap on the background or press enter to start searching.";
            }
        }
    }
}