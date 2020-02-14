using System;
using System.Collections.Generic;
using System.Text;
using static App3.Models.FoodModel;

namespace App3.Models
{
    public class MealModel
    {
        public MealModel(TimeOfDayEmum timeofday, string mealName,int mealclassId,int timesPressed)
        {
            TimeOfDay = timeofday;
            MealName = mealName;
            MealClassId = mealclassId;
            TimesPressed = timesPressed;
            foodClassIds = new List<int>();
            //Calories = cal;
            //Protein = prot;
            //Carbs = carb;
            //Fat = fat;
            //MoreInfo = moreInfo;
            //ClassId = classId;
            //TimesPressed = timesPressed;
            //Portion = portion;
            //Name = name;
            //TimeOfDay = timeofday;
            //isPartOfMeal = ispartofmeal;
            //MealNo = mealno;
        }
        public TimeOfDayEmum TimeOfDay { get; set; }
        public string MealName { get; set; }
        public List<int> foodClassIds { get; set; }
        //public string Name { get; set; }
        //public double Calories { get; set; }
        //public double Protein { get; set; }
        //public double Carbs { get; set; }
        //public double Fat { get; set; }
        //public string MoreInfo { get; set; }
        //public double Portion { get; set; }
        public int MealClassId { get; set; }
        public int TimesPressed { get; set; }
        //public TimeOfDayEmum TimeOfDay { get; set; }
        //public bool isPartOfMeal { get; set; }
        //public int MealNo { get; set; }

    }

}
