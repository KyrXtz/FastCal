using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Models
{
   public class FoodModel
    {
        public FoodModel(TimeOfDayEmum timeofday,  double cal, double carb, double fat,  double prot,string moreInfo,double portion, string name, int classId,int timesPressed, bool ispartofmeal = false,int mealno=-1)
        {
            Calories = cal;
            Protein = prot;
            Carbs = carb;
            Fat = fat;
            MoreInfo = moreInfo;
            ClassId = classId;
            TimesPressed = timesPressed;
            Portion = portion;
            Name = name;
            TimeOfDay = timeofday;
            isPartOfMeal = ispartofmeal;
            MealNo = mealno;
        }
       public enum TimeOfDayEmum
        {
            Breakfast = 0,
            Brunch = 1,
            Lunch = 2,
            Supper = 3,
            Dinner = 4,
            Extra = 5,
          
        }
        public string Name { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Carbs { get; set; }
        public double Fat { get; set; }
        public string MoreInfo { get; set; }
        public double Portion { get; set; }
        public int ClassId { get; set; }
        public int TimesPressed { get; set; }
        public TimeOfDayEmum TimeOfDay { get; set; }
        public bool isPartOfMeal { get; set; }
        public int MealNo { get; set; }
       
    }

}
