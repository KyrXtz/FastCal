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
	public partial class MealTutorialView10 : PopupPage
	{
		public MealTutorialView10 ( )
		{
			InitializeComponent ();

            TutLabel.Text = "Tap on a Calendar Cell to see info about consumed nutrients of that day. Tap on the same Calendar Cell a second time to see a list of consumed food of said day. Alternatively swipe left or right to change the selected date.";
        }

      
        private void OnBackgroundClicked1()
        {
            TapGestureRecognizer_Tapped(null, null);

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Application.Current.Properties["mealviewedtutorial10"] = "ok";
            await Application.Current.SavePropertiesAsync();

            await this.Navigation.RemovePopupPageAsync(this);
        }
    }
}