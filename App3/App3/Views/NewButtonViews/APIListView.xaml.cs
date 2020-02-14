using App3.Models;
using App3.Views.Tutorials;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.DataControls;
using Telerik.XamarinForms.DataControls.ListView;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class APIListView : PopupPage
    {
        private string _thisPage { get; set; }
        public string thisPage
        {
            get
            {
                return _thisPage;
            }
            set
            {
                _thisPage = value;
                OnPropertyChanged();
            }
        }
        public Page AboutPageRef;
        public string searchInput;
        public int pageNo = 1;
        public int totalPages;
        
        IEnumerable<APIPostFoodModel> foodList { get; set; }
        public APIListView(string json, Page aboutPage, PopupPage loadingPage, string _searchInput, int _totalPages, int currentPage)
        {


            InitializeComponent();
            BindingContext = this;
            searchInput = _searchInput;
            pageNo = currentPage;
            totalPages = _totalPages;
            thisPage = pageNo + "/" + totalPages;
            Navigation.RemovePopupPageAsync(loadingPage);
            AboutPageRef = aboutPage;
            foodList = JsonConvert.DeserializeObject<IEnumerable<APIPostFoodModel>>(json);
            foreach (var x in foodList)
            {
                //maybe too much
                //var fooddatastring= GetMethod(x.fdcId.ToString()).Result;
                // var foodata = JsonConvert.DeserializeObject<IEnumerable<APIGetFoodModel>>(fooddatastring);

                // LocalFoodModel foodtoshow = new LocalFoodModel();
                // int i = 0;
                // foreach (var nu in foodata)
                // {
                //     if (nu.nutrient.name == "Protein" && nu.nutrient.unitName == "g")
                //     {
                //         foodtoshow.Prot = nu.amount;
                //         i += 1;
                //     }
                //     if (nu.nutrient.name == "Total lipid (fat)" && nu.nutrient.unitName == "g")
                //     {
                //         foodtoshow.Fat = nu.amount;

                //         i += 1;

                //     }
                //     if (nu.nutrient.name == "Carbohydrate, by difference" && nu.nutrient.unitName == "g")
                //     {
                //         foodtoshow.Carb = nu.amount;

                //         i += 1;
                //     }
                //     if (nu.nutrient.name == "Energy" && nu.nutrient.unitName == "kcal")
                //     {
                //         foodtoshow.Cal = nu.amount;

                //         i += 1;
                //     }
                //     if (nu.nutrient.name == "Sugars, total including NLEA" && nu.nutrient.unitName == "g")
                //     {
                //         foodtoshow.MoreInfo = nu.amount;

                //         i += 1;
                //     }
                //     if (i == 5)
                //     {
                //         break;
                //     }

                // }
                //maybe too much
                if (x.dataType == "Branded" && x.brandOwner != null)
                {
                    //x.description = x.description + "\nBrand: " + x.brandOwner +"\nCal:" + foodtoshow.Cal +", Carbs:" + foodtoshow.Carb + ", of which sugar:" + foodtoshow.MoreInfo + ", Protein:" + foodtoshow.Prot + ", Fat:" + foodtoshow.Fat ;
                    x.description = x.description + "\nBrand: " + x.brandOwner;
                }
            }
            // var listView = new RadListView();
            //listView.ItemsSource = foodList;
            //listViewFood= new RadListView();
            listViewFood.SelectionGesture = Telerik.XamarinForms.DataControls.ListView.SelectionGesture.Tap;

            listViewFood.SelectionMode = Telerik.XamarinForms.DataControls.ListView.SelectionMode.None;
            //listViewFood.SortDescriptors.Add(new Telerik.XamarinForms.DataControls.ListView.PropertySortDescriptor { PropertyName = "score", SortOrder = Telerik.XamarinForms.Common.SortOrder.Ascending });


            listViewFood.ItemsSource = foodList;
            CheckTutorial5();
            //ApplyShadowLocal();
        }
        public async void ApplyShadowLocal()
        {
            await Task.Delay(50);
            var x = new List<RadButton>();
            x.Add(Prev);
            x.Add(Next);
            MainPage.ApplyShadowHack(x);
        }
        private async void CheckTutorial5()
        {
            if (Application.Current.Properties["mealviewedtutorial5"].ToString() != "ok" && Application.Current.Properties["mealviewedtutorial4"].ToString() == "ok")
            {
                await Task.Delay(200);
                var page = new MealTutorialView5();
                await Navigation.PushPopupAsync(page);
            }
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
        private async Task<string> GetMethod(string fdcId)
        {
         

            var parsedObject = await APIGET(fdcId);
            if (parsedObject == null)
            {
                return null;
            }

            var json2 = parsedObject["foodNutrients"].ToString();

            return json2;
        }
        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }
        private async void CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        private async void SelectedItem(object sender, EventArgs e)
        {

            loadingPopup = new LoadingPopup();
            await Navigation.PushPopupAsync(loadingPopup);
            loadispushed = true;


            var tapped_label = sender as StackLayout;
            tapped_label.BackgroundColor = Color.Orange;
            var fcdId = tapped_label.ClassId;
            var parsedObject = await APIGET(fcdId);
            if (parsedObject == null)
            {
                return;
            }

            var json2 = parsedObject["foodNutrients"].ToString();

            var foodNutrients = JsonConvert.DeserializeObject<IEnumerable<APIGetFoodModel>>(json2);
            var foodportion = parsedObject["foodPortions"].ToString();
            var portionUnits = JsonConvert.DeserializeObject<IEnumerable<APIgramWeightModel>>(foodportion);


            LocalFoodModel foodtosave = new LocalFoodModel();
            int i = 0;
            foreach (var nu in foodNutrients)
            {
                if (nu.nutrient.name == "Protein" && nu.nutrient.unitName == "g")
                {
                    foodtosave.Prot = nu.amount;
                    i += 1;
                }
                if (nu.nutrient.name == "Total lipid (fat)" && nu.nutrient.unitName == "g")
                {
                    foodtosave.Fat = nu.amount;

                    i += 1;

                }
                if (nu.nutrient.name == "Carbohydrate, by difference" && nu.nutrient.unitName == "g")
                {
                    foodtosave.Carb = nu.amount;

                    i += 1;
                }
                if (nu.nutrient.name == "Energy" && nu.nutrient.unitName == "kcal")
                {
                    foodtosave.Cal = nu.amount;

                    i += 1;
                }
                if (nu.nutrient.name == "Sugars, total including NLEA" && nu.nutrient.unitName == "g")
                {
                    foodtosave.MoreInfo = nu.amount;

                    i += 1;
                }
                if (i == 5)
                {
                    break;
                }

            }
            // foodtosave.Name = foodNutrients.First().gramWeight; // to add grams to name of food
            foodtosave.gramsEnum = portionUnits;
            var x = new FoodInfoModel();
            x.FoodCal = String.Format("{0:C2}", foodtosave.Cal);
            x.FoodCarb = String.Format("{0:C2}", foodtosave.Carb);
            x.FoodProt = String.Format("{0:C2}", foodtosave.Prot);
            x.FoodFat = String.Format("{0:C2}", foodtosave.Fat);
            x.MoreInfo = foodtosave.MoreInfo;
            x.IsCustomButton = "100g\n\nnote: you can change\nthe portion later.";
            var view = new FoodInfo(x,null,0,true);
            //var x = new PopupAlert("is this ok?");
            //await Navigation.PushPopupAsync(x, true);
            string myinput = await IsOk(this.Navigation,view, loadingPopup);

            //pageref.ClearButtons(false);
            //pageref.SetButtons(foodtosave);
            if (myinput == "Ok")
            {
                CreateSearch.Invoker.SetButtons(foodtosave);
               // await Navigation.RemovePopupPageAsync(loadingPage);

                await Navigation.RemovePopupPageAsync(this);
            }
            else
            {
                tapped_label.BackgroundColor = Color.Default;
                //await Navigation.RemovePopupPageAsync(loadingPage);

            }

            //var your_viewcell = tapped_label.Parent as ViewCell;
            //var your_binded_object = your_viewcell.BindingContext;
        }
        public Task<string> IsOk(INavigation navigation , FoodInfo foodinfoview, PopupPage loadingpage)
        {
            // wait in this proc, until user did his input 
            var tcs = new TaskCompletionSource<string>();




            var btnCancel = new RadButton
            {
                Text = "Cancel",
                WidthRequest = 100,
                BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8)
            };
           

            var btnOk = new RadButton
            {
                Text = "Ok",
                WidthRequest = 100,
                BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8)
            };
           
            var slButtons = new StackLayout
            {

                Children = { btnOk, btnCancel },
            };
            
            var layout = new StackLayout
            {

                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { foodinfoview.Content, slButtons },
                //Children = { carview },

            };

            //create and show page
            var page = new PopupPage();
            page.Content = layout;

            btnCancel.Clicked += async (s, e) =>
            {
                // close page
                await navigation.RemovePopupPageAsync(page);
                // pass empty result
                tcs.SetResult(null);
            };
            btnOk.Clicked += async (s, e) =>
            {
                // close page
                await navigation.RemovePopupPageAsync(page);
                // pass empty result
                tcs.SetResult("Ok");
            };


            // create and show page

            navigation.PushPopupAsync(page);
            navigation.RemovePopupPageAsync(loadingpage);

            // open keyboard


            // code is waiting her, until result is passed with tcs.SetResult() in btn-Clicked
            // then proc returns the result
            return tcs.Task;
        }
        private async Task<JObject> APIGET(string fcdId)
        {
            checkdelay = true;
            await Task.Run(() => { Delayer(); });
            string URL = "https://api.nal.usda.gov/fdc/v1/";
            string urlParameters = fcdId + "?api_key=aurg5ejFKMIpBq79PlHAkkCRCfEKt6s1hYnNKwg8";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            //HttpResponseMessage response = client.PostAsync(URL);
            // List data response.
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                // Blocking call! Program will wait here until a response is received or a timeout occurs.
                response = await client.GetAsync(urlParameters);
            }
            catch
            {
                checkdelay = false;
                try
                {
                    await Navigation.RemovePopupPageAsync(loadingPopup);

                    
                }
                catch
                {

                }
                return null;
            }
            if (response.IsSuccessStatusCode)
            {
                //var json =  Newtonsoft.Json.JsonConvert.SerializeObject(response.Content, Newtonsoft.Json.Formatting.Indented);
                //Debug.WriteLine(json);
                //var yo = response.Content.ReadAsStringAsync();
                // Parse the response body.
                var responseString = await response.Content.ReadAsStringAsync();
                checkdelay = false;
                try
                {
                    loadispushed = false;

                }
                catch
                {
                    loadispushed = false;
                }
                // var json = Newtonsoft.Json.JsonConvert.SerializeObject(x, Newtonsoft.Json.Formatting.Indented);
                var parsedObject = JObject.Parse(responseString);
                return parsedObject;
                //  var json2 = parsedObject["foodNutrients"].ToString();
                //  foodNutrients = JsonConvert.DeserializeObject<IEnumerable<APIGetFoodModel>>(json2);
                //var  foodportion = parsedObject["foodPortions"].ToString();
                //  var portionUnits = JsonConvert.DeserializeObject<IEnumerable<APIgramWeightModel>>(foodportion);
                //  return foodNutrients;
                //Debug.WriteLine(json2);

                //var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                //foreach (var d in dataObjects)
                //{
                //    Console.WriteLine("{0}", d.Name);
                //}
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }
            //client.Dispose();
        }
        private async Task<JObject> APIPOST(string _searchInput, int _pageno)
        {




            //await Navigation.RemovePopupPageAsync(loadingPage);
            checkdelay = true;
            await Task.Run(() => { Delayer(); });
            string URL2 = "https://api.nal.usda.gov/fdc/v1/search?api_key=aurg5ejFKMIpBq79PlHAkkCRCfEKt6s1hYnNKwg8";

            var searchinput = _searchInput;
            var pageno = _pageno;
            var values = new Dictionary<string, string>
            {

            { "generalSearchInput", searchinput },
             { "pageNumber", pageno.ToString() }
            };
            HttpClient client2 = new HttpClient();
            // client2.BaseAddress = new Uri(URL);

            // HttpContent content = new FormUrlEncodedContent(values);
            //      HttpContent content = new En(values);
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(values));
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");


            //var yo =  content.GetType();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (CheckForInternetConnection())
            {
                var response2 = await client2.PostAsync(URL2, content);
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
                if (response2.IsSuccessStatusCode)
                {
                    var responseString = await response2.Content.ReadAsStringAsync();

                    //  var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(responseString, Newtonsoft.Json.Formatting.Indented);
                    return JObject.Parse(responseString);
                    // var parsedObject = JObject.Parse(responseString);
                    //var json2 = parsedObject["foods"].ToString();

                    //                            Debug.WriteLine(json);
                    //                          Debug.WriteLine(json2);
                    //                           client2.Dispose();

                    // var yo = json2.Count(); 



                }
            }
            else
            {

                await DisplayAlert("Error", "Could not reach database.\nCheck your internet conenction.", "OK");
                await Navigation.PopAllPopupAsync();
            }
            return null;



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

        private async void Button_ClickedPrev(object sender, EventArgs e)
        {
            loadingPopup  = new LoadingPopup();
            await Navigation.PushPopupAsync(loadingPopup);
            loadispushed = true;

            pageNo -= 1; //check gia <0
            if (pageNo <= 0)
            {
                pageNo = totalPages;
            }
            var parsedObject = await APIPOST(searchInput, pageNo);

            var json2 = parsedObject["foods"].ToString();
            foodList = JsonConvert.DeserializeObject<IEnumerable<APIPostFoodModel>>(json2);
            foreach (var x in foodList)
            {
                if (x.dataType == "Branded" && x.brandOwner != null)
                {
                    x.description = x.description + "\nBrand: " + x.brandOwner;
                }
            }
            var listView = new ListView();

            listViewFood.ItemsSource = foodList;
            await Navigation.RemovePopupPageAsync(loadingPopup);
            thisPage = pageNo + "/" + totalPages;
            //  var foodNutrients = JsonConvert.DeserializeObject<IEnumerable<APIGetFoodModel>>(json2);

        }
        private async void Button_ClickedNext(object sender, EventArgs e)
        {
            loadingPopup = new LoadingPopup();
            await Navigation.PushPopupAsync(loadingPopup);
            loadispushed = true;

            pageNo += 1;
            if (pageNo > totalPages)
            {
                pageNo = 1;
            }
            var parsedObject = await APIPOST(searchInput, pageNo);

            var json2 = parsedObject["foods"].ToString();
            foodList = JsonConvert.DeserializeObject<IEnumerable<APIPostFoodModel>>(json2);
            foreach (var x in foodList)
            {
                if (x.dataType == "Branded" && x.brandOwner != null)
                {
                    x.description = x.description + "\nBrand: " + x.brandOwner;
                }
            }
            var listView = new ListView();

            listViewFood.ItemsSource = foodList;
            await Navigation.RemovePopupPageAsync(loadingPopup);

            thisPage = pageNo + "/" + totalPages;
        }
    }


}