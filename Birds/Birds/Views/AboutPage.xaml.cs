using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Birds.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage() //Constructor
        {
            InitializeComponent();
        }

        async void OnButtonClicked(object sender, EventArgs e) //Executed when the user presses the "Learn more about Xamarin" button
        {
            //Open the Xamarin quickstart page in the user's default web browser.
            await Launcher.OpenAsync("https://aka.ms/xamarin-quickstart");
        }

        async void OnIdentifyButtonClicked(object sender, EventArgs e) //Executed when the user presses the "Learn more about identifying birds" button
        {
            //Open the RSPB bird identification page in the user's default web browser.
            await Launcher.OpenAsync("https://www.rspb.org.uk/birds-and-wildlife/wildlife-guides/identify-a-bird/");
        }
    }
}