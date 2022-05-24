using System;
using System.IO;
using Birds.Models;
using Xamarin.Forms;
using Xamarin.Essentials;
using Plugin.Media;

namespace Birds.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class SightingEntryPage : ContentPage
    {
        public string ItemId //Used for the ID of the sighting
        {
            set
            {
                LoadSighting(value); //Get sighting details when the ItemId is set
            }
        }

        public SightingEntryPage() //Constructor
        {
            InitializeComponent();
            BindingContext = new Sighting(); // Set the BindingContext of the page to a new Sighting.
        }

        async void LoadSighting(string itemId) //Used to load the sighting information
        {
            try
            {
                int id = Convert.ToInt32(itemId); //Get the ID for the sighting
                Sighting sighting = await App.Database.GetSightingAsync(id); //Get the sighting from the ID
                BindingContext = sighting; //Set the BindingContext for this page to the sighting
                LocationButton.IsVisible = true; //Enable the location button (only want the button on sightings that have been already created)
                BirdImage.Source = sighting.ImagePath; //Display loaded bird image
            }
            catch (Exception) //If loading failed
            {
                Console.WriteLine("Failed to load sighting."); //Write error to console
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e) //Executed when the Save button is pressed
        {
            var sighting = (Sighting)BindingContext; //Get the current sighting object
            sighting.Date = DateTime.UtcNow; //Set the sighting object's date field to be the current date
            if (sighting.Latitude==0) //If the latitude has not been set yet
            {
                var location = await Geolocation.GetLastKnownLocationAsync(); //Get the device's current GPS location
                sighting.Latitude = location.Latitude; //Set the sighting object's latitude to be the device's latitude
                sighting.Longitude = location.Longitude; //Set the sighting object's longitude to be the device's longitude
            }

            await App.Database.SaveSightingAsync(sighting); //Save the sighting to the database
            await DisplayAlert("Sighting Updated", "Your sighting has been saved succesfully.", "OK"); //Display an alert message informing the user that their sighting has been updated
        }

        async void OnPhotoButtonClicked(object sender, EventArgs e) //Executed when the Add Photo button is pressed
        {
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions //Open the file picker and assign the given filepath to the file variable
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            });

            var sighting = (Sighting)BindingContext; //Get the current sighting object
            sighting.ImagePath = file.Path; //Update the sighting object's ImagePath to the chosen image's filepath
            BirdImage.Source = sighting.ImagePath; //Update the displayed BirdImage element onscreen to match this newly chosen image
        }

        async void OnUpdateLocationButtonClicked(object sender, EventArgs e) //Executed when the Update Location button is pressed
        {
            var sighting = (Sighting)BindingContext; //Get the current sighting object
            var location = await Geolocation.GetLastKnownLocationAsync(); //Get the current GPS location of the device
            sighting.Latitude = location.Latitude; //Update the sighting's latitude
            sighting.Longitude = location.Longitude; //Update the sighting's longitude
            await DisplayAlert("Location Succesfully Reset", "The location for this sighting has been updated to match your current position. Press Save to confirm your changes.", "OK"); //Display an alert message informing the user that the location has now been reset
        }

        async void OnLocationButtonClicked(object sender, EventArgs e) //Executed when the View Location button is pressed
        {
            var sighting = (Sighting)BindingContext; //Get the current sighting object
            await Map.OpenAsync(new Location(sighting.Latitude, sighting.Longitude), new MapLaunchOptions { Name = sighting.Text }); //Open the current sighting's position in the default map app, with the location name being the name of the bird sighted
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e) //Executed when the Delete button is pressed
        {
            var sighting = (Sighting)BindingContext; //Get the current sighting object
            await App.Database.DeleteSightingAsync(sighting); //Delete the sighting from the database 

            await Shell.Current.GoToAsync(".."); //Return to the previous page
        }
    }
}