using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DiceRollGame.Models;
using DiceRollGame.Services;
using DiceRollGame.Views;
using System.Collections.ObjectModel;

namespace DiceRollGame.ViewModels
{
    public partial class PlayersViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public ObservableCollection<PlayerScore> Players { get; } = new();

        public PlayersViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [RelayCommand]
        public async Task LoadPlayersAsync()
        {
            try
            {
                var players = await _databaseService.GetPlayersAsync();
                Players.Clear();
                foreach (var player in players)
                {
                    Players.Add(player);
                }
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Data Error", "Failed to load players.", "OK");
            }
        }

        [RelayCommand]
        public async Task ShareResultsAsync()
        {
            if (!Players.Any())
            {
                await Application.Current.MainPage.DisplayAlert("Share Error", "No scoreboard data available to share.", "OK");
                return;
            }

            var winner = Players.OrderByDescending(p => p.CurrentScore).FirstOrDefault();

            if (winner == null)
            {
                await Application.Current.MainPage.DisplayAlert("Logic Error", "Could not determine a winner.", "OK");
                return;
            }

            string shareText = $"Winner is {winner.PlayerName} with {winner.CurrentScore} points!\n\nScoreboard:\n";

            foreach (var p in Players)
            {
                shareText += $"{p.PlayerName}: {p.CurrentScore} ({p.GameName})\n";
            }

            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = shareText,
                Title = "Game Results"
            });
        }

        [RelayCommand]
        public async Task AddPlayerAsync()
        {
            await Shell.Current.GoToAsync(nameof(PlayerDetailPage));
        }
    }
}
