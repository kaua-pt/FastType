using FastType.ViewModels;
using UXDivers.Grial;

namespace FastType
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
                    fonts.AddFont("Poppins-Regular.ttf", "Poppins");
                    fonts.AddFont("materialdesignicons-webfont.ttf", "Material Design Icons");
                })

                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler<NavigationPage, UXDivers.Grial.GrialNavigationPageHandler>();
                    handlers.AddHandler<ScrollView, FastType.ScrollViewHandler>();
                    handlers.AddHandler<Label, FastType.LabelHandler>();
                    handlers.AddHandler<Entry, UXDivers.Grial.EntryHandler>();
                })
                .UseGrial();
            builder.Services.AddTransient<GameViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<GameViewModel>();
            builder.Services.AddTransient<MenuViewModel>();
            builder.Services.AddTransient<GamePage>();
            builder.Services.AddTransient<MenuPage>();

            return builder.Build();
        }
    }
}
