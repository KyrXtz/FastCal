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
    public partial class MealTutorialView5 : PopupPage
    {
        public MealTutorialView5()
        {
            
            InitializeComponent();
            TutLabel.Text = "Tap on a result to see exact information about its calories.\nIf you think it is not what you were looking for you can press cancel and select another result, otherwise press ok. ";
            //TutSearchButton.IsVisible = false;
        }

        protected override void OnAppearing()
        {
          
            base.OnAppearing();
            //FadeIn();
        }
        //private async void FadeIn()
        //{
        //    await TutSearchButton.FadeTo(0.5, 350, Easing.SpringIn);
        //    FadeOut();
        //}
        //private async void FadeOut()
        //{
        //    await TutSearchButton.FadeTo(0, 550, Easing.SpringIn);
        //    await Task.Delay(400);
        //    FadeIn();
        //}


        private void OnBackgroundClicked1()
        {
            TapGestureRecognizer_Tapped(null, null);

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Application.Current.Properties["mealviewedtutorial5"] = "ok";
            await Application.Current.SavePropertiesAsync();

            await this.Navigation.RemovePopupPageAsync(this);
        }
    }
}