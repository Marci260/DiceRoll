using DiceRollGame.ViewModels;

namespace DiceRollGame.Views;

public partial class PlayerDetailPage : ContentPage
{
    public PlayerDetailPage(PlayerDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}