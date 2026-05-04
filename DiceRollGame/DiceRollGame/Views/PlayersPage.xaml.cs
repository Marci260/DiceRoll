using DiceRollGame.ViewModels;

namespace DiceRollGame.Views;

public partial class PlayersPage : ContentPage
{
    private readonly PlayersViewModel _viewModel;

    public PlayersPage(PlayersViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }
   
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadPlayersCommand.Execute(null);
    }
}