using CommunityToolkit.Maui;
using MauiIcons.FontAwesome.Solid;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using Dogan_Rush.ViewModels;

namespace Dogan_Rush
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                // Initialize the .NET MAUI Community Toolkit by adding the below line of code
                .UseMauiCommunityToolkit()
                // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton(AudioManager.Current); // Register audio manager
            

            return builder.Build();
        }
    }
}