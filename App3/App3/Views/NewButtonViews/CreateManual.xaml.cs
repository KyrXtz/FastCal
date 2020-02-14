using App3.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateManual : ContentPage
    {
        public FoodModel.TimeOfDayEmum timeOfDay;
        public bool usercall = false;
        public string lastPortiong;
        public string fullPortionString;
        public CreateManual(FoodModel.TimeOfDayEmum timeofday)
        {
            timeOfDay = timeofday;

            InitializeComponent();
            lastPortiong = "100g";
            fullPortionString = "(100g)";
            List<string> portions = new List<string>();
            portions.Add("100g");       
            portions.Add("Set Manually..");
            FoodPortions.ItemsSource = portions;
            usercall = true;
            FoodPortions.SelectedIndex = 0;
            ManualPortion.IsVisible = false;

        }
        public async void ApplyShadowLocal()
        {
            await Task.Delay(1);
            var x = new List<RadButton>();
            x.Add(Createbtn);
            x.Add(Clearbtn);
            MainPage.ApplyShadowHack(x);
        }
        private void Button_ClickedClearSearch(object sender, EventArgs e)
        {

            ClearButtons();
            // viewModel.ClearNamePlc("search...");
        }
        private async void Button_ClickedCreate(object sender, EventArgs e)
        {
            var data = Application.Current.Properties;

            if (Name.Text == "" || Calories2.Text == "")
            {
                var alert = new PopupAlert("You must at least fill the Name and Calories fields.");
                await Navigation.PushPopupAsync(alert);
                return;
            }
            else
            {
                var loadingPage = new LoadingPopup();
                var btncreated = new PopupAlert("Button Created!");
                await Navigation.PushPopupAsync(loadingPage);

                var no = data["noOfFoods"] = (Convert.ToInt32(data["noOfFoods"]) + 1).ToString(); //classid
                string[] modArr = fullPortionString.Split('(');
                string modArrNumb = modArr[modArr.Count() - 1].Replace(')', ' ');
                double modArrFloat = Convert.ToDouble(modArrNumb.Replace('g', ' '));
                FoodModel foodtosave = new FoodModel(timeOfDay,                    
                   (Calories2.Text != "") ? Convert.ToDouble(Calories2.Text) : 0,
                    (CarbsPg.Text != "") ? Convert.ToDouble(CarbsPg.Text) : 0,
                    (FatPg.Text != "") ? Convert.ToDouble(FatPg.Text) : 0,
                    (ProteinPg.Text != "") ? Convert.ToDouble(ProteinPg.Text) : 0,
                    (MoreInfo.Text != "") ? MoreInfo.Text : "",
                    modArrFloat,
                    (Name.Text != "") ? Name.Text : "0",
                    Convert.ToInt32(no.ToString()),                    
                    0
                    );
                var foodJson = JsonConvert.SerializeObject(foodtosave);
                data.Add(foodtosave.ClassId.ToString() + "food", foodJson);
                await Application.Current.SavePropertiesAsync();
                


                await Navigation.PushPopupAsync(btncreated);
                await Navigation.RemovePopupPageAsync(loadingPage);
                ClearButtons();
                MealView.Invoker.CustomButtonCreatorInvoker(foodtosave);
            }
        }
        public void ClearButtons()

        {


            Name.Text = "";
            CarbsPg.Text = "";
            FatPg.Text = "";
            ProteinPg.Text = "";
            Calories2.Text = "";
            MoreInfo.Text = "";
            ManualPortion.Text = "";
            usercall = true;
            FoodPortions.SelectedIndex = 0;
            ManualPortion.IsVisible = false;


        }

        private void FoodPortions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (usercall)
            {
                usercall = false;
                return;
            }

            if (FoodPortions.Items[FoodPortions.SelectedIndex].ToString() == "Set Manually..")
            {
                ManualPortion.IsVisible = true;
                //lastPortiong
            }
            else
            {

                ManualPortion.IsVisible = false;

                var mod = FoodPortions.Items[FoodPortions.SelectedIndex].ToString();
                if (mod != lastPortiong) //mod/lastportion
                {
                    ModifyValues("(" + mod + ")");
                }

                lastPortiong = mod;
            }

        }
        private void ManualPortionChanged(object sender, EventArgs e)
        {
            var entry = sender as Entry;
            if (entry.Text != "")
            {
                var mod = "(" + entry.Text + "g)";
                ModifyValues(mod);
                lastPortiong = mod;
            }
        }

        private void ModifyValues(string mod)
        {
            fullPortionString = mod;
            string[] modArr = mod.Split('(');
            string modArrNumb = modArr[modArr.Count() - 1].Replace(')', ' ');
            double modArrFloat = Convert.ToDouble(modArrNumb.Replace('g', ' '));

            string[] lastPortiongArr = lastPortiong.Split('(');
            string lastPortiongArrNumb = lastPortiongArr[lastPortiongArr.Count() - 1].Replace(')', ' ');
            double lastPortiongArrFloat = Convert.ToDouble(lastPortiongArrNumb.Replace('g', ' '));


            double modifier = modArrFloat / lastPortiongArrFloat;
            if (CarbsPg.Text!="")
            CarbsPg.Text = ((Convert.ToDouble(CarbsPg.Text) * modifier).ToString());
            if (FatPg.Text != "")
                FatPg.Text = ((Convert.ToDouble(FatPg.Text) * modifier).ToString());
            if (ProteinPg.Text != "")
                ProteinPg.Text = ((Convert.ToDouble(ProteinPg.Text) * modifier).ToString());
            if (Calories2.Text != "")
                Calories2.Text = ((Convert.ToDouble(Calories2.Text) * modifier).ToString());

            if (MoreInfo.Text.Contains("Sugars:"))
            {
                try
                {
                    var x = MoreInfo.Text.Split(':');
                    string yy = "";
                    int i = 0;
                    foreach (var yo in x)
                    {
                        if (yo.Contains("g") && (!yo.Contains("Sugars")))
                        {
                            yy = x[i];
                            break;
                        }
                        i += 1;
                    }
                    var y = yy.Split('g');

                    MoreInfo.Text = "Sugars:" + ((Convert.ToDouble(y[0]) * modifier).ToString()) + "g";
                }
                catch
                {
                    lastPortiong = mod;
                }
            }


      

        }


    }
}