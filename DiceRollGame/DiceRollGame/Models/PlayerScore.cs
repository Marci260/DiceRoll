using SQLite;

namespace DiceRollGame.Models
{
    public class PlayerScore
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public int CurrentScore { get; set; }
        public string GameName { get; set; }
    }
}
