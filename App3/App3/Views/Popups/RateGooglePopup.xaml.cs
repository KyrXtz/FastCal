using App3.Interfaces;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RateGooglePopup : PopupPage
    {
        public RateGooglePopup(string message)
        {
            BindingContext = this;
            
            InitializeComponent();
          
                
            
            msg.Text = message;
        }

        private async void Button_ClickedremovethisANDRemind(object sender, EventArgs e)
        {
            var data = Xamarin.Forms.Application.Current.Properties;

            data["hasRatedCounter"] = "0";
            await Application.Current.SavePropertiesAsync();
            await  this.Navigation.RemovePopupPageAsync(this);
        }
        private async void Button_ClickedremovethisANDno(object sender, EventArgs e)
        {
            var data = Xamarin.Forms.Application.Current.Properties;

            data["hasRatedCounter"] = "0";
            data["hasRated"] = "true";
            await Application.Current.SavePropertiesAsync();
            await this.Navigation.RemovePopupPageAsync(this);

        }
        private async void Button_ClickedremovethisANDyes(object sender, EventArgs e)
        {

            IAppRating appRater = DependencyService.Get<IAppRating>();
            appRater.RateApp();
            var data = Xamarin.Forms.Application.Current.Properties;

            data["hasRatedCounter"] = "0";
            data["hasRated"] = "true";
            await this.Navigation.RemovePopupPageAsync(this);
        }
       
    }
    
}