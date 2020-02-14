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
    public partial class MealTutorialView7 : PopupPage
    {
        public MealTutorialView7()
        {

            InitializeComponent();
            TutLabel.Text = "We're almost done!\nNow you can see all the food details based on the portion you selected, modify any value you want.";
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
            Application.Current.Properties["mealviewedtutorial7"] = "ok";
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
                TutLabel.Text = "Or you can just tap the create button and save your search.";
                TutFrame.Margin = new Thickness(0,150,0,0);
            }
        }
    }
}