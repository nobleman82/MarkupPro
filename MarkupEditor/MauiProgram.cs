using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Microsoft.Maui.LifecycleEvents;

// Diese Usings sind wichtig für die Windows-Fenstersteuerung
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
#endif

namespace MarkupEditor
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
                })
                // Hier wird 'events' definiert:
                .ConfigureLifecycleEvents(events =>
                {
#if WINDOWS
                    events.AddWindows(windows => windows.OnWindowCreated(window =>
                    {
                        var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
                        var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
                        
                        // Setzt die Startgröße auf 1200x800 Pixel
                        appWindow.Resize(new Windows.Graphics.SizeInt32(1200, 800));
                    }));
#endif
                });
            builder.Services.AddSingleton<AppState>();
            builder.Services.AddMauiBlazorWebView();
            builder.UseMauiCommunityToolkit();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}