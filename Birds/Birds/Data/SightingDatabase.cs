using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Birds.Models;
using Xamarin.Forms;

namespace Birds.Data
{
    public class SightingDatabase //Used to locally save sighting objects
    {
        readonly SQLiteAsyncConnection database; //Init database

        public SightingDatabase(string dbPath) //Constructor
        {
            database = new SQLiteAsyncConnection(dbPath); //Create new database
            database.CreateTableAsync<Sighting>().Wait(); //Create table of sightings
        }

        public Task<List<Sighting>> GetSightingsAsync() //Used to get all sightings
        {
            return database.Table<Sighting>().ToListAsync(); //Returns the sightings table in a list format
        }

        public Task<Sighting> GetSightingAsync(int id) //Used to get information about a specific sighting
        {
            return database.Table<Sighting>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync(); //Returns the sighting object with the given ID
        }

        public Task<int> SaveSightingAsync(Sighting sighting) //Save a given sighting to the database
        {
            if (sighting.ID != 0) //If the sighting ID has already been set, i.e. we're updating an already existing sighting
            {
                return database.UpdateAsync(sighting); //Update the sighting object to match the new one
            }
            else //Otherwise, if this is a brand new sighting
            {
                return database.InsertAsync(sighting); //Insert it into the table
            }
        }

        public Task<int> DeleteSightingAsync(Sighting sighting) //Delete a given sighting from the database
        {
            return database.DeleteAsync(sighting); //Deletes the given sighting from the database
        }
    }
}