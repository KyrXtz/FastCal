using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

namespace App3.ViewModels
{
    public class MealViewModel : BaseViewModel
    {
        private Color bgcolor { get; set; }
        public Color BgColor
        {
            get
            {
                return bgcolor;
            }
            set
            {
                bgcolor = value;
                OnPropertyChanged();
            }
        }
        private bool macroBool { get; set; }
        public bool MacroBool
        {
            get
            {
                return macroBool;
            }
            set
            {
                macroBool = value;
                OnPropertyChanged();
            }
        }
        private string name { get; set; }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        //string name = "";
        //public string Name
        //{
        //    get { return name; }
        //    set { SetProperty(ref name, value); }
        //}
        //Color bgcolor = Color.Empty;
        //public Color BgColor
        //{
        //    get { return bgcolor; }
        //    set { SetProperty(ref bgcolor, value); }
        //}

        public MealViewModel()
        {
            MacroBool = false;
            IsBusy = false;

        }

        public void loadshot()
        {
            MacroBool = !MacroBool;
        }


    }
}
