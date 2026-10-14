namespace GestionCommandes.Maui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Nos couleurs sont prévues pour un fond clair : on impose le thème clair.
        UserAppTheme = AppTheme.Light;

        MainPage = new AppShell();
    }
}
