using System;
using System.IO;
using Birds.Data;
using Xamarin.Forms;

namespace Birds
{
    public partial class App : Application
    {
        static SightingDatabase database;

        // Create the database connection as a singleton.
        public static SightingDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new SightingDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Sightings.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}