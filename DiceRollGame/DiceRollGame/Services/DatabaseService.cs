using DiceRollGame.Models;
using SQLite;

namespace DiceRollGame.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        private async Task Init()
        {
            if (_database is not null)
                return;

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "diceandscore.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            await _database.CreateTableAsync<PlayerScore>();
        }

        public async Task<List<PlayerScore>> GetPlayersAsync()
        {
            await Init();
            return await _database.Table<PlayerScore>().ToListAsync();
        }

        public async Task<int> SavePlayerAsync(PlayerScore item)
        {
            await Init();
            if (string.IsNullOrEmpty(item.PlayerName))
            {
                throw new Exception("Player name cannot be empty.");
            }

            if (item.Id != 0)
                return await _database.UpdateAsync(item);
            else
                return await _database.InsertAsync(item);
        }

        public async Task<int> DeletePlayerAsync(PlayerScore item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }
    }
}
