using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using App3.ViewModels;
using Newtonsoft.Json;
using App3.Models;
using App3.Views.Popups.CustomAlerts;
using System.Collections.Generic;
using static App3.Models.FoodModel;

namespace App3.Views
{
    public partial class FoodInfo : PopupPage
    {
        FoodInfoViewModel viewModel;
        private FoodInfoModel foodObj;
        private Button btn;
        #region animations
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();

            FrameContainer.HeightRequest = -1;

            if (!IsAnimationEnabled)
            {
                CloseImage.Rotation = 0;
                CloseImage.Scale = 1;
                CloseImage.Opacity = 1;

                FoodCal.Scale = 1;
                FoodCal.Opacity = 1;
                FoodCarb.Scale = 1;
                FoodCarb.Opacity = 1;
                MoreInfo.Scale = 1;
                MoreInfo.Opacity = 1;
                FoodProt.Scale = 1;
                FoodProt.Opacity = 1;
                FoodFat.Scale = 1;
                FoodFat.Opacity = 1;
                TimesPressed.Scale = 1;
                TimesPressed.Opacity = 1;
                MoreInfo.Scale = 1;
                MoreInfo.Opacity = 1;
                IsCustom.Scale = 1;
                IsCustom.Opacity = 1;


                FoodName.TranslationX = 0;
                FoodName.Opacity = 1;

                return;
            }

            CloseImage.Rotation = 30;
            CloseImage.Scale = 0.3;
            CloseImage.Opacity = 0;

            FoodCal.Scale = 0.3;
            FoodCal.Opacity = 0;
            FoodCarb.Scale = 0.3;
            FoodCarb.Opacity = 0;
            FoodProt.Scale = 0.3;
            FoodProt.Opacity = 0;
            FoodFat.Scale = 0.3;
            FoodFat.Opacity = 0;
            TimesPressed.Scale = 0.3;
            TimesPressed.Opacity = 0;
            MoreInfo.Scale = 0.3;
            MoreInfo.Opacity = 0;
            IsCustom.Scale = 0.3;
            IsCustom.Opacity = 0;

            MoreInfo.Scale = 0.3;
            MoreInfo.Opacity = 0;

            FoodName.TranslationX = -10;
            FoodName.Opacity = 0;
        }

        protected override async Task OnAppearingAnimationEndAsync()
        {
            if (!IsAnimationEnabled)
                return;

            var translateLength = 400u;

            await Task.WhenAll(
                FoodName.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                FoodName.FadeTo(1)
                //,
                //(new Func<Task>(async () =>
                //{
                //    await Task.Delay(200);
                //    await Task.WhenAll(
                //        FoodFat.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                //        FoodFat.FadeTo(1));

                //}))()
                );

            await Task.WhenAll(
                CloseImage.FadeTo(1),
                CloseImage.ScaleTo(1, easing: Easing.SpringOut),
                CloseImage.RotateTo(0),
                FoodCal.ScaleTo(1),
                FoodCal.FadeTo(1),
                  FoodCarb.ScaleTo(1),
                FoodCarb.FadeTo(1),
                 FoodProt.ScaleTo(1),
                FoodProt.FadeTo(1),
                 FoodFat.ScaleTo(1),
                FoodFat.FadeTo(1),
                 TimesPressed.ScaleTo(1),
                TimesPressed.FadeTo(1),
                 IsCustom.ScaleTo(1),
                IsCustom.FadeTo(1),
                   MoreInfo.ScaleTo(1),
                MoreInfo.FadeTo(1));
        }

        protected override async Task OnDisappearingAnimationBeginAsync()
        {
            if (!IsAnimationEnabled)
                return;

            var taskSource = new TaskCompletionSource<bool>();

            var currentHeight = FrameContainer.Height;

            await Task.WhenAll(
                FoodCal.FadeTo(0),
                //FoodFat.FadeTo(0),
                FoodName.FadeTo(0));

            FrameContainer.Animate("HideAnimation", d =>
            {
                FrameContainer.HeightRequest = d;
            },
            start: currentHeight,
            end: 55,
            finished: async (d, b) =>
            {
                await Task.Delay(60);
                taskSource.TrySetResult(true);
            });

            await taskSource.Task;
        }

