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
	public partial class MealTutorialView1 : PopupPage
	{
		public MealTutorialView1 (int row,int col)
		{
			InitializeComponent ();
            OK.SetValue(Grid.RowProperty,row);
            OK.SetValue(Grid.ColumnProperty, col);
            if ((int)OK.GetValue(Grid.RowProperty) == 1)
            {
                OK.Margin = new Thickness(0,15,0,0);
            }
            if ((int)OK.GetValue(Grid.RowProperty) == 2)
            {
                OK.Margin = new Thickness(0, 8, 0, 0);
            }
        }

        private async void OK_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["mealviewedtutorial1"] = "ok";
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
           await OK.FadeTo(0.5, 750, Easing.SpringIn);
            await Task.Delay(400);
            FadeOut();
        }
        private async void FadeOut()
        {
           await OK.FadeTo(0, 450, Easing.SpringIn);
            FadeIn();
        }
    }
}