using GestionCommandes.Maui.Services;
using GestionCommandes.Maui.ViewModels;
using GestionCommandes.Maui.Views;
using Microsoft.Extensions.Logging;

namespace GestionCommandes.Maui;

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

        // ---------- Accès à l'API GestionCommandesApi ----------
        // Depuis l'émulateur Android, le PC s'appelle 10.0.2.2 (et non localhost)
        string adresseApi = DeviceInfo.Platform == DevicePlatform.Android
            ? "http://10.0.2.2:5050/"
            : "http://localhost:5050/";

        builder.Services.AddTransient<JetonHandler>();

        builder.Services.AddHttpClient<ApiCommandesClient>(client =>
        {
            client.BaseAddress = new Uri(adresseApi);
            client.Timeout = TimeSpan.FromSeconds(10);   // délai maximal par appel
        })
        .AddHttpMessageHandler<JetonHandler>();          // le jeton part avec chaque appel

        // ---------- Injection de dépendances ----------
        // Services : les données viennent maintenant de l'API
        // (DepotClientsFactice et DepotCommandesFactice restent dans le projet pour les tests de l'A9)
        builder.Services.AddTransient<IDepotClients, DepotClientsApi>();
        builder.Services.AddTransient<IDepotCommandes, DepotCommandesApi>();

        // ViewModels et pages (une instance à chaque affichage)
        builder.Services.AddTransient<ConnexionViewModel>();
        builder.Services.AddTransient<ConnexionPage>();
        builder.Services.AddTransient<ClientsViewModel>();
        builder.Services.AddTransient<ClientsPage>();
        builder.Services.AddTransient<CommandesViewModel>();
        builder.Services.AddTransient<CommandesPage>();
        builder.Services.AddTransient<DetailCommandeViewModel>();
        builder.Services.AddTransient<DetailCommandePage>();
        builder.Services.AddTransient<CommandeViewModel>();
        builder.Services.AddTransient<CommandePage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
