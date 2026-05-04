using DiceRollGame.ViewModels;

namespace DiceRollGame.Views;

public partial class DicePage : ContentPage
{
    private readonly DiceViewModel _viewModel;

    public DicePage(DiceViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.StartShakeDetectionCommand.Execute(null);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.StopShakeDetectionCommand.Execute(null);
    }
}