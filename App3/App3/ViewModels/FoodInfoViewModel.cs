using App3.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3.ViewModels
{
    public class FoodInfoViewModel : BaseViewModel
    {
        private string _timesPressed { get; set; }
        public string TimesPressed
        {
            get
            { return _timesPressed; }
            set
            { _timesPressed = value; OnPropertyChanged(); }
        }

        private string foodcal { get; set; }
        public string FoodCal
        {
            get
            { return foodcal; }
            set
            { foodcal = value; OnPropertyChanged(); }
        }

        private string foodcarb { get; set; }
        public string FoodCarb
        {
            get
            { return foodcarb; }
            set
            { foodcarb = value; OnPropertyChanged(); }
        }

        private string foodfat { get; set; }
        public string FoodFat
        {
            get
            { return foodfat; }
            set
            { foodfat = value; OnPropertyChanged(); }
        }

        private string foodprot { get; set; }
        public string FoodProt
        {
            get
            { return foodprot; }
            set
            { foodprot = value; OnPropertyChanged(); }
        }

        private string foodname { get; set; }
        public string FoodName
        {
            get
            { return foodname; }
            set
            { foodname = value; OnPropertyChanged(); }
        }
        private string classid { get; set; }
        public string ClassId
        {
            get
            { return classid; }
            set
            { classid = value; OnPropertyChanged(); }
        }

        private string timeofday { get; set; }
        public string TimeOfDay
        {
            get
            { return timeofday; }
            set
            { timeofday = value; OnPropertyChanged(); }
        }

        private string iscustom { get; set; }
        public string IsCustom
        {
            get
            { return iscustom; }
            set
            { iscustom = value; OnPropertyChanged(); }
        }
        private string foodportion { get; set; }
        public string FoodPortion
        {
            get
            { return foodportion; }
            set
            { foodportion = value; OnPropertyChanged(); }
        }
        private string moreinfo { get; set; }

        public string MoreInfo
        {
            get
            { return moreinfo; }
            set
            { moreinfo = value; OnPropertyChanged(); }
        }
        private bool hasmoreinfo { get; set; }

        public bool HasMoreInfo
        {
            get
            { return hasmoreinfo; }
            set
            { hasmoreinfo = value; OnPropertyChanged(); }
        }
        public FoodInfoViewModel(FoodInfoModel foodobject)
        {
            HasMoreInfo = false;
            FoodName = foodobject.FoodName;

            FoodCal = "Calories: " + foodobject.FoodCal + "kcal";
            FoodCarb = "Carbs: " + foodobject.FoodCarb + "g";
            FoodFat = "Fat: " + foodobject.FoodFat + "g";
            FoodProt = "Protein: " + foodobject.FoodProt + "g";
            TimeOfDay = foodobject.TimeOfDay;
            if (foodobject.MoreInfo != "")
            {
                HasMoreInfo = true;
                MoreInfo = "More Info:\n" + foodobject.MoreInfo;
            }
            // ClassId = foodobject.ClassId;
            if (foodobject.TimesPressed != "0")
            {
                TimesPressed = "Times Pressed: " + foodobject.TimesPressed;
            }
            
           
            IsCustom = "Food Portion: " +  foodobject.IsCustomButton +" g";


        }
    }
}
