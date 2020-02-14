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
	public partial class MealTutorialViewLast : PopupPage
	{
		public MealTutorialViewLast ( )
		{
			InitializeComponent ();

            TutLabel.Text = "And that's it! Create as many buttons as you like, and customize the app to fit your needs!\nNow set your goals in the settings menu if you haven't already, and check out the overview page for a complete presentation of your consumed foods.";
        }

      
        private void OnBackgroundClicked1()
        {
            TapGestureRecognizer_Tapped(null, null);

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Application.Current.Properties["mealviewedtutoriallast"] = "ok";
            await Application.Current.SavePropertiesAsync();

            await this.Navigation.RemovePopupPageAsync(this);
        }
    }
}