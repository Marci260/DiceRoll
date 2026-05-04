using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DiceRollGame.Models;
using DiceRollGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceRollGame.ViewModels
{
    [QueryProperty(nameof(Player), "PlayerToEdit")]
    public partial class PlayerDetailViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private PlayerScore _player;

        public PlayerDetailViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            Player = new PlayerScore();
        }

        [RelayCommand]
        public async Task SaveAsync()
        {
            try
            {
                await _databaseService.SavePlayerAsync(Player);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Database Error", ex.Message, "OK");
            }
        }
    }
}
