using App3.CustomRenderers;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage
    {
        private int fatmod = 30;
        private int carbmod = 45;
        private int protmod = 25;
        private double slidervalueforsave = 0;
        private string titleLabel = "Balanced";
        private string infoLabel = "The most basic distribution of nutrients, followed by most people and suggested by the AMDR.";

        public SettingsView()
        {
            var data = Application.Current.Properties;

            InitializeComponent();
            var exists = data["goalset"].ToString();
            if (exists == "true")
            {
                var cal = data["goalcal"].ToString();
                var carb = data["goalcarb"].ToString();
                var fat = data["goalprot"].ToString();
                var prot = data["goalfat"].ToString();
                slidervalueforsave = Convert.ToDouble(data["goalslidervalue"].ToString());
                slider.Value = slidervalueforsave;

                CarbsMg.Text = carb;
                ProteinMg.Text = prot;
                FatMg.Text = fat;
                Calories.Text = cal;
                Calories_TextChanged(null, null);//ginetai apo dw update twn timwn
                GoalGrid.IsVisible = true;
                GoalButton.IsVisible = false;
                TitleLabel.IsVisible = true;
                InfoLabel.IsVisible = true;
                AdjustLabel.IsVisible = true;
                AdjustSlider.IsVisible = true;
                GoalLabel.Text = "Goals";


            }
            else
            {
                GoalGrid.IsVisible = false;
                GoalButton.IsVisible = true;
                AdjustLabel.IsVisible = false;
                AdjustSlider.IsVisible = false;
                TitleLabel.IsVisible = false;
                InfoLabel.IsVisible = false;
                GoalLabel.Text = "Calculate Recommended Caloric Intake";
                // RadButton_ClickedCalculate(null, null);
            }

            ApplyShadowsLocal();
            //GoalLabel.Effects.Add(new ShadowEffect
            //{
            //    Radius = 5,
            //    Color = Color.Black,
            //    DistanceX = 5,
            //    DistanceY = 5
            //});
            //GoalButton.Effects.Add(new ShadowEffect
            //{
            //    Radius = 5,
            //    Color = Color.Black,
            //    DistanceX = 5,
            //    DistanceY = 5
            //});

        }
        private void ApplyShadowsLocal()
        {
            var x = new List<RadButton>();
            x.Add(Goalbtn);
            x.Add(Savebtn);
            x.Add(Clearbtn);
            MainPage.ApplyShadowHack(x);
        }
        private void Button_ClickedClear(object sender, EventArgs e)
        {
            CarbsMg.Text = "";
            ProteinMg.Text = "";
            FatMg.Text = "";
            Calories.Text = "";
            Calories_TextChanged(null, null);//ginetai apo dw update twn timwn
            GoalGrid.IsVisible = false;
            GoalButton.IsVisible = true;
            AdjustLabel.IsVisible = false;
            AdjustSlider.IsVisible = false;
            TitleLabel.IsVisible = false;
            InfoLabel.IsVisible = false;
            GoalLabel.Text = "Calculate Recommended Caloric Intake";

        }
        private async void Button_ClickedSave(object sender, EventArgs e)
        {

            if (Calories.Text == "")
            {
                return;
            }
            if (!Double.TryParse(Calories.Text, out double cal))
            {
                return;
            }
            if (!Double.TryParse(FatMg.Text, out double fat))
            {
                return;
            }
            if (!Double.TryParse(CarbsMg.Text, out double carbs))
            {
                return;
            }
            if (!Double.TryParse(ProteinMg.Text, out double prot))
            {
                return;
            }
            var data = Application.Current.Properties;

            data["goalcal"] = cal;
            data["goalcarb"] = carbs;
            data["goalprot"] = prot;
            data["goalfat"] = fat;
            data["goalslidervalue"] = slidervalueforsave;

            data["goalset"] = "true";

            await Application.Current.SavePropertiesAsync();
            var alert = new PopupAlert("Goals saved.");
            await Navigation.PushPopupAsync(alert);
            return;
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double StepValue = 1.0;
            var newStep = Math.Round(e.NewValue / StepValue);

            slider.Value = newStep * StepValue;
            switch (slider.Value)
            {
                case 0:
                    titleLabel = "Balanced";
                    infoLabel = "The most basic distribution of nutrients, followed by most people and suggested by the AMDR.";
                    fatmod = 30;
                    carbmod = 45;
                    protmod = 25;
                    break;
                case 1.0:
                    titleLabel = "High Protein-Medium Carbs";
                    infoLabel = "A distribution aimed towards those following strength training programs, rathen than bulking.";
                    fatmod = 30;
                    carbmod = 35;
                    protmod = 35;
                    break;
                case 2.0:
                    titleLabel = "High Carbs-Medium Protein";
                    infoLabel = "A distribution aimed towards those looking to bulk up.";
                    fatmod = 30;
                    carbmod = 50;
                    protmod = 20;
                    break;
                case 3.0:
                    titleLabel = "Weight Loss";
                    infoLabel = "Low fat - High protein distribution , aimed towards losing weight and burning fat.\nNote: when aiming to lose weight what matters most is burning more calories than you consume.";
                    fatmod = 25;
                    carbmod = 45;
                    protmod = 30;
                    break;
                case 4.0:
                    titleLabel = "Keto Diet";
                    infoLabel = "Special diet with a High fat - Low carb distribution.\nNote: Consult your doctor before you try this diet.";
                    fatmod = 65;
                    carbmod = 10;
                    protmod = 25;
                    break;
                case 5.0:
                    titleLabel = "Paleo Diet";
                    infoLabel = "Similar to Keto Diet but less restrictive.\nNote: Most processed foods are banned in this diet.";
                    fatmod = 40;
                    carbmod = 30;
                    protmod = 30;
                    break;


            }
            slidervalueforsave = slider.Value;
            Calories_TextChanged(null, null);//ginetai apo dw update twn timwn


        }

        private void Calories_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Calories.Text == "")
            {
                return;
            }
            if (!Double.TryParse(Calories.Text, out double cal))
            {
                return;
            }

            GoalGrid.IsVisible = true;
            GoalButton.IsVisible = false;
            AdjustLabel.IsVisible = true;
            AdjustSlider.IsVisible = true;
            TitleLabel.IsVisible = true;
            InfoLabel.IsVisible = true;
            GoalLabel.Text = "Goals";

            var fat = cal * fatmod / 900;
            FatMg.Text = fat.ToString("F2");
            var carb = cal * carbmod / 400;
            CarbsMg.Text = carb.ToString("F2");
            var prot = cal * protmod / 400;
            ProteinMg.Text = prot.ToString("F2");
            TitleLabel.Text = titleLabel;
            InfoLabel.Text = infoLabel + "\nCarbs: " + carbmod + "%, Protein: " + protmod + "%, Fat:" + fatmod + "%.";
            //ApplyShadowsLocal();


        }

        private async void RadButton_ClickedCalculate(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopup();
            await Navigation.PushPopupAsync(loadingPage);
            string myinput = await AddCustomButton(this.Navigation, loadingPage);
            
        }
        public Task<string> AddCustomButton(INavigation navigation, LoadingPopup loadingPage)
        {

            //apo edw kai katw gia na ginei to return
            var tcs = new TaskCompletionSource<string>();
            var page = new PopupPage();



            var btnOk = new RadButton
            {
                Text = "Done",
                WidthRequest = 100,
                BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8),
            };

            var slButtons = new StackLayout
            {

                Children = { btnOk },
                //Children = { btnOk, btnCancel },
            };
            //apo edw kai panw gia na ginei to return







            //var layout = new StackLayout
            //{

            //    VerticalOptions = LayoutOptions.CenterAndExpand,
            //    HorizontalOptions = LayoutOptions.CenterAndExpand,
            //    Orientation = StackOrientation.Vertical,
            //    //Children = { lblTitle, lblMessage, carview, slButtons },
            //    Children = {  carview },

            //};

            // create and show page
            //var page = new ContentPage();
            //page.Content = layout;

            var carview = new CalcCal();
            //carview.BackgroundColor = Color.FromHex("#c0cca7") ;
            page.BackgroundColor = Color.NavajoWhite;
            carview.MealFrame.BackgroundColor = Color.NavajoWhite;
            var layout = new StackLayout
            {
                Children = { carview.Content, slButtons }
            };
            page.Content = layout;
            // page.BackgroundColor = Color.FromHex("#c0cca7");
            page.BackgroundColor = Color.NavajoWhite;

            btnOk.BackgroundColor = Color.DarkTurquoise;
            navigation.RemovePopupPageAsync(loadingPage);
            navigation.PushPopupAsync(page);
            btnOk.Clicked += async (s, e) =>
            {
                // close page
                if (carview.SliderGoalCalories.Text == "" || carview.SliderGoalCalories.Text == null) {
                    string myinput = await MainPage.CustomAlert(this.Navigation, "Goal Calories not calculated.\nCancel?");
                    if (myinput == "yes")
                    {

                    }
                    if (myinput == "no")
                    {
                        return;
                    }
                }
                if (carview.SliderGoalCalories.Text != "" && carview.SliderGoalCalories.Text != null)
                {
                    var cal = Convert.ToDouble(carview.SliderGoalCalories.Text.Replace(" Calories.", ""));
                    Calories.Text = cal.ToString();
                    Calories_TextChanged(null, null);//ginetai apo dw update twn timwn
                }
                await navigation.RemovePopupPageAsync(page);
                ApplyShadowsLocal();
                // pass result
                tcs.SetResult("ok");
            };
            // open keyboard
            //txtInput.Focus();

            // code is waiting her, until result is passed with tcs.SetResult() in btn-Clicked
            // then proc returns the result
            return tcs.Task;
        }
       
    }
}