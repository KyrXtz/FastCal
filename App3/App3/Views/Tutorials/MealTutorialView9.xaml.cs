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
    public partial class MealTutorialView9 : PopupPage
    {
        public MealTutorialView9()
        {
            
            InitializeComponent();
            TutLabel.Text = "Select 2 or more buttons and then tap on the ++ button to create a meal.\nIf you want to cancel the merging de-select all buttons and then tap the ++ button.\nYou can always undo the merging later.";
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
            Application.Current.Properties["mealviewedtutorial9"] = "ok";
            await Application.Current.SavePropertiesAsync();

            await this.Navigation.RemovePopupPageAsync(this);
        }
    }
}