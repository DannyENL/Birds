using System;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Birds.Models
{
    public class Sighting //Used to store Sighting information
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } //Each sighting has a unique ID
        public string AdditionalNotes { get; set; } //Contents of the "Additional Notes" field
        public double Latitude { get; set; } //Coordinate for latitude position
        public double Longitude { get; set; } //Coordinate for longitude position
        public string ImagePath { get; set; } //Path to the chosen image file
        public string Text { get; set; } //Contents of the "Sighting" field
        public DateTime Date { get; set; } //Date of creation
    }
}