        private async void OnLogin(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopup();
            await Navigation.PushPopupAsync(loadingPage);
            await Task.Delay(2000);
            await Navigation.RemovePopupPageAsync(loadingPage);
            //   await Navigation.PushPopupAsync(new LoginSuccessPopupPage());
        }

        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
        #endregion
        public FoodInfo(FoodInfoModel foodobject, Button button,int timesPressed,bool hideDelBtn = false)
        {
            var data = Application.Current.Properties;
            var qqq = foodobject.IsMeal;
            InitializeComponent();
            foodObj = foodobject;
            foodobject.TimesPressed = timesPressed.ToString();
            btn = button;
            if (!foodObj.IsMeal)
            {
                UndoButton.IsVisible = false;
            }
            if(btn == null)
            {
                RemoveEntryButton.IsVisible = false;
                EditNameButton.IsVisible = false;
                ChangeMealTime.IsVisible = false;
                UndoButton.IsVisible = false;
                //FoodCal.Text = foodObj.FoodCal;
                //FoodFat.Text = foodObj.FoodFat;
                //FoodCarb.Text = foodObj.FoodCarb;
                //FoodProt.Text = foodObj.FoodProt;
                //IsCustom.Text = foodObj.FoodPortion;

                FoodCal.Text = "Calories: " + foodobject.FoodCal + "kcal";
                FoodCarb.Text = "Carbs: " + foodobject.FoodCarb + "g";
                FoodFat.Text = "Fat: " + foodobject.FoodFat + "g";
                FoodProt.Text = "Protein: " + foodobject.FoodProt + "g";

                IsCustom.Text = "Food Portion: " + foodobject.IsCustomButton;
               

            }
            else if (timesPressed == 0)
            {
                RemoveEntryButton.IsVisible = false;
                //RemoveEntryBoxView.IsVisible = false;
            }
            if (hideDelBtn)
            {
                DeleteButon.IsVisible = false;
                RemoveEntryButton.IsVisible = false;
                //RemoveEntryBoxView.IsVisible = false;
            }
            BindingContext = viewModel = new FoodInfoViewModel(foodobject);
            //var swp = new SwipeGestureRecognizer();
            //swp.Swiped += SwipeGestureRecognizer_Swiped;
            //(this as PopupPage).Content.GestureRecognizers.Add(swp);
            //var leftSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
            //leftSwipeGesture.Swiped += SwipeGestureRecognizer_Swiped;
            //sl.GestureRecognizers.Add(leftSwipeGesture);



        }
      
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        protected async override void OnDisappearing()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var data = Application.Current.Properties;

