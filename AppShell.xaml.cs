using GestionCommandes.Maui.Views;

namespace GestionCommandes.Maui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Pages accessibles par navigation, hors de la structure visible
        Routing.RegisterRoute("detailCommande", typeof(DetailCommandePage));
        Routing.RegisterRoute("nouvelleCommande", typeof(CommandePage));
    }
}
