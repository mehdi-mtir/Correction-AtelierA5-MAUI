using GestionCommandes.Maui.ViewModels;

namespace GestionCommandes.Maui.Views;

public partial class ClientsPage : ContentPage
{
    private readonly ClientsViewModel vm;

    // Le ViewModel est FOURNI à la page (injection), jamais créé avec new
    public ClientsPage(ClientsViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;   // toutes les {Binding …} de la page lisent ce ViewModel
    }

    // Cycle de vie : à l'affichage de la page, premier chargement automatique
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (vm.Clients.Count == 0)
            await vm.ChargerClientsCommand.ExecuteAsync(null);
    }
}
