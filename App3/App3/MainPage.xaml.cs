using App.Controls.Behaviors;
using App3.Models;
using App3.ViewModels;
using App3.Views;
using App3.Views.Tutorials;
using Newtonsoft.Json;
using Plugin.LatestVersion;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace App3
{
    public partial class MainPage : ContentPage
    {
        //private bool _isenable;
        //public bool isenable
        //{
        //    get
        //    {
        //        return _isenable;
        //    }
        //    set
        //    {
        //        _isenable = value;
        //        OnPropertyChanged();
        //    }
        //}
        public MainPage()
        {
            //foreach(var x in data)
            //{
            //    Debug.WriteLine(x.Key + " " + x.Value);
            //}
            //data.Clear();
            InitializeComponent();

            CheckData();
            CheckRate();
            InitParams();
           
            //var food = new FoodModel
            //{
            //   Calories = 10,
            //   Protein = 10,
            //   Fat=10,
            //   Carbs = 10,
            //   Name = "yolo",
            //   ClassId = 1,
            //   TimeOfDay = FoodModel.TimeOfDayEmum.Breakfast,
            //   TimesPressed = 0,
            //   Portion = 100,
            //   MoreInfo = ""

            //};

            //data["goalcal"] = "20000";
            //data["goalcarb"] = "2000";
            //data["goalprot"] = "1500";
            //data["goalfat"] = "750";
            //Application.Current.SavePropertiesAsync();

            //testing
            //data["datestoshow"] = "3";
            ////ayksanoume ta dates pou tha deiksoume (for i=1 , i<=datestoshow)

            //data[1 + "datetoshow"] = "1574516814"; //apothikeyoume to date poy tha deiksoyme
            //data[1 + "cal"] = 1000;
            //data[1 + "fat"] = 10;
            //data[1 + "prot"] = 10;
            //data[1 + "carb"] = 10;

            //data[2 + "datetoshow"] = "1574603214"; //apothikeyoume to date poy tha deiksoyme
            //data[2 + "cal"] = 2000;
            //data[2 + "fat"] = 20;
            //data[2 + "prot"] = 20;
            //data[2 + "carb"] = 20;

            //data[3 + "datetoshow"] = "1574689614"; //apothikeyoume to date poy tha deiksoyme
            //data[3 + "cal"] = 3000;
            //data[3 + "fat"] = 30;
            //data[3 + "prot"] = 30;
            //data[3 + "carb"] = 30;


            //Breakfastbutton.TextColor.WithHue(100);


            //Breakfastbutton.BackgroundImage = "@drawable/breakfast.jpg";
            //Brunchbutton.BackgroundImage = "@drawable/breakfast.jpg";
            

        }
        private async void CheckRate()
        {
            var data = Xamarin.Forms.Application.Current.Properties;
            if (data["hasRated"].ToString() == "false")
            {
                var counter = Convert.ToInt32(data["hasRatedCounter"].ToString()) + 1;
                data["hasRatedCounter"] = counter.ToString();
                await Xamarin.Forms.Application.Current.SavePropertiesAsync();
                if (counter > 4)
                {
                    RateGoogl();
                }
            }
        }
        private async void RateGoogl()
        {
            var rate = new RateGooglePopup("If you enjoy this app , please take the time to leave a rating and/or a review!");
            await Navigation.PushPopupAsync(rate);

        }
        //public async static void ApplyShadowHack(List<Xamarin.Forms.Button> btns){
        //    await Task.Delay(50);
        //    foreach (var btn in btns)
        //    {
        //        var col = btn.BackgroundColor;
        //        btn.BackgroundColor = Color.Wheat;
        //        btn.BackgroundColor = col;
        //    }
        //    }

        public async static void ApplyShadowHack(List<RadButton> btns)
        {
            await Task.Delay(100);
            foreach (var btn in btns)
            {
                var col = btn.BackgroundColor;
                btn.BackgroundColor = Color.Wheat;
                btn.BackgroundColor = col;
            }
        }
        public async static void ApplyShadowHack(RadButton btn)
        {
            await Task.Delay(100);
         
                var col = btn.BackgroundColor;
                btn.BackgroundColor = Color.Wheat;
                btn.BackgroundColor = col;
            
        }
        
        private void CheckData()
        {
            var data = Xamarin.Forms.Application.Current.Properties;
           //data.Clear();
            bool isLatest = true;
            try
            {
                var appV =  CrossLatestVersion.Current.InstalledVersionNumber;
                if (data["appV"].ToString() == appV)
                {
                    isLatest = true;
                }
            }
            catch
            {
                
            }

            #region dataset
            if (!(data.Any(p => p.Key == "dataset")))
            {

                data["dataset"] = "set";
                data["goalset"] = "false";
                data["todayscal"] = "0";
                data["todaysfat"] = "0";
                data["todayscarb"] = "0";
                data["todaysprot"] = "0";

                data["todayspressedlist"] = "";
                data["removedClassIds"] = "";
                data["removedMeals"] = "";


                data["viewedtutorial"] = "";
                data["mealviewedtutorial"] = "";
                data["mealviewedtutorial1"] = "";
                data["mealviewedtutorial2"] = "";
                data["mealviewedtutorial3"] = "";
                data["mealviewedtutorial4"] = "";
                data["mealviewedtutorial5"] = "";
                data["mealviewedtutorial6"] = "";
                data["mealviewedtutorial7"] = "";
                data["mealviewedtutorial8"] = "";
                data["mealviewedtutorial9"] = "";
                data["mealviewedtutorial10"] = "";
                
                data["mealviewedtutoriallast"] = "";
                data["appV"] = CrossLatestVersion.Current.InstalledVersionNumber;

                data["hasRated"] = "false";
                data["hasRatedCounter"] = "0";
                




                // data["noOfFoods"] = "25";
                data["noOfFoods"] = "24";
                data["noOfMeals"] = "0";


                //

                var food = new FoodModel(FoodModel.TimeOfDayEmum.Breakfast, 68, 9, 3, 1, "Sugars: 0g", 14.5, "Biscuit No Sugar", 1, 0);
                var foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                //


                food = new FoodModel(FoodModel.TimeOfDayEmum.Breakfast, 40, 6, 1, 2, "", 100, "Rusk", 2, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                //


                food = new FoodModel(FoodModel.TimeOfDayEmum.Breakfast, 70, 0, 5, 6, "", 50, "Egg", 3, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Breakfast, 44, 10, 0, 1, "Prepacked", 100, "Orange Juice", 4, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);

                food = new FoodModel(FoodModel.TimeOfDayEmum.Breakfast, 64, 17, 0, 0, "1 spoon", 21, "Honey", 5, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Brunch, 105, 27, 1, 1, "", 100, "Banana", 6, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Brunch, 111, 17, 4, 1, "Sugars: 8.9g", 100, "Chocolate bar", 7, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Brunch, 72, 12, 1, 1, "Sugars: 2g", 20, "Roasted Chick Peas", 8, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Brunch, 83, 12, 2, 3, "Whole Grain\nSugars: 2.2g", 24, "Whole Bread Slice", 9, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Brunch, 80, 0, 5, 10, "", 28, "Low Fat Cheesee Slice", 10, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Lunch, 124, 27, 1, 5, "Equivalent to a medium plate, cooked", 100, "Whole Spaghetti", 11, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Lunch, 157, 31, 1, 6, "Equivalent to a medium plate, cooked", 100, "Spaghetti", 12, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Lunch, 140, 40, 1, 5, "Quarter Cup\nSugars: 0g", 30, "Basmati", 13, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Lunch, 80, 0, 1, 19, "", 80, "Tuna in Water", 14, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Lunch, 25, 4, 1, 2, "Equivalent to one normal serving.\nSugars: 3.5g", 50, "Tomato Sauce", 15, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Lunch, 49, 1, 4, 3, "One small.\nSugars: 0.2g", 20, "Meatball Baked", 16, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Lunch, 134, 29, 1, 3, "One medium,100g.\nSugars: 7g", 100, "Vegetable Patty", 17, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Supper, 95, 25, 1, 1, "", 180, "Apple", 18, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////
                food = new FoodModel(FoodModel.TimeOfDayEmum.Supper, 180, 45, 0, 1, "500 ml\nSugars: 44.5g", 500, "Aloe", 19, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Dinner, 42, 5, 3, 3, "", 100, "Milk", 20, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Lunch, 99, 4, 7, 5, "One.", 50, "Vienna Sausage", 21, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Brunch, 25, 0.9, 1.6, 1.7, "", 16, "Smoked Turkey Slice", 22, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Dinner, 82, 0, 6.9, 5, "", 30, "Feta Cheese", 23, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                ////

                food = new FoodModel(FoodModel.TimeOfDayEmum.Extra, 43, 3.6, 0, 0.5, "", 100, "Beer", 24, 0);
                foodJson = JsonConvert.SerializeObject(food);
                data.Add(food.ClassId.ToString() + "food", foodJson);
                //


                Xamarin.Forms.Application.Current.SavePropertiesAsync();



            }
            #endregion
            if(!isLatest)
            {
                //new vars here
            }
        }
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);

        private static long ConvertToTimestamp(DateTime value, bool isNow)
        {
            TimeSpan elapsedTime = value - Epoch;

            return (long)elapsedTime.TotalSeconds;


           
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
           // dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);

            return dtDateTime;
        }

        public async void InitParams()
        {
            var data = Xamarin.Forms.Application.Current.Properties;
            bool exists = data.Any(p => p.Key == "setdate");
            if (!exists)
            {
                string myinput = await InputBox(this.Navigation);
                string[] hour = myinput.Split(':');
                var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(hour[0]), Convert.ToInt32(hour[1]), Convert.ToInt32(hour[2]));
                data["startofday"] = (ConvertToTimestamp(date, false) ).ToString();

              //  data["setdate"] =( ConvertToTimestamp(date, false)+86400).ToString();
                data["setdate"] = (ConvertToTimestamp(date, false) ).ToString();

                data["datestoshow"] = "0";
                // await Application.Current.SavePropertiesAsync();

            }
            else
            {
                long lastdate = Convert.ToInt64(data["setdate"]);
                long now = ConvertToTimestamp(DateTime.Now, true);
                Daypassed(lastdate, now);
            }
            await Xamarin.Forms.Application.Current.SavePropertiesAsync();

        }
        public int timescalled=0;
        public void Daypassed(long lastdate, long now)
        {

            timescalled += 1;
            if (now - lastdate < 86400)
            {
                return;
            }


            if (now - lastdate >= 86400 && now - lastdate < 172800) //day passed
            {
                var data = Xamarin.Forms.Application.Current.Properties;
                data["setdate"] = (lastdate + 86400*timescalled).ToString();
                var datestoshow = Convert.ToInt32(data["datestoshow"]);
                datestoshow = datestoshow += 1; //ayksanoume ta dates pou tha deiksoume (for i=1 , i<=datestoshow)
                data["datestoshow"] = datestoshow.ToString(); //ayto edw dn xreiazetai pros to paron
                var startofday = UnixTimeStampToDateTime(Convert.ToDouble(data["startofday"].ToString()));
                var now2 = DateTime.Now;
                var num = (now2.Date - startofday.Date).TotalDays;
                data[num + "datetoshow"] = lastdate.ToString(); //apothikeyoume to date poy tha deiksoyme
                data[num + "cal"] = data["todayscal"];
                data[num + "fat"] = data["todaysfat"];
                data[num + "prot"] = data["todaysprot"];
                data[num + "carb"] = data["todayscarb"];
                data["todayscal"] = "0";
                data["todaysfat"] = "0";
                data["todayscarb"] = "0";
                data["todaysprot"] = "0";
                //var no = Convert.ToInt32(data["noOfFoods"]);
                //for (int i = 1; i <= no; i++)
                //{
                //    data[i.ToString() + "foodtimespressed"] = "0";
                //}
                //no = Convert.ToInt32(data["noOfMeals"]);
                //for (int i = 1; i <= no; i++)
                //{
                //    data[i.ToString() + "foodtimespressed"] = "0";
                //}
                SaveFoodEatenOfOldDate(num);
                data["todayspressedlist"] = "";

                ResetTimesPressed();
                ResetTimesPressedMeal();

                // var datetosave = UnixTimeStampToDateTime(lastdate);//kalytera save to timestamp, kai meta convert+
              

                return;
            }
            else
            {
                Daypassed(lastdate , now-86400);
            }
        }
        private void SaveFoodEatenOfOldDate(double num)
        {
            var data = Xamarin.Forms.Application.Current.Properties;
            var list = new List<int>();
            var food = "";
         
            data[num + "day"] = "";
            food = data["todayspressedlist"].ToString();

            int inde = 0;
            do
            {
                //if(food[inde] == ',')
                //{
                //    inde += 1;
                //    continue;
                //}

                var q = "";// + ",";
                do
                {
                    q += food[inde].ToString();// + ",";
                    inde += 1;
                } while (food[inde].ToString() != ",");
                inde = 0;

                if (!list.Contains(Convert.ToInt32(q.ToString())))
                {
                    list.Add(Convert.ToInt32(q.ToString()));
                }

                q += ",";
                // food = food.Replace(q, "");
                int qLength = q.Length;
                int chRemoved = 0;
                int tempInt = 0;
                string temp = "";
                for (int ii = 0; ii < food.Length; ii++)
                {
                    if (tempInt != qLength)
                    {
                        temp += food[ii - chRemoved];
                        tempInt += 1;
                    }
                    if (temp == q)
                    {
                        food = food.Remove(ii - tempInt + 1 - chRemoved, tempInt);
                        chRemoved += tempInt;
                        tempInt = 0;
                        temp = "";
                    }

                }

                if (food.Length == 0)
                {
                    break;
                }
                // inde += 1;
            } while (true);
            string show = "";
            inde = 0;
            foreach (var y in list)
            {
                if (data["removedClassIds"].ToString().Contains(list[inde].ToString() + ","))
                {
                    inde += 1;
                    continue;
                }
                var q = JsonConvert.DeserializeObject<FoodModel>(data[list[inde] + "food"].ToString());
                show += q.Name + " : " + q.TimesPressed + ". \n";





                inde += 1;
            }
            data[num + "day"] = show;
            
        }
        private void ResetTimesPressed()
        {
            var data = Xamarin.Forms.Application.Current.Properties;
            for (int i = 1; i <= Convert.ToInt32(data["noOfFoods"]); i++)
            {
                if (data["removedClassIds"].ToString().Contains(i.ToString() + ","))
                {
                    continue;
                }
                var x = data[i.ToString() + "food"].ToString();
                var food = JsonConvert.DeserializeObject<FoodModel>(x);
                if (food.TimesPressed == 0)
                {
                    continue;
                }
                food.TimesPressed = 0;
                var qq = JsonConvert.SerializeObject(food);
                data[i.ToString() + "food"] = qq;
            }
        }
        private void ResetTimesPressedMeal()
        {
            var data = Xamarin.Forms.Application.Current.Properties;
            for (int i = 1; i <= Convert.ToInt32(data["noOfMeals"]); i++)
            {
                if (data["removedMeals"].ToString().Contains(i.ToString() + ","))
                {
                    continue;
                }
                var x = data[i.ToString() + "Meal"].ToString();
                var food = JsonConvert.DeserializeObject<MealModel>(x);
                if (food.TimesPressed == 0)
                {
                    continue;
                }
                food.TimesPressed = 0;
                var qq = JsonConvert.SerializeObject(food);
                data[i.ToString() + "Meal"] = qq;
            }
        }
        private object syncLock = new object();
        bool isInCall = false;
        private async void Mainbutton_Clicked(object sender, EventArgs e)
        {
            lock (syncLock)
            {
                if (isInCall)
                    return;
                isInCall = true;
            }

            try
            {
                var data = Xamarin.Forms.Application.Current.Properties;
                var SenderName = (sender as Xamarin.Forms.Button).ClassId;
                var loadingPage = new LoadingPopup();
                await Navigation.PushPopupAsync(loadingPage); //KYR EDW AMA KOLLAEI

                ContentPage contentPage = null;
                //switch (SenderName)
                //{
                //    case "Overview":
                //        break;
                //    case "Breakfast":
                //        break;
                //    case "Brunch":
                //        break;
                //    case "Lunch":
                //        break;
                //    case "Supper":
                //        break;
                //    case "Dinner":
                //        break;
                //    case "Extra":
                //        break;
                //    case "Settings":
                //        break;

                //}
                if (SenderName == "Overview" )
                {
                    //string x = "Calories: " + data["todayscal"].ToString() + " Fat: " + data["todaysfat"].ToString() + " Prot: " + data["todaysprot"].ToString() + " Carb: " + data["todayscarb"].ToString();
                    //var datestoshow = Convert.ToInt32(data["datestoshow"]);
                    //if (datestoshow > 0)
                    //{
                    //    for (int i = 1; i <= datestoshow; i++)
                    //    {
                    //        x += UnixTimeStampToDateTime(Convert.ToDouble(data[i.ToString() + "datetoshow"].ToString()));
                    //        x += "\n";
                    //        x += "Calories: " + data[i.ToString() + "cal"].ToString() + " Fat: " + data[i.ToString() + "fat"].ToString() + " Prot: " + data[i.ToString() + "prot"].ToString() + " Carb: " + data[i.ToString() + "carb"].ToString();
                    //        x += "\n";
                    //    }
                    //}
                    //var alert = new PopupAlert(x);
                    //await Navigation.RemovePopupPageAsync(loadingPage);
                    //await Navigation.PushPopupAsync(alert);
                   

                        var exists = data["goalset"].ToString();
                        if (exists != "true")
                        {
                            var alert = new PopupAlert("Set your goals in the settings menu first!");
                            await Navigation.PushPopupAsync(alert);
                            await Navigation.RemovePopupPageAsync(loadingPage);
                            return;

                        }
                    
                    contentPage = new OverviewView();
                }else if ( SenderName == "Settings")
                {
                    contentPage = new SettingsView();
                    
                }
                else
                {
                    int.TryParse(SenderName, out int i);
                    contentPage = new MealView((FoodModel.TimeOfDayEmum)i);
                }

                string myinput = await OpenPageFromMain(this.Navigation, loadingPage, contentPage, SenderName);
                if (myinput == "ok")
                {

                }
            }
            finally
            {
                
                lock (syncLock)
                {
                    isInCall = false;
                 }
            }

        }

        public  Task<string> OpenPageFromMain(INavigation navigation, PopupPage loadingPage,ContentPage pagetoopen, string SenderName)
        {

            //apo edw kai katw gia na ginei to return
            var tcs = new TaskCompletionSource<string>();




            var btnOk = new Xamarin.Forms.Button
            {
                Text = "Done",
                WidthRequest = 100,
                BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8),
            };
            btnOk.Clicked += async (s, e) =>
            {
                // close page

                await navigation.PopModalAsync();
                // pass result
                tcs.SetResult("ok");
            };
            
            //apo edw kai panw gia na ginei to return

            var fabBtn = new RadButton
            {
                FontSize = 50,
                VerticalContentAlignment = TextAlignment.End,
                HeightRequest = 65,
                WidthRequest = 65,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.ForestGreen,
                TextColor = Color.White,
                FontFamily = "Quicksand-Regular#Quicksand",
                Text = "+",
                BorderColor = Color.White,
                CornerRadius=60,
                ClassId = "addFab",
                IsVisible = true,
                Scale = 1,


            };
            fabBtn.On<Android>().SetUseDefaultPadding(true).SetUseDefaultShadow(true);
            fabBtn.Margin = new Thickness(250, 0, 0, 0);
            fabBtn.Behaviors.Add(new LongPressBehavior()); //LONG PRESS BEHAVIORS
            var yo = fabBtn.Behaviors[0] as LongPressBehavior; //LONG PRESS BEHAVIORS
            yo.LongPressed += OnFabButtonLongPressed; //LONG PRESS BEHAVIORS
            fabBtn.Behaviors[0] = yo; //LONG PRESS BEHAVIORS
            mealViewRef = (pagetoopen as MealView); //LONG PRESS BEHAVIORS
            var fabBtn2 = new RadButton
            {
                FontSize = 50,
                VerticalContentAlignment = TextAlignment.End,
                HeightRequest = 65,
                WidthRequest = 65,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.IndianRed,
                TextColor = Color.White,
                FontFamily = "Quicksand-Regular#Quicksand",
                Text = "++",
                BorderColor = Color.White,
                CornerRadius = 60,
                ClassId = "mergeFab",
                IsVisible = false,
                Scale = 0,


            };
            fabBtn2.On<Android>().SetUseDefaultPadding(true).SetUseDefaultShadow(true);
            fabBtn2.Margin = new Thickness(250, 0, 0, 0);

            var x = new List<RadButton>();
            addFabRef = fabBtn;
            mergeFabRef = fabBtn2;
            x.Add(addFabRef);
            x.Add(mergeFabRef);

            var slButtons = new StackLayout
            {
               // Children = { fabBtn },

                Children = { fabBtn,fabBtn2 },
                //Children = { btnOk, btnCancel },
            };
           
           
            fabBtn.Clicked += (s, e) =>
            {
                if ((s as Xamarin.Forms.Button).Scale != 1)
                {
                    return;
                }
                if (!longpressFab)
                {
                    (pagetoopen as MealView).AddButtonWindow(s, e);
                }
                else
                {
                    // OnFabButtonLongPressed(s,null);
                }
                ApplyShadowHack(x);

            };
            fabBtn2.Clicked += (s, e) =>
            {
                if ((s as Xamarin.Forms.Button).Scale != 1)
                {
                    return;
                }
                if (longpressFab)
                {
                    OnFabButtonLongPressed(s, null);
                }
                ApplyShadowHack(x);
            };

            ApplyShadowHack(x);
            // Clicked = "AddButtonWindow"




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

            var PageToOpen = pagetoopen;

            //var layout = new StackLayout
            //{
            //    Children = { carview.Content, slButtons }
            //};
            var layout = new StackLayout { }; // to idio xwris to koympi
            
            if ((pagetoopen as MealView) != null)
            {
                var pp = pagetoopen as MealView; //ayto edw einai gia button grid ousiastika
                if (SenderName == ((int)FoodModel.TimeOfDayEmum.Breakfast).ToString())
                {
                    pp.MealFrame.BackgroundColor = Color.LightGoldenrodYellow;

                }
                if (SenderName == ((int)FoodModel.TimeOfDayEmum.Brunch).ToString())
                {
                     pp.MealFrame.BackgroundColor= Color.SandyBrown;
                }
                if (SenderName == ((int)FoodModel.TimeOfDayEmum.Lunch).ToString())
                {
                     pp.MealFrame.BackgroundColor= Color.Khaki;
                }
                if (SenderName == ((int)FoodModel.TimeOfDayEmum.Supper).ToString())
                {
                     pp.MealFrame.BackgroundColor= Color.Teal;
                }
                if (SenderName == ((int)FoodModel.TimeOfDayEmum.Dinner).ToString())
                {
                     pp.MealFrame.BackgroundColor= Color.SteelBlue;
                }
                if (SenderName == ((int)FoodModel.TimeOfDayEmum.Extra).ToString())
                {
                     pp.MealFrame.BackgroundColor= Color.LightBlue;
                }

                layout = new StackLayout // to idio xwris to koympi
                {
                    Children = { PageToOpen.Content, slButtons }
                };
            }
            else
            {
                var pp = pagetoopen as SettingsView;
                if (pp != null)
                {
                    if (SenderName == "Settings")
                    {
                        pp.MealFrame.BackgroundColor = Color.LightSalmon;
                    }
                }
                layout = new StackLayout // to idio xwris to koympi
                {
                    Children = { PageToOpen.Content}
                };
                
            }
            var page = new ContentPage();
            if (SenderName == ((int)FoodModel.TimeOfDayEmum.Breakfast).ToString())   //ayto edw einai gia to katw katw me to addfab
            {
                page.BackgroundColor = Color.LightGoldenrodYellow;
                
            }
            if (SenderName == ((int)FoodModel.TimeOfDayEmum.Brunch).ToString())  
            {
                page.BackgroundColor = Color.SandyBrown;
            }
            if (SenderName == ((int)FoodModel.TimeOfDayEmum.Lunch).ToString())  
            {
                page.BackgroundColor = Color.Khaki;
            }
            if (SenderName == ((int)FoodModel.TimeOfDayEmum.Supper).ToString())  
            {
                page.BackgroundColor = Color.Teal;
            }
            if (SenderName == ((int)FoodModel.TimeOfDayEmum.Dinner).ToString()) 
            {
                page.BackgroundColor = Color.SteelBlue;
            }
            if (SenderName == ((int)FoodModel.TimeOfDayEmum.Extra).ToString())  
            {
                page.BackgroundColor = Color.LightBlue;
            }
            if (SenderName == "Settings")  
            {
                page.BackgroundColor = Color.LightSalmon;
            }
            page.Content = layout;
             navigation.RemovePopupPageAsync(loadingPage); 
            // navigation.PushAsync(page);
            //navigation.PushAsync(new NavigationPage( page));

           
            navigation.PushModalAsync(page);
            // open keyboard
            //txtInput.Focus();

            // code is waiting her, until result is passed with tcs.SetResult() in btn-Clicked
            // then proc returns the result
            tcs.SetResult("ok");
            return tcs.Task;
        }
        MealView mealViewRef;
        RadButton addFabRef;
        RadButton mergeFabRef;
        public static bool longpressFab = false;
        public async void OnFabButtonLongPressed(object sender, EventArgs args)
        {
            //longpressFab = true;
           
            if (mealViewRef != null)
            {
                if ((sender as RadButton) == null)
                {
                    return;
                }
                if ((sender as RadButton).ClassId == "addFab")
                {
     
              
                 
                    await (sender as RadButton).ScaleTo(0,200,Easing.CubicOut);
                    (sender as RadButton).IsVisible = false;
                    mergeFabRef.IsVisible = true;
                    await mergeFabRef.ScaleTo(1, 200, Easing.CubicIn);
                    var data = Xamarin.Forms.Application.Current.Properties;
                    if (data["mealviewedtutorial9"].ToString() != "ok" && data["mealviewedtutorial8"].ToString() == "ok")
                    {

                        //var qq =  rdbutton.Parent.GetPropertyIfSet(Grid.RowProperty,false);


                        var page = new MealTutorialView9();
                        await Navigation.PushPopupAsync(page);
                        //string myinput = await TutorialPage(this.Navigation);

                        // StartTutorial();
                    }


                }
                else if ((sender as RadButton).ClassId == "mergeFab")
                {
            
                    
                    await (sender as RadButton).ScaleTo(0, 200, Easing.CubicOut);
                    (sender as RadButton).IsVisible = false;
                    addFabRef.IsVisible = true;
                    await addFabRef.ScaleTo(1, 200, Easing.CubicIn);


                }
                var x = new List<RadButton>();
              
                x.Add(addFabRef);
                x.Add(mergeFabRef);
                ApplyShadowHack(x);

                mealViewRef.StartCombine(null, null);
            }
           
        }
        public Task<string> InputBox(INavigation navigation)
        {
            // wait in this proc, until user did his input 
            var tcs = new TaskCompletionSource<string>();

            var lblTitle = new Label { Text = "Beggining of day", HorizontalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold };
            var lblMessage = new Label { Text = "Choose when to reset your daily calories." };
            var txtInput = new TimePicker
            {
                Time = TimeSpan.FromHours(9),
            };

            var btnOk = new Xamarin.Forms.Button
            {
                Text = "Ok",
                WidthRequest = 100,
                BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8),
            };
            btnOk.Clicked += async (s, e) =>
            {
                // close page
                var result = txtInput.Time.ToString();
                await navigation.PopModalAsync();
                // pass result
                tcs.SetResult(result);
            };

            //var btnCancel = new Button
            //{
            //    Text = "Cancel",
            //    WidthRequest = 100,
            //    BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8)
            //};
            //btnCancel.Clicked += async (s, e) =>
            //{
            //    // close page
            //    await navigation.PopModalAsync();
            //    // pass empty result
            //    tcs.SetResult(null);
            //};

            var slButtons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { btnOk },
                //Children = { btnOk, btnCancel },
            };

            var layout = new StackLayout
            {
                Padding = new Thickness(0, 40, 0, 0),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { lblTitle, lblMessage, txtInput, slButtons },
            };

            // create and show page
            var page = new ContentPage();
            page.Content = layout;
            navigation.PushModalAsync(page);
            // open keyboard
            txtInput.Focus();

            // code is waiting her, until result is passed with tcs.SetResult() in btn-Clicked
            // then proc returns the result
            return tcs.Task;
        }
        public static Task<string> CustomAlert(INavigation navigation, string msg)
        {
            //apo edw kai katw gia na ginei to return
            var tcs = new TaskCompletionSource<string>();
            var page = new PopupPage();


            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });


            var btnYes = new RadButton
            {
                Text = "Yes",
                BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8),
            };
            var btnNo = new RadButton
            {
                Text = "No",
                BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8),
            };


            grid.Children.Add(btnYes);
            grid.Children.Add(btnNo);
            Grid.SetRow(btnYes, 0);
            Grid.SetRow(btnNo, 0);
            Grid.SetColumn(btnYes, 0);
            Grid.SetColumn(btnNo, 1);

            var slButtons = new StackLayout
            {

                Children = { grid },
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

            var carview = new PopupAlert(msg, false, true);

            var layout = new StackLayout
            {
                Children = { carview.Content, slButtons },
                VerticalOptions = LayoutOptions.Center

            };
            page.Content = layout;
            navigation.PushPopupAsync(page);
            btnYes.Clicked += async (s, e) =>
            {
                // close page

                await navigation.RemovePopupPageAsync(page);

                tcs.SetResult("yes");
            };
            btnNo.Clicked += async (s, e) =>
            {
                // close page

                await navigation.RemovePopupPageAsync(page);

                tcs.SetResult("no");
            };
            // open keyboard
            //txtInput.Focus();

            // code is waiting her, until result is passed with tcs.SetResult() in btn-Clicked
            // then proc returns the result
            return tcs.Task;
        }

        public  Task<string> ShowTutorial(INavigation navigation)
        {
            //apo edw kai katw gia na ginei to return
            var tcs = new TaskCompletionSource<string>();




            var btnOk = new Xamarin.Forms.Button
            {
                Text = "Got It!",
                WidthRequest = 100,
                BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8),
            };
            btnOk.Clicked += async (s, e) =>
            {
                // close page
               if (Xamarin.Forms.Application.Current.Properties["viewedtutorial"].ToString() != "ok")
                {
                    string myinput = await CustomAlert(this.Navigation, "You haven't finished the tutorial!\nAre you sure you want to skip it?");
                    if(myinput == "no")
                    {
                        return;
                    }
                }

                await navigation.PopModalAsync();
                // pass result
                tcs.SetResult("ok");
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

            var carview = new TutorialView();

            var layout = new StackLayout
            {
                Children = { carview.Content, slButtons }
            };
            var page = new ContentPage();
            page.Content = layout;
            navigation.PushModalAsync(page);
            // open keyboard
            //txtInput.Focus();

            // code is waiting her, until result is passed with tcs.SetResult() in btn-Clicked
            // then proc returns the result
            return tcs.Task;
        }
        public async void StartTutorial()
        {
            string myinput = await ShowTutorial(this.Navigation);
            if (myinput == "ok")
            {
                Xamarin.Forms.Application.Current.Properties["viewedtutorial"] = "ok";
            }
        }
    }
}
