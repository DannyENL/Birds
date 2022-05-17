using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Birds.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            // Launch the specified URL in the system browser.
            await Launcher.OpenAsync("https://aka.ms/xamarin-quickstart");
        }

        async void OnIdentifyButtonClicked(object sender, EventArgs e)
        {
            // Launch the specified URL in the system browser.
            await Launcher.OpenAsync("https://www.rspb.org.uk/birds-and-wildlife/wildlife-guides/identify-a-bird/");
        }
    }
}