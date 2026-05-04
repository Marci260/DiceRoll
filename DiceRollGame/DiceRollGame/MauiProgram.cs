using DiceRollGame.Services;
using DiceRollGame.ViewModels;
using DiceRollGame.Views;
using Microsoft.Extensions.Logging;

namespace DiceRollGame
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<DatabaseService>();

            builder.Services.AddTransient<DiceViewModel>();
            builder.Services.AddTransient<DicePage>();

            builder.Services.AddTransient<PlayersViewModel>();
            builder.Services.AddTransient<PlayersPage>();

            builder.Services.AddTransient<PlayerDetailViewModel>();
            builder.Services.AddTransient<PlayerDetailPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
