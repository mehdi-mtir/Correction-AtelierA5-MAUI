using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionCommandes.Maui.Models;
using GestionCommandes.Maui.Services;

namespace GestionCommandes.Maui.ViewModels;

// Le paramètre « id » de la route est rangé dans la propriété CommandeId
[QueryProperty(nameof(CommandeId), "id")]
public partial class DetailCommandeViewModel : ObservableObject
{
    private readonly IDepotCommandes depotCommandes;

    public DetailCommandeViewModel(IDepotCommandes depotCommandes)
    {
        this.depotCommandes = depotCommandes;
    }

    // Identifiant de la commande à afficher (reçu par la route)
    [ObservableProperty]
    private int commandeId;

    [ObservableProperty]
    private Commande? commande;

    [ObservableProperty]
    private bool isBusy;

    [RelayCommand]
    private async Task ChargerAsync()
    {
        IsBusy = true;
        try
        {
            Commande = await depotCommandes.ObtenirAsync(CommandeId);
        }
        finally
        {
            IsBusy = false;
        }
    }

    // Route « .. » : dépiler la page courante (retour à la liste)
    [RelayCommand]
    private async Task RetourAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
