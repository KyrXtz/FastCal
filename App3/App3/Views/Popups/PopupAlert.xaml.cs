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
    public partial class PopupAlert : PopupPage
    {
        public static bool isYesNoActive = false;
        private OverviewView calledOw;
        public PopupAlert(string message, bool isOk = true, bool showNoButtons = false , OverviewView owView = null)
        {
            BindingContext = this;
            calledOw = owView;
            if (isYesNoActive)
            {
                return;
            }
            InitializeComponent();
            if (showNoButtons)
            {
                OKBTN.IsVisible = false;
                yesbtn.IsVisible = false;
                nobtn.IsVisible = false;

            }
            else
            {
                if (!isOk)
                {
                    OKBTN.IsVisible = false;
                    isYesNoActive = true;



                }
                else
                {

                    yesbtn.IsVisible = false;
                    nobtn.IsVisible = false;
                }
            }
            msg.Text = message;
        }

        private void Button_Clickedremovethis(object sender, EventArgs e)
        {
            this.Navigation.RemovePopupPageAsync(this);
        }
        private void Button_ClickedremovethisANDno(object sender, EventArgs e)
        {
            CreateSearch.loadispushed = true;
            isYesNoActive = false;
            this.Navigation.RemovePopupPageAsync(this);
        }
        private async void Button_ClickedremovethisANDyes(object sender, EventArgs e)
        {
            if (calledOw!=null)
            {
                calledOw.ProceedWithClearDay();
            }
            else
            {
                try
                {
                    CreateSearch.loadispushed = false;
                }
                catch
                {

                }
                try
                {
                    APIListView.loadispushed = false;
                }
                catch
                {

                }
                try
                {
                    CreateSearch.HttpClient.Dispose();
                }
                catch
                {

                }
                try
                {
                    await Navigation.RemovePopupPageAsync(CreateSearch.loadingPopup);
                }
                catch
                {

                }
                try
                {
                    await Navigation.RemovePopupPageAsync(APIListView.loadingPopup);
                }
                catch
                {

                }
            }
            isYesNoActive = false;
            await this.Navigation.RemovePopupPageAsync(this);
        }
    }
    
}