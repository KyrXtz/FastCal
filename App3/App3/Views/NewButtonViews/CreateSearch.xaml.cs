using App3.Models;
using App3.Views.Tutorials;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateSearch : ContentPage
    {
        public FoodModel.TimeOfDayEmum timeOfDay;
        public bool usercall = false;
        public string lastPortiong;
        public bool justsearched = false;
        public bool activeSearch = true;
        public string fullPortionString;
        public static CreateSearch Invoker;
        public CreateSearch(FoodModel.TimeOfDayEmum timeofday)
        {
            Invoker = this;
            InitializeComponent();
            timeOfDay = timeofday;
            lastPortiong = "100g";
            fullPortionString = "(100g)";
            List<string> portions = new List<string>();
            portions.Add("100g");
            portions.Add("Set Manually..");
            FoodPortions.ItemsSource = portions;
            usercall = true;
            FoodPortions.SelectedIndex = 0;
            ManualPortion.IsVisible = false;

            FoodPortions.IsVisible = false;
            Calories2.IsVisible = false;
            ProteinPg.IsVisible = false;
            FatPg.IsVisible = false;
            CarbsPg.IsVisible = false;
            MoreInfo.IsVisible = false;
            CreateBtn.IsVisible = false;

            CaloriesLabel.IsVisible = false;
            ProteinLabel.IsVisible = false;
            FatLabel.IsVisible = false;
            CarbsLabel.IsVisible = false;
            MoreInfoLabel.IsVisible = false;
            FoodPortionLabel.IsVisible = false;
         


        }
        public async void ApplyShadowLocal()
        {
            await Task.Delay(1);
            var x = new List<RadButton>();
            x.Add(CreateBtn);
            x.Add(Clearbtn);
            MainPage.ApplyShadowHack(x);
        }
        public async void CheckTutorial6()
        {
            if (Application.Current.Properties["mealviewedtutorial6"].ToString() != "ok" && Application.Current.Properties["mealviewedtutorial5"].ToString() == "ok")
            {
                await Task.Delay(200);
                var page2 = new MealTutorialView6();
                await Navigation.PushPopupAsync(page2);
            }
        }
        public async void CheckTutorial7()
        {
            if (Application.Current.Properties["mealviewedtutorial7"].ToString() != "ok" && Application.Current.Properties["mealviewedtutorial6"].ToString() == "ok")
            {
                await Task.Delay(200);
                var page2 = new MealTutorialView7();
                await Navigation.PushPopupAsync(page2);
            }
        }
        private void Button_ClickedClearSearch(object sender, EventArgs e)
        {
            activeSearch = true;
            justsearched = false;
            ClearButtons();
            // viewModel.ClearNamePlc("search...");
        }
        bool checkdelay = true;
        public PopupAlert globalAlert = null;
        public static HttpClient HttpClient = null;
        public static LoadingPopup loadingPopup = null;
        public static bool loadispushed = false;
        private async void Delayer()
        {
            do
            {
                await Task.Delay(3800);
                if (checkdelay && loadispushed)
                {
                    if (PopupAlert.isYesNoActive)
                    {
                        continue;
                    }
                    globalAlert = new PopupAlert("This operation is taking too long.....\nCancel?", false);
                   
                    await Navigation.PushPopupAsync(globalAlert);

                }
            } while (loadispushed);
           

           
            // viewModel.ClearNamePlc("search...");
        }
        private async void SearchboxUnFocused(object sender, FocusEventArgs e)
        {
            checkdelay = true;
            if (activeSearch)
            {
                if (Name.Text.Length >= 3 && !justsearched)
                {
                    await Task.Run(() => { Delayer(); });
                    
                    justsearched = true;
                    //viewModel.ClearNamePlc("");
                    loadingPopup = new LoadingPopup();
                    await Navigation.PushPopupAsync(loadingPopup);
                    loadispushed = true;
                    //await Navigation.RemovePopupPageAsync(loadingPage);
                    string URL2 = "https://api.nal.usda.gov/fdc/v1/search?api_key=aurg5ejFKMIpBq79PlHAkkCRCfEKt6s1hYnNKwg8";

                    var searchinput = Name.Text;
                    var values = new Dictionary<string, string>
            {

            { "generalSearchInput", searchinput },
             { "pageNumber", "1" },
                        { "requireAllWords","true"},
                        {"sortField","score" }
            };
                    HttpClient HttpClient = new HttpClient();
                    // client2.BaseAddress = new Uri(URL);

                    // HttpContent content = new FormUrlEncodedContent(values);
                    //      HttpContent content = new En(values);
                    var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(values));
                    var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");


                    //var yo =  content.GetType();
                    HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (CheckForInternetConnection())
                    {
                            var response2 = await HttpClient.PostAsync(URL2, content);

                        if (response2.IsSuccessStatusCode)
                        {
                            var responseString = await response2.Content.ReadAsStringAsync();

                            //  var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                            // var json = Newtonsoft.Json.JsonConvert.SerializeObject(responseString, Newtonsoft.Json.Formatting.Indented);
                            var parsedObject = JObject.Parse(responseString);
                            // var properties = JsonConvert.DeserializeObject<IEnumerable<APIHitsAndPages>>(parsedObject.ToString());

                            var json2 = parsedObject["foods"].ToString();
                            var totalPages = Convert.ToInt32(parsedObject["totalPages"].ToString());
                            var currentPage = Convert.ToInt32(parsedObject["currentPage"].ToString());

                            //Debug.WriteLine(json);
                            HttpClient.Dispose();
                            checkdelay = false;
                            try
                            {
                                await Navigation.RemovePopupPageAsync(globalAlert);
                                loadispushed = false;

                            }
                            catch
                            {
                                loadispushed = false;
                            }

                            
                            // var yo = json2.Count(); 
                            if (json2.Count() == 2) //[] -> 2 chars
                            {

                                justsearched = false;
                                //                                viewModel.ClearNamePlc("search...");
                                var alert = new PopupAlert("No Entries found.");
                                try
                                {
                                    await Navigation.RemovePopupPageAsync(loadingPopup);
                                    loadispushed = false;
                                }
                                catch
                                {
                                    loadispushed = false;
                                }
                                await Navigation.PushPopupAsync(alert);
                                return;
                            }
                            
                            var loadingPage2 = new APIListView(json2, this, loadingPopup, searchinput, totalPages, currentPage);
                           // await Navigation.PopAllPopupAsync();
                            await Navigation.PushPopupAsync(loadingPage2);

                        }
                    }
                    else
                    {
                        justsearched = false;
                      //  viewModel.ClearNamePlc("search...");
                        await DisplayAlert("Error", "Could not reach database.\nCheck your internet conenction.", "OK");
                        await Navigation.PopAllPopupAsync();
                    }
                }
            }
        }


        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        private async void Button_ClickedCreate(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopup();
            var data = Application.Current.Properties;

            if (Name.Text == "" || Calories2.Text == "")
            {
                var alert = new PopupAlert("You must at least fill the Name and Calories fields.");
                await Navigation.PushPopupAsync(alert);
                return;
            }
            else
            {
                //var loadingPage = new LoadingPopup();
                
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





                
                await Navigation.RemovePopupPageAsync(loadingPage);
                ClearButtons();
                MealView.Invoker.CustomButtonCreatorInvoker(foodtosave);
                await Task.Delay(100);
                PopupPage btncreated = new PopupAlert("Button Created!");
                
                await Navigation.PushPopupAsync(btncreated);

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
            FoodPortions.IsVisible = false;
            Calories2.IsVisible = false;
            ProteinPg.IsVisible = false;
            FatPg.IsVisible = false;
            CarbsPg.IsVisible = false;
            MoreInfo.IsVisible = false;
            CreateBtn.IsVisible = false;
            CaloriesLabel.IsVisible = false;
            ProteinLabel.IsVisible = false;
            FatLabel.IsVisible = false;
            CarbsLabel.IsVisible = false;
            MoreInfoLabel.IsVisible = false;
            FoodPortionLabel.IsVisible = false;
            ManualPortion.IsVisible = false;



        }
        private void FoodPortions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (usercall)
            {
                usercall = false;
                return;
            }
            if (FoodPortions.Items[FoodPortions.SelectedIndex].ToString() != "-1" && FoodPortions.Items[FoodPortions.SelectedIndex].ToString() != "Set Manually..")
            {
                CheckTutorial7();
                Calories2.IsVisible = true;
                ProteinPg.IsVisible = true;
                FatPg.IsVisible = true;
                CarbsPg.IsVisible = true;
                MoreInfo.IsVisible = true;
                CreateBtn.IsVisible = true;

                CaloriesLabel.IsVisible = true;
                ProteinLabel.IsVisible = true;
                FatLabel.IsVisible = true;
                CarbsLabel.IsVisible = true;
                MoreInfoLabel.IsVisible = true;
                ApplyShadowLocal();
            }
            if (FoodPortions.Items[FoodPortions.SelectedIndex].ToString() == "Set Manually..")
            {
                ManualPortion.IsVisible = true;
                ManualPortion.Text = "";
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
            if (Calories2.Text!="" && Calories2.Text!=null)
            {
                CheckTutorial7();
                Calories2.IsVisible = true;
                ProteinPg.IsVisible = true;
                FatPg.IsVisible = true;
                CarbsPg.IsVisible = true;
                MoreInfo.IsVisible = true;
                CreateBtn.IsVisible = true;
                
                CaloriesLabel.IsVisible = true;
                ProteinLabel.IsVisible = true;
                FatLabel.IsVisible = true;
                CarbsLabel.IsVisible = true;
                MoreInfoLabel.IsVisible = true;
                ApplyShadowLocal();
            }
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
            if (CarbsPg.Text != "")
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
        public void SetButtons(LocalFoodModel foodtosave)
        {
            lastPortiong = "(100g)";
            Name.Text = Name.Text; //+" ("+ foodtosave.Name + " g)"
            CarbsPg.Text = String.Format("{0:C2}", foodtosave.Carb);
            FatPg.Text = String.Format("{0:C2}", foodtosave.Fat);
            ProteinPg.Text = String.Format("{0:C2}", foodtosave.Prot);
            if (foodtosave.MoreInfo != null)
                MoreInfo.Text = "Sugars:" + foodtosave.MoreInfo + "g";
            Calories2.Text = String.Format("{0:C2}", foodtosave.Cal);
            List<string> portions = new List<string>();
            portions.Add("(100g)");
            foreach (var x in foodtosave.gramsEnum)
            {
                portions.Add(x.portionDescription + " (" + x.gramWeight + "g)");
            }

            portions.Add("Set Manually..");
            usercall = true;
            FoodPortions.ItemsSource = portions;
           

            FoodPortions.IsVisible = true;
            FoodPortionLabel.IsVisible = true;

            FoodPortions.SelectedIndex = -1;
            //CarbsPg.Keyboard = Keyboard.Text;
            // CarbsPg.Text = 1.ToString();
            //// CarbsMg.Keyboard = Keyboard.Numeric;
            // FatPg.Text = 2.ToString();
            // ProteinPg.Text = 2.ToString();
            // MoreInfo.Text = 3.ToString();
            // Calories2.Text = 4.ToString();
            CheckTutorial6();
        }

        //protected override bool OnBackButtonPressed()
        //{



        //    var data = Application.Current.Properties;

        //    if (data["mealviewedtutorial9"].ToString() != "ok" && data["mealviewedtutorial8"].ToString() == "ok")
        //    {
        //        MealView.Invoker.StartTut8();
        //    }
        //    return false;

        //}
    }
}