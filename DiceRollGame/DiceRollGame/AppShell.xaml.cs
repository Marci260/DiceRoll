using DiceRollGame.Views;

namespace DiceRollGame
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(PlayerDetailPage), typeof(PlayerDetailPage));
        }
    }
}
