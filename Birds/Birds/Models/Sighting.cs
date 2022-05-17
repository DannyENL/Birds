using System;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Birds.Models
{
    public class Sighting
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string AdditionalNotes { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ImagePath { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}