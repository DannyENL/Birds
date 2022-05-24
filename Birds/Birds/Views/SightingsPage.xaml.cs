using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Birds.Models;
using Xamarin.Forms;

namespace Birds.Views
{
    public partial class SightingsPage : ContentPage
    {
        public SightingsPage() //Constructor
        {
            InitializeComponent();
        }

        protected override async void OnAppearing() //Executed when the page loads
        {
            base.OnAppearing();
            SearchBar.Text = ""; //Reset the search bar text
            collectionView.ItemsSource = await App.Database.GetSightingsAsync(); //Retrieve all the sightings from the database, and set them as the data source for the CollectionView.
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e) //Executed when a sighting is selected
        {
            if (e.CurrentSelection != null) //If it's not nothing
            {
                Sighting sighting = (Sighting)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(SightingEntryPage)}?{nameof(SightingEntryPage.ItemId)}={sighting.ID.ToString()}"); // Navigate to the SightingEntryPage, passing the ID so that the relevant sighting can be loaded
            }
        }

        async void OnAddClicked(object sender, EventArgs e) //Executed when the Record New Sighting button is pressed
        {
            await Shell.Current.GoToAsync(nameof(SightingEntryPage)); //Navigate to the SightingEntryPage, without passing an ID so that a new entry is made
        }

        async void OnTextChanged(object sender, EventArgs e) //Executed when the text in the search bar is updated
        {
            SearchBar searchBar = (SearchBar)sender; //Get the esarch bar that triggered this function
            List<Sighting> all_sightings = await App.Database.GetSightingsAsync();  //Get all the sightings
            List<Sighting> searched_list = new List<Sighting>(); //Create a new empty list to store sightings that match the search
            foreach (var recorded_sighting in all_sightings) //Loop through each sighting in the list of all sightings
            {
                if (recorded_sighting.Text.ToLower().Contains(searchBar.Text.ToLower())) //If the sighting text contains the search bar text in any capitalisation
                {
                    searched_list.Add(recorded_sighting); //Add the sighting to the list of searched sightings
                }
            }
            collectionView.ItemsSource = searched_list; //Change the displayed collectionView to the sightings that match the search
        }

    }
}