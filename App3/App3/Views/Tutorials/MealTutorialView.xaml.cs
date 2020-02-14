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
	public partial class MealTutorialView : PopupPage
	{
		public MealTutorialView ()
		{
			InitializeComponent ();
		}

        private async void OK_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["mealviewedtutorial"] = "ok";
            await Application.Current.SavePropertiesAsync();
            await this.Navigation.RemovePopupPageAsync(this);

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            FadeIn();
        }
        private async void FadeIn()
        {
           await OK.FadeTo(0.5, 350, Easing.SpringIn);
            FadeOut();
        }
        private async void FadeOut()
        {
           await OK.FadeTo(0, 550, Easing.SpringIn);
            await Task.Delay(400);
            FadeIn();
        }
    }
}