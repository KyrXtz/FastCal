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
    public partial class CalcCal : ContentView
    {
        public CalcCal()
        {
            InitializeComponent();
            Goalslider.IsVisible = false;
        }

        private void CalculateRMR(object sender, EventArgs e)
        {



            if (Age.Text != null && Height.Text != null && Weight.Text != null && Sex.SelectedItem != null && Age.Text != "" && Height.Text != "" && Weight.Text != "")
            {
                if (Sex.SelectedIndex == 0)
                {
                    //            (RMR)kcal / day:
                    //(males) = 9.99 x weight(kg) +6.25 x height(cm) -4.92 x age(years) +5;
                    var rmr = 9.99 * Convert.ToInt32(Weight.Text) + 6.25 * Convert.ToInt32(Height.Text) - 4.92 * Convert.ToInt32(Age.Text) + 5;
                    RMR.Text = rmr.ToString();
                    RMR.Text += " Calories.";
                }
                if (Sex.SelectedIndex == 1)
                {
                    //            (RMR)kcal / day:
                    //(females) = 9.99 x weight(kg) +6.25 x height(cm) -4.92 x age(years) -161.
                    var rmr = 9.99 * Convert.ToInt32(Weight.Text) + 6.25 * Convert.ToInt32(Height.Text) - 4.92 * Convert.ToInt32(Age.Text) - 161;
                    RMR.Text = rmr.ToString("F2");
                    RMR.Text += " Calories.";
                    myScrollView.ScrollToAsync(TDEE, ScrollToPosition.Start, true);

                }
            }
        }

        private void Activitylvl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TDEE.Text = "";
            switch (Activitylvl.SelectedIndex)
            {
                case 0:
                    InfoLabel.Text = "";
                    break;
                case 1:
                    InfoLabel.Text = " Intensive exercise for at least 20 minutes 1 to 3 times per week. This may include such things as bicycling, jogging, basketball, swimming, skating, etc.  If you do not exercise regularly, but you maintain a busy life style that requires you to walk frequently for long periods, you meet the requirements of this level.";
                    break;
                case 2:
                    InfoLabel.Text = "Intensive exercise for at least 30 to 60 minutes 3 to 4 times per week. This may include such things as bicycling, jogging, basketball, swimming, skating, etc.";
                    break;
                case 3:
                    InfoLabel.Text = "Intensive exercise for 60 minutes or greater 5 to 7 days per week.  Labor-intensive occupations also qualify for this level.  Labor-intensive occupations include construction work (brick laying, carpentry, general labor, etc.). Also farming, landscape worker or similar occupations.";
                    break;
                case 4:
                    InfoLabel.Text = "Exceedingly active and/or very demanding activities:  Examples include:  athlete with an almost unstoppable training schedule with multiple training sessions throughout the day or a very demanding job, such as shoveling coal or working long hours on an assembly line. Generally, this level of activity is very difficult to achieve.";
                    break;
            }
            CalculateTDEE(null, null);
        }
        private void CalculateTDEE(object sender, EventArgs e)
        {
            if(RMR.Text!=null && RMR.Text != "")
            {
                var rmr = Convert.ToDouble(RMR.Text.Replace(" Calories.",""));
                if(Activitylvl.SelectedIndex!=-1)
                {
                    switch (Activitylvl.SelectedIndex)
                    {
                        case 0:
                            TDEE.Text = (rmr * 1.2).ToString("F2");
                                break;
                        case 1:
                            TDEE.Text = (rmr * 1.375).ToString("F2");
                            break;
                        case 2:
                            TDEE.Text = (rmr * 1.55).ToString("F2");
                            break;
                        case 3:
                            TDEE.Text = (rmr * 1.7).ToString("F2");
                            break;
                        case 4:
                            TDEE.Text = (rmr * 1.9).ToString("F2");
                            break;
                    }
                    TDEE.Text += " Calories.";
                    Goalslider.IsVisible = true;
                    Goalslider.Value = 2;
                    myScrollView.ScrollToAsync(SetGoalLabel, ScrollToPosition.Start, true);


                }
            }
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double StepValue = 1.0;
            var newStep = Math.Round(e.NewValue / StepValue);

            Goalslider.Value = newStep * StepValue;
            var mod = 0;
            switch (Goalslider.Value)
            {
                case 0:
                    SliderTitleLabel.Text = "Lose";
                    SliderInfoLabel.Text = "500 calorie deficit.";
                    mod = -500;
                    break;
                case 1:
                    SliderTitleLabel.Text = "Lose Slowly";
                    SliderInfoLabel.Text = "250 calorie deficit.";
                    mod = -250;
                    break;
                case 2:
                    SliderTitleLabel.Text = "Maintain.";
                    SliderInfoLabel.Text = "No calorie deficit.";
                    mod = 0;
                    break;
                case 3:
                    SliderTitleLabel.Text = "Gain slowly";
                    SliderInfoLabel.Text = "250 calorie surplus.";
                    mod = 250;
                    break;
                case 4:
                    SliderTitleLabel.Text = "Gain";
                    SliderInfoLabel.Text = "500 calorie surplus.";
                    mod = 500;
                    break;
            }
            if (TDEE.Text != "" || TDEE.Text != null) {
                var tdee = Convert.ToDouble(TDEE.Text.Replace(" Calories.", ""));

                SliderGoalCalories.Text = (tdee + mod).ToString();
                SliderGoalCalories.Text+= " Calories.";
            }
        }

        private void CheckRMR(object sender, FocusEventArgs e)
        {
            if(CkEntryEmptyOrNull(Age) && CkEntryEmptyOrNull(Weight) && CkEntryEmptyOrNull(Height) && Sex.SelectedIndex!=-1)
            {
                CalculateRMR(null,null);
            }
        }
        private void CheckTDEE(object sender, FocusEventArgs e)
        {
            CalculateTDEE(null, null);
        }
        private bool CkEntryEmptyOrNull(Entry entry)
        {
            if(entry.Text!="" && entry.Text != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Activitylvl_Unfocused(object sender, FocusEventArgs e)
        {

        }
    }
}