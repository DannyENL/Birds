﻿using System;
using System.IO;
using Birds.Models;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Birds.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class SightingEntryPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadSighting(value);
            }
        }

        public SightingEntryPage()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Sighting.
            BindingContext = new Sighting();
        }

        async void LoadSighting(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the sighting and set it as the BindingContext of the page.
                Sighting sighting = await App.Database.GetSightingAsync(id);
                BindingContext = sighting;
                LocationButton.IsVisible = true;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load sighting.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var sighting = (Sighting)BindingContext;
            sighting.Date = DateTime.UtcNow;
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (sighting.Latitude==0)
            {
                sighting.Latitude = location.Latitude;
                sighting.Longitude = location.Longitude;
            }

            if (!string.IsNullOrWhiteSpace(sighting.Text))
            {
                await App.Database.SaveSightingAsync(sighting);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
        async void OnExistingPhotoButtonClicked (object sender, EventArgs e)
        {
            return;
        }

        async void OnNewPhotoButtonClicked(object sender, EventArgs e)
        {
            var photo = await MediaPicker.PickPhotoAsync();
        }

        async void OnLocationButtonClicked(object sender, EventArgs e)
        {
            var sighting = (Sighting)BindingContext;
            await Map.OpenAsync(new Location(sighting.Latitude, sighting.Longitude), new MapLaunchOptions { Name = sighting.Text });
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var sighting = (Sighting)BindingContext;
            await App.Database.DeleteSightingAsync(sighting);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}