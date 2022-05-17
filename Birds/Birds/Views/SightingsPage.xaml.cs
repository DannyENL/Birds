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
        public SightingsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the sightings from the database, and set them as the
            // data source for the CollectionView.
            collectionView.ItemsSource = await App.Database.GetSightingsAsync();
            SearchBar.Text = "";
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the SightingEntryPage, passing the ID as a query parameter.
                Sighting sighting = (Sighting)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(SightingEntryPage)}?{nameof(SightingEntryPage.ItemId)}={sighting.ID.ToString()}");
            }
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the SightingEntryPage, without passing any data.
            await Shell.Current.GoToAsync(nameof(SightingEntryPage));
        }

        async void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            List<Sighting> all_sightings = await App.Database.GetSightingsAsync(); 
            List<Sighting> searched_list = new List<Sighting>();
            foreach (var recorded_sighting in all_sightings)
            {
                if (recorded_sighting.Text.ToLower().Contains(searchBar.Text.ToLower()))
                {
                    searched_list.Add(recorded_sighting);
                }
            }
            collectionView.ItemsSource = searched_list;
        }

    }
}