using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Birds.Models;

namespace Birds.Data
{
    public class SightingDatabase
    {
        readonly SQLiteAsyncConnection database;

        public SightingDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Sighting>().Wait();
        }

        public Task<List<Sighting>> GetSightingsAsync()
        {
            //Get all notes.
            return database.Table<Sighting>().ToListAsync();
        }

        public Task<Sighting> GetSightingAsync(int id)
        {
            // Get a specific note.
            return database.Table<Sighting>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveSightingAsync(Sighting sighting)
        {
            if (sighting.ID != 0)
            {
                // Update an existing sighting.
                return database.UpdateAsync(sighting);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(sighting);
            }
        }

        public Task<int> DeleteSightingAsync(Sighting sighting)
        {
            // Delete a note.
            return database.DeleteAsync(sighting);
        }
    }
}