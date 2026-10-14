using GestionCommandes.Maui.ViewModels;

namespace GestionCommandes.Maui.Views;

public partial class ConnexionPage : ContentPage
{
    public ConnexionPage(ConnexionViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
