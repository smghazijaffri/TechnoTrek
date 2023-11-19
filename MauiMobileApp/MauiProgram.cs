using Microsoft.Extensions.Logging;
using SharedClass.Components.Data;

namespace MauiMobileApp
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
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddBlazorBootstrap(); // Add this line
            builder.Services.AddScoped<Login>();
            builder.Services.AddScoped<Connection>();
            builder.Services.AddScoped<Insert>();
            builder.Services.AddScoped<Select>();
            builder.Services.AddScoped<Delete>();
            builder.Services.AddScoped<Update>();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