            if (data["mealviewedtutorial3"].ToString() != "ok" && data["mealviewedtutorial2"].ToString() == "ok")
            {
                MealView.Invoker.StartTut3();
            }
            base.OnDisappearing();

        }
       
        private async void RemoveButtonEntry(object sender, EventArgs e)
        {
            if (!foodObj.IsMeal)
            {
                var data = Application.Current.Properties;
                var q = data[foodObj.ClassId + "food"].ToString();
                var foodtoget = JsonConvert.DeserializeObject<FoodModel>(q);
                if (foodtoget.TimesPressed != 0)
                {
                    var cal = data["todayscal"] = Convert.ToDouble(data["todayscal"].ToString()) - foodtoget.Calories;
                    var fat = data["todaysfat"] = Convert.ToDouble(data["todaysfat"].ToString()) - foodtoget.Fat;
                    var prot = data["todaysprot"] = Convert.ToDouble(data["todaysprot"].ToString()) - foodtoget.Protein;
                    var carb = data["todayscarb"] = Convert.ToDouble(data["todayscarb"].ToString()) - foodtoget.Carbs;
                    MealView.Invoker.UpdateCalLabel(cal, carb, prot, fat);
                    foodtoget.TimesPressed -= 1;
                    if (foodtoget.TimesPressed == 0)
                    {
                        RemoveEntryButton.IsVisible = false;
                        //RemoveEntryBoxView.IsVisible = false;
                        TimesPressed.Text = "";

                        //afairesh apo calories, update ta counter buttons sto view
                    }
                    else
                    {
                        TimesPressed.Text = "Times Pressed: " + foodtoget.TimesPressed;

                    }
                    q = JsonConvert.SerializeObject(foodtoget);
                    data[foodtoget.ClassId + "food"] = q;
                    MealView.Invoker.ShowTimesPressed(foodtoget, btn);
                    await Application.Current.SavePropertiesAsync();
                }
            }
            else
            {
                var data = Application.Current.Properties;
                foodObj.ClassId= foodObj.ClassId.Replace("Meal","");
                var q = data[foodObj.ClassId + "Meal"].ToString();
                var foodtoget = JsonConvert.DeserializeObject<MealModel>(q);
                if (foodtoget.TimesPressed != 0)
                {
                    foodtoget.TimesPressed -= 1;
                    foreach (var realfood in foodtoget.foodClassIds)
                    {
                        var fooddes = JsonConvert.DeserializeObject<FoodModel>(data[realfood+"food"].ToString());
                        var cal = data["todayscal"] = Convert.ToDouble(data["todayscal"].ToString()) - fooddes.Calories;
                        var fat = data["todaysfat"] = Convert.ToDouble(data["todaysfat"].ToString()) - fooddes.Fat;
                        var prot = data["todaysprot"] = Convert.ToDouble(data["todaysprot"].ToString()) - fooddes.Protein;
                        var carb = data["todayscarb"] = Convert.ToDouble(data["todayscarb"].ToString()) - fooddes.Carbs;
                        fooddes.TimesPressed -= 1;
                        var qqq = JsonConvert.SerializeObject(fooddes);
                        data[realfood + "food"] = qqq;
                        MealView.Invoker.UpdateCalLabel(cal, carb, prot, fat);

                    }
                  
                    if (foodtoget.TimesPressed == 0)
                    {
                        RemoveEntryButton.IsVisible = false;
                        //RemoveEntryBoxView.IsVisible = false;
                        TimesPressed.Text = "";

                        //afairesh apo calories, update ta counter buttons sto view
                    }
                    else
                    {
                        TimesPressed.Text = "Times Pressed: " + foodtoget.TimesPressed;

                    }
                    q = JsonConvert.SerializeObject(foodtoget);
                    data[foodObj.ClassId + "Meal"] = q;
                    MealView.Invoker.ShowTimesMealPressed(foodtoget.TimesPressed, btn);
                    await Application.Current.SavePropertiesAsync();
                }
            }
           
            
        }

        private async void DeleteButon_Clicked(object sender, EventArgs e)
        {
            var customalert = new DeleteButtonPopupAlert("This entry will be deleted permanently.\nContinue?", this);
            await Navigation.PushPopupAsync(customalert);




        }
        public async void DeleteConfirmed()
        {
            var data = Application.Current.Properties;
            var x = foodObj.ClassId;
            if (x.Contains("Meal"))
            {
                x = x.Replace("Meal", "");
                var q = data[x + "Meal"].ToString();
                MealModel foodClicked = JsonConvert.DeserializeObject<MealModel>(q);
                foreach (var fod in foodClicked.foodClassIds)
                {
                    data["removedClassIds"] += fod + ",";
                    data.Remove(fod + "food");
                  
                }
                data["removedMeals"] += x + ",";
                data.Remove(x + "Meal");                
            }
            else
            {
                data["removedClassIds"] += x + ",";
                data.Remove(x + "food");
                // (btn.Parent as StackLayout).Children.Clear();
            }
            MealView.Invoker.UpdateAllButtons();
            await Application.Current.SavePropertiesAsync();

            OnCloseButtonTapped(null, null); //close page
        }

        private void EditNameButton_Clicked(object sender, EventArgs e)
        {
            FoodName.IsVisible = false;
            FoodEntry.IsVisible = true;
            FoodEntry.Focus();
        }

        private async void FoodEntry_Unfocused(object sender, FocusEventArgs e) //change name
        {
            if (FoodEntry.Text!="" && FoodEntry.Text != null)
            {
                FoodName.Text = FoodEntry.Text;
                var x = foodObj.ClassId;
                if (x.Contains("Meal"))
                {
                    x = x.Replace("Meal","");
                    var data = Application.Current.Properties;
                    var q = data[x + "Meal"].ToString();
                    MealModel foodClicked = JsonConvert.DeserializeObject<MealModel>(q);
                    foodClicked.MealName = FoodName.Text;
                    q = JsonConvert.SerializeObject(foodClicked);
                    data[x + "Meal"] = q;
                    await Application.Current.SavePropertiesAsync();
                }
                else
                {
                    var data = Application.Current.Properties;
                    var q = data[x + "food"].ToString();
                    FoodModel foodClicked = JsonConvert.DeserializeObject<FoodModel>(q);
                    foodClicked.Name = FoodName.Text;
                    q = JsonConvert.SerializeObject(foodClicked);
                    data[x + "food"] = q;
                    await Application.Current.SavePropertiesAsync();
                }
                
                MealView.Invoker.UpdateAllButtons();
            }


            FoodName.IsVisible = true;
            FoodEntry.IsVisible = false;

        }

        private void ChangeMealTime_Clicked(object sender, EventArgs e)
        {
            var x = new List<string>();
          x.Add("Breakfast");
          x.Add("Brunch");
          x.Add("Lunch");
          x.Add("Supper");
          x.Add("Dinner");
          x.Add("Extra");
            TimeOfDayPicker.ItemsSource = x;
            TimeOfDayPicker.Focus();
        }

        private async void ChangeTimeOfDay(object sender, FocusEventArgs e)
        {
            if (TimeOfDayPicker.SelectedIndex == -1)
            {
                return;
            }
            var tim = (TimeOfDayEmum)TimeOfDayPicker.SelectedIndex;
            var x = foodObj.ClassId;
            if (tim.ToString() == foodObj.TimeOfDay)
            {
                return;
            }
           
            if (x.Contains("Meal"))
            {
                x = x.Replace("Meal", "");
                var data = Application.Current.Properties;
                var q = data[x + "Meal"].ToString();
                MealModel foodClicked = JsonConvert.DeserializeObject<MealModel>(q);
                foodClicked.TimeOfDay = tim;
                q = JsonConvert.SerializeObject(foodClicked);
                data[x + "Meal"] = q;
                await Application.Current.SavePropertiesAsync();
            }
            else
            {
                var data = Application.Current.Properties;
                var q = data[x + "food"].ToString();
                FoodModel foodClicked = JsonConvert.DeserializeObject<FoodModel>(q);
                foodClicked.TimeOfDay = tim;
                q = JsonConvert.SerializeObject(foodClicked);
                data[x + "food"] = q;
                await Application.Current.SavePropertiesAsync();
            }

            MealView.Invoker.UpdateAllButtons();
            CloseAllPopup();
        }

        private async void UndoButton_Clicked(object sender, EventArgs e)
        {
            var data = Application.Current.Properties;
            var x = foodObj.ClassId;
            if (x.Contains("Meal"))
            {
                x = x.Replace("Meal", "");
                data["removedMeals"] += x+",";
             
                var q = data[x + "Meal"].ToString();
                MealModel foodClicked = JsonConvert.DeserializeObject<MealModel>(q);
                foreach (var fod in foodClicked.foodClassIds)
                {
                    var qq = data[fod + "food"].ToString();
                    FoodModel foodClicked2 = JsonConvert.DeserializeObject<FoodModel>(qq);
                    foodClicked2.isPartOfMeal = false;
                    qq = JsonConvert.SerializeObject(foodClicked2);
                    data[fod + "food"] = qq;
                }
                foodClicked.foodClassIds.Clear();
         
                data.Remove(x + "Meal");
                await Application.Current.SavePropertiesAsync();
            }
            else
            {
                //nothing
            }
            MealView.Invoker.UpdateAllButtons();
            CloseAllPopup();
        }
    }

    public class FoodInfoModel
    {
        public string FoodName { get; set; }
        public string TimeOfDay { get; set; }
        public string FoodCal { get; set; }
        public string FoodCarb { get; set; }
        public string FoodProt { get; set; }
        public string FoodFat { get; set; }
        public string ClassId { get; set; }
        public string IsCustomButton { get; set; }
        public string MoreInfo { get; set; }
        public string FoodPortion { get; set; }

        public string TimesPressed { get; set; }
        public bool IsMeal { get; set; }


    }
}
