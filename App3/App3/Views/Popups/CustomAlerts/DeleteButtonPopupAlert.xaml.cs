using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views.Popups.CustomAlerts
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DeleteButtonPopupAlert : PopupPage
    {
        private FoodInfo infoPageref;
        public DeleteButtonPopupAlert (string message, FoodInfo infopage)
		{
            BindingContext = this;
            InitializeComponent();
            infoPageref = infopage;
            msg.Text = message;
              
        }

        
        private void Button_ClickedremovethisANDno(object sender, EventArgs e)
        {
            this.Navigation.RemovePopupPageAsync(this);
        }
        private async void Button_ClickedremovethisANDyes(object sender, EventArgs e)
        {

            try
            {
                infoPageref.DeleteConfirmed();
            }
            catch
            {

            }
           
            
          
            await this.Navigation.RemovePopupPageAsync(this);
        }
    }
}

