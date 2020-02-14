using App3.Models;
using App3.Views.Tutorials;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.Chart;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverviewView : ContentPage
    {
        //private Color _legendcolor;
        //public Color LegendColor
        //{
        //    get
        //    {
        //        return _legendcolor;
        //    }
        //    set
        //    {
        //        _legendcolor = value;
        //        OnPropertyChanged();
        //    }
        //}
        public OverviewView()
        {

            InitializeComponent();
            //BarSeries5.ItemsSource = GetCategoricalData();



            ChartPalette palette = new ChartPalette();
            palette.Entries.Add(new PaletteEntry() { FillColor = Color.FromRgb(20, 216, 0), StrokeColor = Color.FromRgb(14, 152, 0) });

            palette.Entries.Add(new PaletteEntry() { FillColor = Color.FromRgb(246, 0, 29), StrokeColor = Color.FromRgb(178, 0, 21) });
            palette.Entries.Add(new PaletteEntry() { FillColor = Color.FromRgb(255, 206, 0), StrokeColor = Color.FromRgb(186, 150, 0) });
            palette.Entries.Add(new PaletteEntry() { FillColor = Color.FromRgb(0, 0, 0), StrokeColor = Color.FromRgb(14, 152, 0) });
            palette.Entries.Add(new PaletteEntry() { FillColor = Color.FromRgb(155, 155, 0), StrokeColor = Color.FromRgb(14, 152, 0) });
            BarSeries1Main.Palette = palette;
            BarSeries2Main.Palette = palette;
            BarSeries3Main.Palette = palette;
            BarSeries4Main.Palette = palette;

            CheckTutorial10();
            ////////////
            //var date = DateTime.Today;

            //    calendar.AppointmentsSource = new ObservableCollection<Appointment>
            //{
            //    new Appointment{ Title = "Tom Birthday", IsAllDay = true, Color = Color.FromHex("#C1D8FF"), Detail ="Buy present!", StartDate = date, EndDate = date.AddHours(12)},
            //    new Appointment{ Title = "Lunch with Sara", IsAllDay = false, Color = Color.FromHex("#EDFDE3"), Detail ="Discuss the new marketing strategy", StartDate = date.AddDays(1).AddHours(12), EndDate = date.AddDays(1).AddHours(13).AddMinutes(30)},
            //    new Appointment{ Title = "Security Training", IsAllDay = false, Color = Color.FromHex("#EDFDE3"), Detail ="Details for the event come here", StartDate = date.AddHours(15), EndDate = date.AddHours(16)},
            //    new Appointment{ Title = "Elle Birthday", IsAllDay = true, Color = Color.FromHex("#FFF1F9"), Detail ="Buy present!", StartDate = date.AddDays(1), EndDate = date.AddDays(1).AddHours(12)},
            //    new Appointment{ Title = "One to One Meeting", IsAllDay = false, Color = Color.FromHex("#EBF2FD"), Detail ="Details for the event come here - for example place, participants and add information", StartDate = date.AddHours(16), EndDate = date.AddHours(17)},
            //    new Appointment{ Title = "Marathon", IsAllDay = false, Color = Color.FromHex("#FDE2AC"), Detail ="Enjoy running", StartDate = date.AddHours(8), EndDate = date.AddHours(11)},
            //};
            //    calendar.AppointmentTapped += (sender, e) =>
            //    {
            //        Application.Current.MainPage.DisplayAlert(e.Appointment.Title, e.Appointment.Detail, "OK");
            //    };


            //BarSeries4.LegendTitleBinding = PropertyNameDataPointBinding.ClassIdProperty.DefaultBindingMode
            // BarSeries5.ItemsSource = GetCategoricalData();
            //BarSeries6.DataSource = GetCategoricalData();
            // BarSeries7.ItemsSource = GetCategoricalData();
            // BarSeries3.ItemsSource = GetCategoricalData();

            var leftSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
            leftSwipeGesture.Swiped += SwipeGestureRecognizer_SwipedLeft;
            OverviewMainGrid.GestureRecognizers.Add(leftSwipeGesture);
            OverviewMainGrid2.GestureRecognizers.Add(leftSwipeGesture);

            var rightSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Right };
            rightSwipeGesture.Swiped += SwipeGestureRecognizer_SwipedRight;
            OverviewMainGrid.GestureRecognizers.Add(rightSwipeGesture);
            OverviewMainGrid2.GestureRecognizers.Add(rightSwipeGesture);

        }


        private async void CheckTutorial10()
        {

            if (Application.Current.Properties["mealviewedtutorial10"].ToString() != "ok")
            {
                await Task.Delay(500);
                var page2 = new MealTutorialView10();
                await Navigation.PushPopupAsync(page2);
            }
        }



        private static ObservableCollection<CategoricalData> GetTodaysData(string type)
        {
            var data2 = Application.Current.Properties;
            double goal = 0;
            goal = Convert.ToDouble(data2["goal" + type].ToString());




            var data = new ObservableCollection<CategoricalData>  {
            new CategoricalData { Category = type, Value = Convert.ToDouble(data2["todays"+type].ToString()) },
            new CategoricalData { Category = "Goal "+type, Value = goal-Convert.ToDouble(data2["todays"+type].ToString()) },


        };
            if (data[1].Value < 0)
            {
                data[1].Value = 0;
            }
            return data;
        }
        private static ObservableCollection<CategoricalData> GetOldData(string type, int day)
        {
            var data2 = Application.Current.Properties;
            double goal = 0;
            goal = Convert.ToDouble(data2["goal" + type].ToString());

            var data = new ObservableCollection<CategoricalData>  {
            new CategoricalData { Category = type, Value = Convert.ToDouble(data2[day+type].ToString()) },
            new CategoricalData { Category = "Goal "+type, Value = goal-Convert.ToDouble(data2[day+type].ToString()) },


        };
            if (data[1].Value < 0)
            {
                data[1].Value = 0;
            }
            return data;
        }

        private void ControlLoaded(object sender, EventArgs e)
        {
            var data = Application.Current.Properties;

            var startofday = MainPage.UnixTimeStampToDateTime(Convert.ToDouble(data["startofday"].ToString()));
            //var endofday = MainPage.UnixTimeStampToDateTime(Convert.ToDouble(data["startofday"].ToString()) + (3600 * 8));

            //starting point of calendar
            (calendar as RadCalendar).DisplayDate = DateTime.Now;
            (calendar as RadCalendar).SelectedDate = DateTime.Now;
            (calendar as RadCalendar).MinDate = startofday.Date;
            (calendar as RadCalendar).MaxDate = DateTime.Now;



            //(calendar as RadCalendar).DayViewSettings.DayStartTime = startofday.TimeOfDay;
            //(calendar as RadCalendar).DayViewSettings.DayEndTime = endofday.TimeOfDay;
            //(calendar as RadCalendar).DayViewSettings.TimelineInterval = TimeSpan.FromHours(2);



        }

        private void Calendar_SelectionChanged(object sender, Telerik.XamarinForms.Input.Calendar.CalendarSelectionChangedEventArgs<object> e)
        {
            var data = Application.Current.Properties;

            var now = DateTime.Now;
            DateTime start = (calendar as RadCalendar).MinDate;
            DateTime x = (calendar as RadCalendar).SelectedDate ?? now;

            if (x < start)
            {
                return;
            }
            if (x > DateTime.Now)
            {
                return;
            }
            if (x.Date == now.Date)
            {
                BarSeries4.ItemsSource = GetTodaysData("fat");
                BarSeries3.ItemsSource = GetTodaysData("prot");
                BarSeries2.ItemsSource = GetTodaysData("carb");
                BarSeries1.ItemsSource = GetTodaysData("cal");

                return;
            }

            if ((now.Date - x.Date).TotalDays > 0)
            {
                var q = (x.Date - start.Date).TotalDays + 1;
                int y = Convert.ToInt32(q);
                bool exists = data.Any(p => p.Key == y + "fat");

                if (!exists)
                {
                    BarSeries4.ItemsSource = null;
                    BarSeries3.ItemsSource = null;
                    BarSeries2.ItemsSource = null;
                    BarSeries1.ItemsSource = null;
                    return;
                }
                BarSeries4.ItemsSource = GetOldData("fat", y);
                BarSeries3.ItemsSource = GetOldData("prot", y);
                BarSeries2.ItemsSource = GetOldData("carb", y);
                BarSeries1.ItemsSource = GetOldData("cal", y);
            }
            else
            {
                return;
            }
        }
        private DateTime lastdateselected=DateTime.MinValue;
        private async void Calendar_CellTapped(object sender, CalendarCell e)
        {

            var x = e as CalendarDateCell;
            if (x != null)
            {
                if (lastdateselected.Date != x.Date)
                {
                    lastdateselected = x.Date;
                    return;
                }

                var loading = new LoadingPopup();
                await Navigation.PushPopupAsync(loading);
                var data = Application.Current.Properties;
                DateTime start = (calendar as RadCalendar).MinDate;
                var num = (x.Date - start.Date).TotalDays + 1;
                var food = "";

                if (x.Date == DateTime.Now.Date)
                {
                    food = data["todayspressedlist"].ToString();

                    if (food.Length == 0)
                    {
                        var alert1 = new PopupAlert("No data for selected date.");
                        await Navigation.RemovePopupPageAsync(loading);
                        await Navigation.PushPopupAsync(alert1);
                        return;
                    }
                }
                else
                {
                    //   var num = (DateTime.Now.Date - x.Date).TotalDays;
                    

                    bool exists = data.Any(p => p.Key == num+"day");
                    if (!exists)
                    {
                        var alert1 = new PopupAlert("No data for selected date.");
                        await Navigation.RemovePopupPageAsync(loading);
                        await Navigation.PushPopupAsync(alert1);
                        return;
                    }
                    else
                    {
                        food = data[num+"day"].ToString();

                    }

                }
                var list = new List<int>();
                int inde = 0;

                if (x.Date == DateTime.Now.Date)
                {

                    if (food == "")
                    {
                        var alert1 = new PopupAlert("No data for selected date.");
                        await Navigation.RemovePopupPageAsync(loading);
                        await Navigation.PushPopupAsync(alert1);
                        return;
                    }
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
                            else if (temp.Contains(","))
                            {
                                temp = "";
                                tempInt = 0;
                            }

                        }

                        if (food.Length == 0)
                        {
                            break;
                        }
                        // inde += 1;
                    } while (true);
                }
                string show = "";
                inde = 0;
                if (x.Date == DateTime.Now.Date)
                {


                    foreach (var y in list)
                    {
                        if (data["removedClassIds"].ToString().Contains(list[inde].ToString() + ","))
                        {
                            inde += 1;
                            continue;
                        }
                        var q = JsonConvert.DeserializeObject<FoodModel>(data[list[inde] + "food"].ToString());
                        if (x.Date == DateTime.Now.Date)
                        {
                            show += q.Name + " : " + q.TimesPressed + ". \n";

                        }
                        
                        //kyr edw prepei na mpei check an einai simera h oxi. 
                        //an einai pairnei to q.TimesPressed. alliws prepei na apothikeyw st telos tis meras ta TimesPressed kapou.
                        inde += 1;
                    }
                }
                else
                {
                    
                        

                        try
                        {
                            show = data[num + "day"].ToString();
                        }
                        catch
                        {
                            var alert1 = new PopupAlert("No data for selected date.");
                            await Navigation.RemovePopupPageAsync(loading);
                            await Navigation.PushPopupAsync(alert1);
                            return;
                        }



                    
                }
                var alert = new PopupAlert(show);
                await Navigation.RemovePopupPageAsync(loading);
                await Navigation.PushPopupAsync(alert);
            }

        }
        
       

        private void SwipeGestureRecognizer_SwipedLeft(object sender, SwipedEventArgs e) // means go right
        {
            //(calendar as RadCalendar).DisplayDate = DateTime.Now;
            //(calendar as RadCalendar).SelectedDate = DateTime.Now;
            //(calendar as RadCalendar).MinDate = startofday.Date;
            //(calendar as RadCalendar).MaxDate = DateTime.Now;
            if ((calendar as RadCalendar).SelectedDate?.AddDays(1) > (calendar as RadCalendar).MaxDate)
            {
                return;
            }
            (calendar as RadCalendar).SelectedDate = (calendar as RadCalendar).SelectedDate?.AddDays(1);
            (calendar as RadCalendar).DisplayDate = (calendar as RadCalendar).DisplayDate.AddDays(1);

            if ((calendar as RadCalendar).SelectedDate == DateTime.Now.Date)
            {
                clearBtn.IsVisible = true;
            }
            else
            {
                clearBtn.IsVisible = false;

            }
        }
        private void SwipeGestureRecognizer_SwipedRight(object sender, SwipedEventArgs e) // means go left
        {
            if ((calendar as RadCalendar).SelectedDate?.AddDays(-1) < (calendar as RadCalendar).MinDate)
            {
                return;
            }

            (calendar as RadCalendar).SelectedDate = (calendar as RadCalendar).SelectedDate?.AddDays(-1);
            (calendar as RadCalendar).DisplayDate = (calendar as RadCalendar).DisplayDate.AddDays(-1);

            if ((calendar as RadCalendar).SelectedDate == DateTime.Now.Date)
            {
                clearBtn.IsVisible = true;
            }
            else
            {
                clearBtn.IsVisible = false;

            }


        }

        private async void RadButton_Clicked(object sender, EventArgs e)
        {
            var al = new PopupAlert("This will reset todays calories.\nContinue?",false,false,this);
            await Navigation.PushPopupAsync(al);
        }
        public void ProceedWithClearDay()
        {
            var data = Application.Current.Properties;

            data["todayscal"] = "0";
            data["todaysfat"] = "0";
            data["todayscarb"] = "0";
            data["todaysprot"] = "0";     
            data["todayspressedlist"] = "";

            ResetTimesPressed();
            ResetTimesPressedMeal();
            Calendar_SelectionChanged(null, null);
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
    }

    public class CategoricalData
    {
        public object Category { get; set; }

        public double Value { get; set; }

        public Color LegendColor { get; set; }
    }

    //public class DayViewAppointmentTemplateSelector : DataTemplateSelector
    //{
    //    public DataTemplate AllDay { get; set; }
    //    public DataTemplate NotAllDay { get; set; }

    //    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    //    {
    //        var appointmentsTemplate = item as Appointment;

    //        if (appointmentsTemplate.IsAllDay)
    //        {
    //            return this.AllDay;
    //        }
    //        return this.NotAllDay;
    //    }


    //}
}