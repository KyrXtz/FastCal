﻿using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views.Tutorials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealTutorialView3 : PopupPage
    {
        public MealTutorialView3()
        {
            InitializeComponent();
           // TutLabel.Text = "Here you can see info about the button.\nTap the button to remove one entry from your daily macros....";

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            FadeIn();
        }
        private async void FadeIn()
        {
            await fabbtntut.FadeTo(0.5, 350, Easing.SpringIn);
            FadeOut();
        }
        private async void FadeOut()
        {
            await fabbtntut.FadeTo(0, 550, Easing.SpringIn);
            await Task.Delay(400);
            FadeIn();
        }
      
        private async void fbbtnclicked(object sender, EventArgs e)
        {
            Application.Current.Properties["mealviewedtutorial3"] = "ok";
            await Application.Current.SavePropertiesAsync();
            await this.Navigation.RemovePopupPageAsync(this);

        }
    }
}