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
    public partial class MealTutorialView2 : PopupPage
    {
        public MealTutorialView2()
        {
            InitializeComponent();
            TutLabel.Text = "Here you can see info about the button.\nYou can also tap the trashcan button to permanently delete the button...";
            TutDeleteButon.IsVisible = false;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //FadeIn();
        }
        //private async void FadeIn()
        //{
        //    await TutRemoveEntryButton.FadeTo(0.5, 350, Easing.SpringIn);
        //    FadeOut();
        //}
        //private async void FadeOut()
        //{
        //    await TutRemoveEntryButton.FadeTo(0, 550, Easing.SpringIn);
        //    await Task.Delay(400);
        //    FadeIn();
        //}
        //private async void FadeIn2()
        //{
        //    await TutDeleteButon.FadeTo(0.5, 350, Easing.SpringIn);
        //    FadeOut2();
        //}
        //private async void FadeOut2()
        //{
        //    await TutDeleteButon.FadeTo(0, 550, Easing.SpringIn);
        //    await Task.Delay(400);
        //    FadeIn2();
        //}
        private int timestapped = 0;
        protected override bool OnBackgroundClicked()
        {
            //CloseAllPopup();
            timestapped += 1;
            
           
            NextLabel();
            if (timestapped == 4)
            {
                TutDeleteButon_Clicked(null, null);
              
            }
            return false;
        }
        private void TutRemoveEntryButton_Clicked(object sender, EventArgs e)
        {
            TutRemoveEntryButton.IsVisible = false;
            TutDeleteButon.IsVisible = true;
            TutLabel.Text = "...and tap on that button to permanently delete the selected food.";
            FrameTut.Margin = new Thickness(0,-70,0,0);
            //FadeIn2();
        }
        private async void TutDeleteButon_Clicked(object sender, EventArgs e)
        {
            
            Application.Current.Properties["mealviewedtutorial2"] = "ok";
            await Application.Current.SavePropertiesAsync();
           
            await this.Navigation.RemovePopupPageAsync(this);

        }
        private void NextLabel()
        {
            if(timestapped == 1)
            {
                FrameTut.Margin = new Thickness(0,-155,0,0);
                TutLabel.Text = "... or tap on the -1 button to remove one entry...";

            }
            if (timestapped == 2)
            {
                FrameTut.Margin = new Thickness(0, -175, 0, 0);
                TutLabel.Text = "... or tap the clock button to change the time of day this food appears in...";

            }
            if (timestapped == 3)
            {
                FrameTut.Margin = new Thickness(0, -220, 0, 0);
                TutLabel.Text = "... and finally tap on the edit button to edit the name of the food.";

            }

        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            timestapped += 1;
            NextLabel();
            if (timestapped == 4)
            {
                TutDeleteButon_Clicked(null, null);
            }
        }
    }
}