using App.Controls.Behaviors;
using App3.Models;
using App3.ViewModels;
using App3.Views.Tutorials;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MealView : ContentPage
	{
        public static MealView Invoker;
        public bool longpress = false;
        public FoodModel.TimeOfDayEmum timofday;
        private int startrow = 0, startcol = 0;
        public List<RadButton> buttonlist = new List<RadButton>();
        public List<RadButton> buttonstomerge;
        //public MealViewModel viewModel;
        public MealView (FoodModel.TimeOfDayEmum timeOfDay)
		{
            Invoker = this;
            InitializeComponent ();

            timofday = timeOfDay;
            DisplayFoods(timeOfDay); 
            CheckTutorial();
            var data = Xamarin.Forms.Application.Current.Properties;
            var cal = Convert.ToDouble(data["todayscal"].ToString());
            var fat =Convert.ToDouble(data["todaysfat"].ToString());
            var prot =  Convert.ToDouble(data["todaysprot"].ToString());
            var carb =  Convert.ToDouble(data["todayscarb"].ToString());

            UpdateCalLabel(cal, carb, prot, fat);
            //var swp = new SwipeGestureRecognizer();
            //swp.Swiped += OnButtonClicked;
            //this.Content.GestureRecognizers.Add(swp);
            //foreach (var x in buttonlist)
            //{
            //    var col = x.BackgroundColor;
            //    x.BackgroundColor = Color.Beige;
            //    x.BackgroundColor = col;
            //}

            MainPage.ApplyShadowHack(buttonlist);


        }
    
        public void UpdateAllButtons()
        {
            foreach (RadButton btn in buttonlist)
            {
                var sl = btn.Parent as StackLayout;
                sl.Children.Clear();


            }
            buttonlist.Clear();
            DisplayFoods(timofday);
            MainPage.ApplyShadowHack(buttonlist);

            //foreach (var x in buttonlist)
            //{
            //    x.On<Android>().SetUseDefaultPadding(true).SetUseDefaultShadow(true);
            //}
        }
        private void DisplayFoods(FoodModel.TimeOfDayEmum timeOfDay)
        {
            startrow = 0;
            startcol = 0;
            var data = Xamarin.Forms.Application.Current.Properties;
           
            for (int i = 1; i <= Convert.ToInt32(data["noOfFoods"]); i++)
            {
                if (data["removedClassIds"].ToString().Contains(i.ToString() + ","))
                {
                    continue;
                }
                var x = data[i.ToString() + "food"].ToString();
                var food = JsonConvert.DeserializeObject<FoodModel>(x);
                if (food.TimeOfDay != timeOfDay)
                {
                    continue;
                }
                if (food.isPartOfMeal)
                {
                    continue;
                }
                var button = CustomButtonCreator(food, startrow, startcol);
                if (food.TimesPressed != 0)
                {
                    ShowTimesPressed(food, button as Xamarin.Forms.Button);
                }

                if (startcol == 2)
                {
                    ButtonGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    Grid.SetRow(fillerLabel, Grid.GetRow(fillerLabel) + 1);
                    startrow += 1;
                    startcol = 0;
                }
                else
                {
                    startcol += 1;
                }
            }
            for (int i = 1; i <= Convert.ToInt32(data["noOfMeals"]); i++)
            {
                if (data["removedMeals"].ToString().Contains(i.ToString() + ","))
                {
                    continue;
                }
                var x = data[i.ToString() + "Meal"].ToString();
                var meal = JsonConvert.DeserializeObject<MealModel>(x);
                if (meal.TimeOfDay != timeOfDay)
                {
                    continue;
                }

              
                var button = CustomMealCreator(meal, startrow, startcol);
                if (meal.TimesPressed != 0)
                {
                    ShowTimesMealPressed(meal.TimesPressed, button as Xamarin.Forms.Button);
                }

                if (startcol == 2)
                {
                    ButtonGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    Grid.SetRow(fillerLabel, Grid.GetRow(fillerLabel) + 1);
                    startrow += 1;
                    startcol = 0;
                }
                else
                {
                    startcol += 1;
                }
            }


        }
        public async void StartTut3()
        {

           
                await Task.Delay(150);
                var page = new MealTutorialView3();
                await Navigation.PushPopupAsync(page);
            
        }
        public async void StartTut8()
        {


            await Task.Delay(150);
            var page = new MealTutorialView8();
            await Navigation.PushPopupAsync(page);

        }
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //}
        public void CustomButtonCreatorInvoker(FoodModel food)
        {
             CustomButtonCreator(food, startrow, startcol);

        }
        public RadButton CustomButtonCreator(FoodModel food, int row, int col)
        {
            var data = Xamarin.Forms.Application.Current.Properties;
         
            RadButton custombutton = new RadButton
            {
                Text =food.Name,
                ClassId = food.ClassId.ToString(),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                MinimumHeightRequest = 50,
                WidthRequest = 300,
                BackgroundColor = Color.DarkTurquoise,
                TextColor = Color.Black,
                BorderColor = Color.White,
                CornerRadius = 60
            };
            buttonlist.Add(custombutton);
            
            //PROSOXI EXW BALEI NA GINONTAI DISABLE TA MEAL KOUMPIA ME VASEI TO XRWMA
            //PSAKSE REFERENCE TOU buttonslist GIA NA TO DEIS
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            custombutton.Clicked += async (sender, args) => OnButtonClicked(sender, args);
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

            var stackLayout = new StackLayout
            {
                Children = { custombutton }
            };
            custombutton.Behaviors.Add(new LongPressBehavior()); //LONG PRESS BEHAVIORS
            var yo = custombutton.Behaviors[0] as LongPressBehavior; //LONG PRESS BEHAVIORS
            yo.LongPressed += OnButtonLongPressed; //LONG PRESS BEHAVIORS
            custombutton.Behaviors[0] = yo; //LONG PRESS BEHAVIORS
            Grid.SetRow(stackLayout, row);
            Grid.SetColumn(stackLayout, col);
            ButtonGrid.Children.Add(stackLayout);
            //custombutton.On<Xamarin.Forms.PlatformConfiguration.Android>().SetUseDefaultPadding(true).SetUseDefaultShadow(true);

            return custombutton;
        }
        public RadButton CustomMealCreator(MealModel meal, int row, int col)
        {
            var data = Xamarin.Forms.Application.Current.Properties;

            RadButton custombutton = new RadButton
            {
                Text = meal.MealName,
                ClassId = meal.MealClassId.ToString(),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                MinimumHeightRequest = 50,
                WidthRequest = 300,
                BackgroundColor = Color.MediumVioletRed,
                TextColor = Color.Black,
                BorderColor = Color.White,
                CornerRadius = 60,
                
            };
            buttonlist.Add(custombutton);
            //PROSOXI EXW BALEI NA GINONTAI DISABLE TA MEAL KOUMPIA ME VASEI TO XRWMA
            //PSAKSE REFERENCE TOU buttonslist GIA NA TO DEIS
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            custombutton.Clicked += async (sender, args) => {
                if (combineActive)
                {
                    return;
                }
                if (longpress)
                {
                    return;
                }
                foreach (var clsId in meal.foodClassIds)
                {
                    Xamarin.Forms.Button fakebtn = new Xamarin.Forms.Button();
                    fakebtn.ClassId = clsId.ToString();
                    OnButtonClicked(fakebtn, args);
              
                }
                var q = data[meal.MealClassId.ToString() + "Meal"].ToString();
                MealModel mealClicked = JsonConvert.DeserializeObject<MealModel>(q);
                mealClicked.TimesPressed += 1;
                ShowTimesMealPressed(mealClicked.TimesPressed, (sender as Xamarin.Forms.Button));
                var qq = JsonConvert.SerializeObject(mealClicked);
                data[mealClicked.MealClassId.ToString() + "Meal"] = qq;
                await Xamarin.Forms.Application.Current.SavePropertiesAsync();

            };
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

            var stackLayout = new StackLayout
            {
                Children = { custombutton }
            };
            custombutton.Behaviors.Add(new LongPressBehavior()); //LONG PRESS BEHAVIORS
            var yo = custombutton.Behaviors[0] as LongPressBehavior; //LONG PRESS BEHAVIORS
            yo.LongPressed += OnMealLongPressed; //LONG PRESS BEHAVIORS
            custombutton.Behaviors[0] = yo; //LONG PRESS BEHAVIORS
            Grid.SetRow(stackLayout, row);
            Grid.SetColumn(stackLayout, col);
            ButtonGrid.Children.Add(stackLayout);
            return custombutton;
        }
        public async void OnMealLongPressed(object sender, EventArgs args)
        {
            if (combineActive)
            {
                return;
            }
            longpress = true;
            var data = Xamarin.Forms.Application.Current.Properties;

            var x = sender as Xamarin.Forms.Button;
            var y = x.ClassId;
            int.TryParse(y, out int classid);
            FoodInfoModel foodobject = new FoodInfoModel();
            var q = data[classid + "Meal"].ToString();
            MealModel meal = JsonConvert.DeserializeObject<MealModel>(q);
            double cal = 0, carb = 0, prot = 0, fat = 0, portion = 0;
            string moreinfo = "Combination of : ";
            foreach (var fod in meal.foodClassIds)
            {
                var qq = data[fod + "food"].ToString();
                FoodModel foodtoget = JsonConvert.DeserializeObject<FoodModel>(qq);
                cal += foodtoget.Calories;
                carb += foodtoget.Carbs;
                prot += foodtoget.Protein;
                fat += foodtoget.Fat;
                portion += foodtoget.Portion;
                if (moreinfo == "Combination of : ")
                {
                    moreinfo += foodtoget.Name;
                }
                else
                {
                    moreinfo += " & " + foodtoget.Name;

                }



            }
            foodobject.FoodCal = cal.ToString();
            foodobject.TimeOfDay = meal.TimeOfDay.ToString();
            foodobject.FoodCarb = carb.ToString();
            foodobject.FoodFat = fat.ToString();
            foodobject.FoodProt = prot.ToString();
            foodobject.MoreInfo = moreinfo;
            foodobject.IsCustomButton = portion.ToString();
            foodobject.FoodName = meal.MealName;
            foodobject.ClassId = meal.MealClassId + "Meal";
            foodobject.IsMeal = true;




            //var z = y.ToCharArray();
            //var isNumeric = int.TryParse(z[0].ToString(), out int n);
            //if (isNumeric) //einai custom button
            //{

            //        string q = y.Replace("Brunch", "");

            //        int k = Convert.ToInt32(q);


            //        string calories = Application.Current.Properties[k.ToString() + "custombuttonBrunchCal"].ToString();
            //        string carbs = Application.Current.Properties[k.ToString() + "custombuttonBrunchCarb"].ToString();
            //        string prots = Application.Current.Properties[k.ToString() + "custombuttonBrunchProtein"].ToString();
            //        string fats = Application.Current.Properties[k.ToString() + "custombuttonBrunchFat"].ToString();
            //        string name = Application.Current.Properties[k.ToString() + "custombuttonBrunchName"].ToString();
            //        string moreinfo = Application.Current.Properties[k.ToString() + "custombuttonBrunchMoreInfo"].ToString();

            //        foodobject.FoodCal = calories;
            //        foodobject.FoodCarb = carbs;
            //        foodobject.FoodFat = prots;
            //        foodobject.FoodProt = fats;
            //        foodobject.FoodName = name;
            //        foodobject.MoreInfo = moreinfo;

            //        foodobject.ClassId = x.ClassId;
            //        foodobject.IsCustomButton = true.ToString();
            //        foodobject.TimeOfDay = "Brunch";
            //        foodobject.TimesPressed = "0";
            //        for (int qq = 0; qq < timesButtonPressed.Count; qq++)
            //        {
            //            if (x.ClassId == timesButtonPressed[qq].classid)
            //            {
            //                foodobject.TimesPressed = timesButtonPressed[qq].timespressed.ToString();
            //                break;
            //            }
            //        }


            //        //await DisplayAlert("Info", message, "OK");


            //}
            //else
            //{
            //    //Rg.Plugins.Popup.
            //    //  PopupNavigation.Instance.PushAsync(new FoodInfo());

            //    foodobject = viewModel.Getmacros(x.ClassId);
            //    foodobject.TimesPressed = "0";
            //    for (int qq = 0; qq < timesButtonPressed.Count; qq++)
            //    {
            //        if (x.ClassId == timesButtonPressed[qq].classid)
            //        {
            //            foodobject.TimesPressed = timesButtonPressed[qq].timespressed.ToString();
            //            break;
            //        }
            //    }
            //    foodobject.ClassId = x.ClassId;
            //    foodobject.IsCustomButton = false.ToString();

            //    //await DisplayAlert("Info", message, "OK");
            //}
            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var modalPage = new FoodInfo(foodobject, x, Convert.ToInt32(meal.TimesPressed));
            modalPage.Disappearing += (sender2, e2) =>
            {
                waitHandle.Set();
            };
            await PopupNavigation.Instance.PushAsync(modalPage);
            if (data["mealviewedtutorial2"].ToString() != "ok" && data["mealviewedtutorial1"].ToString() == "ok")
            {

                //var qq =  rdbutton.Parent.GetPropertyIfSet(Grid.RowProperty,false);


                var page = new MealTutorialView2();
                await Navigation.PushPopupAsync(page);
                //string myinput = await TutorialPage(this.Navigation);

                // StartTutorial();
            }
            //edw stamataei k perimenei
            await Task.Run(() => waitHandle.WaitOne());
            //edw stamataei k perimenei
            longpress = false;


            //    var x = sender as Button;


        }
        public async void OnButtonLongPressed(object sender, EventArgs args)
        {
            if (combineActive)
            {
                return;
            }
            longpress = true;
            var data = Xamarin.Forms.Application.Current.Properties;
            
            var x = sender as Xamarin.Forms.Button;
                var y = x.ClassId;
                int.TryParse(y, out int classid);
            FoodInfoModel foodobject = new FoodInfoModel();
            var q = data[classid + "food"].ToString();
            FoodModel foodtoget = JsonConvert.DeserializeObject<FoodModel>(q);
            foodobject.FoodCal = foodtoget.Calories.ToString();
            foodobject.TimeOfDay = foodtoget.TimeOfDay.ToString();
            foodobject.FoodCarb = foodtoget.Carbs.ToString();
            foodobject.FoodFat = foodtoget.Fat.ToString();
            foodobject.FoodProt = foodtoget.Protein.ToString();
            foodobject.MoreInfo = foodtoget.MoreInfo.ToString();
            foodobject.IsCustomButton = foodtoget.Portion.ToString();
            foodobject.FoodName = foodtoget.Name.ToString();
            foodobject.ClassId = foodtoget.ClassId.ToString();
            foodobject.IsMeal = false;



            //var z = y.ToCharArray();
            //var isNumeric = int.TryParse(z[0].ToString(), out int n);
            //if (isNumeric) //einai custom button
            //{

            //        string q = y.Replace("Brunch", "");

            //        int k = Convert.ToInt32(q);


            //        string calories = Application.Current.Properties[k.ToString() + "custombuttonBrunchCal"].ToString();
            //        string carbs = Application.Current.Properties[k.ToString() + "custombuttonBrunchCarb"].ToString();
            //        string prots = Application.Current.Properties[k.ToString() + "custombuttonBrunchProtein"].ToString();
            //        string fats = Application.Current.Properties[k.ToString() + "custombuttonBrunchFat"].ToString();
            //        string name = Application.Current.Properties[k.ToString() + "custombuttonBrunchName"].ToString();
            //        string moreinfo = Application.Current.Properties[k.ToString() + "custombuttonBrunchMoreInfo"].ToString();

            //        foodobject.FoodCal = calories;
            //        foodobject.FoodCarb = carbs;
            //        foodobject.FoodFat = prots;
            //        foodobject.FoodProt = fats;
            //        foodobject.FoodName = name;
            //        foodobject.MoreInfo = moreinfo;

            //        foodobject.ClassId = x.ClassId;
            //        foodobject.IsCustomButton = true.ToString();
            //        foodobject.TimeOfDay = "Brunch";
            //        foodobject.TimesPressed = "0";
            //        for (int qq = 0; qq < timesButtonPressed.Count; qq++)
            //        {
            //            if (x.ClassId == timesButtonPressed[qq].classid)
            //            {
            //                foodobject.TimesPressed = timesButtonPressed[qq].timespressed.ToString();
            //                break;
            //            }
            //        }


            //        //await DisplayAlert("Info", message, "OK");


            //}
            //else
            //{
            //    //Rg.Plugins.Popup.
            //    //  PopupNavigation.Instance.PushAsync(new FoodInfo());

            //    foodobject = viewModel.Getmacros(x.ClassId);
            //    foodobject.TimesPressed = "0";
            //    for (int qq = 0; qq < timesButtonPressed.Count; qq++)
            //    {
            //        if (x.ClassId == timesButtonPressed[qq].classid)
            //        {
            //            foodobject.TimesPressed = timesButtonPressed[qq].timespressed.ToString();
            //            break;
            //        }
            //    }
            //    foodobject.ClassId = x.ClassId;
            //    foodobject.IsCustomButton = false.ToString();

            //    //await DisplayAlert("Info", message, "OK");
            //}
            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var modalPage = new FoodInfo(foodobject,x,foodtoget.TimesPressed);
            modalPage.Disappearing += (sender2, e2) =>
            {
                waitHandle.Set();
            };
            await PopupNavigation.Instance.PushAsync(modalPage);
            if (data["mealviewedtutorial2"].ToString() != "ok" && data["mealviewedtutorial1"].ToString() == "ok")
            {

                //var qq =  rdbutton.Parent.GetPropertyIfSet(Grid.RowProperty,false);


                var page = new MealTutorialView2();
                await Navigation.PushPopupAsync(page);
                //string myinput = await TutorialPage(this.Navigation);

                // StartTutorial();
            }
            //edw stamataei k perimenei
            await Task.Run(() => waitHandle.WaitOne());
            //edw stamataei k perimenei
              longpress = false;


            //    var x = sender as Button;


        }
       
        private async void OnButtonClicked(object sender, EventArgs e)
        {
            //  ((sender) as Button).StartDrag(data, new View.DragShadowBuilder(((sender) as Button)), null, 0);
            if (combineActive)
            {
               
                foreach (RadButton btn in buttonstomerge)
                {
                    if(btn.ClassId == ((RadButton)sender).ClassId)
                    {
                        var sl2 = btn.Parent as StackLayout; //remove to + kai afairesh kai apo thn list buttonstomerge
                        
                        sl2.Children.RemoveAt(sl2.Children.Count-1);
                        buttonstomerge.Remove(btn);
                        return;
                    }
                   
                }
                buttonstomerge.Add((RadButton)sender);
                var button = (Xamarin.Forms.Button)sender;
                

                var view = new ContentView
                {
                    Margin = new Thickness(80, -55, 0, 0),

                    Content = new RadButton
                    {
                        Text = "+",
                        TextColor = Color.White,
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Start,
                        VerticalContentAlignment=TextAlignment.Start,
                        CornerRadius = 60,
                        WidthRequest = 25,
                        HeightRequest = 25,
                        BackgroundColor = Color.ForestGreen,
                        IsEnabled = false,
                        FontFamily = "Sans-serif",
                        FontAttributes = FontAttributes.Bold

                    },
                };

                var sl = button.Parent as StackLayout;
                sl.Children.Add(view);
             
            }
            else
            {


                if (longpress)
                {
                    return;
                }
                
                var data = Xamarin.Forms.Application.Current.Properties;

                var button = (Xamarin.Forms.Button)sender;
            
                var classId = button.ClassId;
                var q = data[classId + "food"].ToString();
                FoodModel foodClicked = JsonConvert.DeserializeObject<FoodModel>(q);
                //show times pressed
                foodClicked.TimesPressed += 1;
                if (!foodClicked.isPartOfMeal)
                {
                    ShowTimesPressed(foodClicked, button);
                }
                if (Xamarin.Forms.Application.Current.Properties["mealviewedtutorial1"].ToString() != "ok" && !foodClicked.isPartOfMeal)
                {
                    var rdbutton = (RadButton)sender;
                   //var qq =  rdbutton.Parent.GetPropertyIfSet(Grid.RowProperty,false);
                    int x = Grid.GetRow(rdbutton.Parent);
                    int y = Grid.GetColumn(rdbutton.Parent);

                    var page = new MealTutorialView1(x,y);
                    await Navigation.PushPopupAsync(page);
                    //string myinput = await TutorialPage(this.Navigation);

                    // StartTutorial();
                }


                //add cal
                var cal = data["todayscal"] = Convert.ToDouble(data["todayscal"].ToString()) + foodClicked.Calories;
                var fat = data["todaysfat"] = Convert.ToDouble(data["todaysfat"].ToString()) + foodClicked.Fat;
                var prot = data["todaysprot"] = Convert.ToDouble(data["todaysprot"].ToString()) + foodClicked.Protein;
                var carb = data["todayscarb"] = Convert.ToDouble(data["todayscarb"].ToString()) + foodClicked.Carbs;

                UpdateCalLabel(cal,carb,prot,fat);
                //if (i == 0) //first time press
                //{
                //    data["todayspressedlist"] += "[{ \"" + "name" + "\": \"" + data[classId + "foodname"].ToString() + "\"," + "\"" + "timespressed" + "\": \"" + (i + 1).ToString() + "\"," + "\"" + "datetimepressed" + "\": \"" + DateTime.Now.TimeOfDay.ToString() + "\"}]";
                //    //data["todayspressedlist"] += "{ " + "name" + ": " + data[classId + "foodname"].ToString() + "," + "" + "timespressed" + ": " + (i + 1).ToString() + "," + "" + "datetimepressed" + ":" + DateTime.Now.TimeOfDay.ToString() + "},";

                //}

                data["todayspressedlist"] += classId + ",";
                q = JsonConvert.SerializeObject(foodClicked);
                data[foodClicked.ClassId + "food"] = q;
                //string x = (data["todayspressedlist"].ToString());
                //var parsedObject = JArray.Parse(x);
                //var json2 = parsedObject.ToString();

                // var playerList = JsonConvert.DeserializeObject<IEnumerable<TimesPressedJsonModel>>(json2);


                await Xamarin.Forms.Application.Current.SavePropertiesAsync();
                //var alert = new PopupAlert("Calories: " + data["todayscal"].ToString() +" Fat: " + data["todaysfat"].ToString() + " Prot: " + data["todaysprot"].ToString() + " Carb: " + data["todayscarb"].ToString());
                //await Navigation.PushPopupAsync(alert);

            }
        }
        public void ShowTimesPressed(FoodModel food, Xamarin.Forms.Button button)
        {
            //var data = Application.Current.Properties;

            //int.TryParse(classId, out int no);

            //int.TryParse(data[no + "foodtimespressed"].ToString(), out int i);
            int i = food.TimesPressed;
            int widthrequest = 10;
            if (i >= 10)
            {
                widthrequest = 20;
            }
            var view = new ContentView
            {
                Margin = new Thickness(80, -15, 0, 0),

                Content = new RadButton
                {
                    Text = (i).ToString(),
                    TextColor = Color.Black,
                    FontSize = 12,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,
                    CornerRadius = 60,
                    WidthRequest = widthrequest,
                    HeightRequest = 15,
                    BackgroundColor = Color.SandyBrown,
                    IsEnabled = false,
                    FontFamily= "Sans-serif",
                    FontAttributes=FontAttributes.Bold
                    
                },
            };

            var sl = button.Parent as StackLayout;
            if (sl == null)
            {
                UpdateAllButtons();
                return;
            }
            if (sl.Children.Count == 1)
            {
                sl.Children.Add(view);
            }
            else
            {
                sl.Children[1] = view;
                if (i == 0)
                {
                    sl.Children.RemoveAt(1);
                }
            }
        }
        public void ShowTimesMealPressed(int timesPressed, Xamarin.Forms.Button button)
        {
            //var data = Application.Current.Properties;

            //int.TryParse(classId, out int no);

            //int.TryParse(data[no + "foodtimespressed"].ToString(), out int i);
            int i = timesPressed;
            int widthrequest = 10;
            if (i >= 10)
            {
                widthrequest = 20;
            }
            var view = new ContentView
            {
                Margin = new Thickness(80, -15, 0, 0),

                Content = new RadButton
                {
                    Text = (i).ToString(),
                    TextColor = Color.Black,
                    FontSize = 12,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,
                    CornerRadius = 60,
                    WidthRequest = widthrequest,
                    HeightRequest = 15,
                    BackgroundColor = Color.SandyBrown,
                    IsEnabled = false,
                    FontFamily = "Sans-serif",
                    FontAttributes = FontAttributes.Bold

                },
            };

            var sl = button.Parent as StackLayout;
            if (sl == null)
            {
                UpdateAllButtons();
                return;
            }
            if (sl.Children.Count == 1)
            {
                sl.Children.Add(view);
            }
            else
            {
                sl.Children[1] = view;
                if (i == 0)
                {
                    sl.Children.RemoveAt(1);
                }
            }

        }


        public async void AddButtonWindow(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopup();
            await Navigation.PushPopupAsync(loadingPage);
            CheckTutorial4();
            string myinput = await AddCustomButton(this.Navigation, loadingPage);
            if (myinput == "ok")
            {

            }
        }
        private bool combineActive=false; 
        private void CancelCombine(object sender, EventArgs e)
        {
            combineActive = false;
            //CancelMerge.IsVisible = false;

            foreach (RadButton btn in buttonstomerge)
            {
                var sl = btn.Parent as StackLayout;
                sl.Children.RemoveAt(sl.Children.Count - 1);


            }
            foreach (RadButton btn in buttonlist)
            {
                if (btn.BackgroundColor == Color.Bisque) {
                    btn.BackgroundColor = Color.DarkTurquoise;
                }else if (btn.BackgroundColor == Color.DimGray)
                {
                    btn.BackgroundColor = Color.MediumVioletRed;

                }
            }
        }
            public void StartCombine(object sender, EventArgs e)
        {
            combineActive = !combineActive;
            MainPage.longpressFab = !MainPage.longpressFab;
            if (combineActive) //ksekinise to combine
            {
                //CancelMerge.IsVisible = true;
                buttonstomerge = new List<RadButton>();
                foreach (RadButton btn in buttonlist)
                {
                    if (btn.BackgroundColor == Color.DarkTurquoise)
                    {
                        btn.BackgroundColor = Color.Bisque;
                    }
                    else if (btn.BackgroundColor == Color.MediumVioletRed)
                    {
                        btn.BackgroundColor = Color.DimGray;

                    }
                }
            }
            else // prepei 3edw na ginei to merge + ola ayta pou kanei kai to cancel combine
            {
                //PROSOXI EXW BALEI NA GINONTAI DISABLE TA MEAL KOUMPIA ME VASEI TO XRWMA
                //PSAKSE REFERENCE TOU buttonslist GIA NA TO DEIS
                CheckTutorialLast();
                if (buttonstomerge.Count == 0 || buttonstomerge.Count == 1 )
                {
                    if (buttonstomerge.Count == 1)
                    {
                        var sl = (buttonstomerge[0].Parent as StackLayout);
                        sl.Children.RemoveAt(sl.Children.Count - 1);
                    }
                    foreach (RadButton btn in buttonlist)
                    {

                        if (btn.BackgroundColor == Color.Bisque)
                        {
                            btn.BackgroundColor = Color.DarkTurquoise;
                        }
                        else if (btn.BackgroundColor == Color.DimGray)
                        {
                            btn.BackgroundColor = Color.MediumVioletRed;

                        }
                    }
                    return;
                }
                var data = Xamarin.Forms.Application.Current.Properties;
                var noOfMeals = Convert.ToInt32(data["noOfMeals"].ToString()) + 1;
                data[noOfMeals.ToString() + "Meal"] = "";
                var meal = new MealModel(timofday,"", noOfMeals,0);
               

                foreach (var btn in buttonstomerge)
                {

                    var classId = btn.ClassId;
                    var q = data[classId + "food"].ToString();
                    FoodModel foodmerged = JsonConvert.DeserializeObject<FoodModel>(q);
                    foodmerged.isPartOfMeal = true;
                    foodmerged.MealNo = noOfMeals;
                    q = JsonConvert.SerializeObject(foodmerged);
                    data[foodmerged.ClassId + "food"] = q;

                    //create meal apo dw k katw
                    meal.foodClassIds.Add(Convert.ToInt32(classId));
                    if (meal.MealName == "")
                    {
                        meal.MealName += foodmerged.Name;
                    }
                    else
                    {
                        meal.MealName +=" & " + foodmerged.Name;

                    }
                    //data[noOfMeals.ToString() + "Meal"] += classId.ToString() + ","; 
                }
                data["noOfMeals"] = noOfMeals;
                var qq = JsonConvert.SerializeObject(meal);
                data[noOfMeals.ToString() + "Meal"] = qq;
                Xamarin.Forms.Application.Current.SavePropertiesAsync();


                buttonstomerge.Clear();
                //foreach (RadButton btn in buttonstomerge)
                //{
                //    var sl = btn.Parent as StackLayout;
                //    var x = sl.Parent;
                //    sl.Children.RemoveAt(sl.Children.Count - 1);
                //    buttonlist.Remove(btn);


                //}
                //foreach (RadButton btn in buttonlist)
                //{
                //    btn.BackgroundColor = Color.DarkTurquoise;
                //}

                //merge
                UpdateAllButtons();

            }
        }
        public void UpdateCalLabel(object cal, object carb,object prot, object fat)
        {
            CalInfoLabel.Text = "Calories: " + cal.ToString() + " Carbs: " + carb.ToString() + " Protein: " + prot.ToString() + " Fat: " + fat.ToString();
        }
        public void UpdateCalLabelGoal(object cal, object carb, object prot, object fat)
        {
            CalInfoLabel.Text = "Goal Calories: " + cal.ToString() + " Goal Carbs: " + carb.ToString() + " Goal Protein: " + prot.ToString() + " Goal Fat: " + fat.ToString();
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
            btnOk.Clicked += async (s, e) =>
            {
                // close page

                await navigation.RemovePopupPageAsync(page);
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

            var carview = new CreateCustomButton(timofday);

            var layout = new StackLayout
            {
              //  Children = { carview.Content, slButtons }
                Children = { carview.Content }

            };
            page.Content = layout;
            navigation.RemovePopupPageAsync(loadingPage);
            navigation.PushPopupAsync(page);
           
            // open keyboard
            //txtInput.Focus();

            // code is waiting her, until result is passed with tcs.SetResult() in btn-Clicked
            // then proc returns the result
            return tcs.Task;
        }

        #region Tutorials
        private async void CheckTutorialLast()
    {
            var data = Xamarin.Forms.Application.Current.Properties;
            if (data["mealviewedtutoriallast"].ToString() != "ok" && data["mealviewedtutorial9"].ToString() == "ok")
            {

                //var qq =  rdbutton.Parent.GetPropertyIfSet(Grid.RowProperty,false);


                var page = new MealTutorialViewLast();
                await Navigation.PushPopupAsync(page);
                //string myinput = await TutorialPage(this.Navigation);

                // StartTutorial();
            }
        }
    private async void CheckTutorial4()
    {
            if (Xamarin.Forms.Application.Current.Properties["mealviewedtutorial4"].ToString() != "ok" && Xamarin.Forms.Application.Current.Properties["mealviewedtutorial3"].ToString() == "ok")
            {
                await Task.Delay(2000);
                var page2 = new MealTutorialView4();
               await Navigation.PushPopupAsync(page2);
            }
        }

        private void CalLabelTapped(object sender, EventArgs e)
        {
            var data = Xamarin.Forms.Application.Current.Properties;

            if (!CalInfoLabel.Text.Contains("Goal"))
            {

                


                if (data["goalset"].ToString() == "false")
                {
                    CalInfoLabel.Text = "Goals not set!";
                }
                else
                {
                    var cal = Convert.ToDouble(data["goalcal"].ToString());
                    var fat = Convert.ToDouble(data["goalfat"].ToString());
                    var prot = Convert.ToDouble(data["goalprot"].ToString());
                    var carb = Convert.ToDouble(data["goalcarb"].ToString());
                    UpdateCalLabelGoal(cal, carb, prot, fat);

                }
            }
            else
            {
                var cal = Convert.ToDouble(data["todayscal"].ToString());
                var fat = Convert.ToDouble(data["todaysfat"].ToString());
                var prot = Convert.ToDouble(data["todaysprot"].ToString());
                var carb = Convert.ToDouble(data["todayscarb"].ToString());

                UpdateCalLabel(cal, carb, prot, fat);
            }
        }

        private async void CheckTutorial()
        {
            if (Xamarin.Forms.Application.Current.Properties["mealviewedtutorial"].ToString() != "ok")
            {
                await Task.Delay(500);
                var page = new MealTutorialView();

                await Navigation.PushPopupAsync(page);
                //string myinput = await TutorialPage(this.Navigation);

                // StartTutorial();
            }
        }
        //public Task<string> TutorialPage(INavigation navigation )
        //{

        //    //apo edw kai katw gia na ginei to return
        //    var tcs = new TaskCompletionSource<string>();
        //    var page = new PopupPage();



        //    var btnOk = new RadButton
        //    {
        //        Text = "Done",
        //        WidthRequest = 100,
        //        BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8),
        //    };
        //    btnOk.Clicked += async (s, e) =>
        //    {
        //        // close page

        //        await navigation.RemovePopupPageAsync(page);
        //        // pass result
        //        tcs.SetResult("ok");
        //    };
        //    var slButtons = new StackLayout
        //    {

        //        Children = { btnOk },
        //        //Children = { btnOk, btnCancel },
        //    };
        //    //apo edw kai panw gia na ginei to return







        //    //var layout = new StackLayout
        //    //{

        //    //    VerticalOptions = LayoutOptions.CenterAndExpand,
        //    //    HorizontalOptions = LayoutOptions.CenterAndExpand,
        //    //    Orientation = StackOrientation.Vertical,
        //    //    //Children = { lblTitle, lblMessage, carview, slButtons },
        //    //    Children = {  carview },

        //    //};

        //    // create and show page
        //    //var page = new ContentPage();
        //    //page.Content = layout;

        //    var carview = new MealTutorialView();

        //    var layout = new StackLayout
        //    {
        //        //  Children = { carview.Content, slButtons }
        //        Children = { carview.Content }

        //    };
        //    page.Content = layout;
        //    navigation.PushPopupAsync(page);
        //    // open keyboard
        //    //txtInput.Focus();

        //    // code is waiting her, until result is passed with tcs.SetResult() in btn-Clicked
        //    // then proc returns the result
        //    return tcs.Task;
        //}
        #endregion
    }
}