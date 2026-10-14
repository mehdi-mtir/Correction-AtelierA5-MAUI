using System.Collections.ObjectModel;
using System.Net;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionCommandes.Maui.Models;
using GestionCommandes.Maui.Services;

namespace GestionCommandes.Maui.ViewModels;

public partial class CommandesViewModel : ObservableObject
{
    private readonly IDepotCommandes depotCommandes;

    public CommandesViewModel(IDepotCommandes depotCommandes)
    {
        this.depotCommandes = depotCommandes;
    }

    [ObservableProperty]
    private ObservableCollection<Commande> commandes = new();

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string message = string.Empty;

    [RelayCommand]
    private async Task ChargerAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        try
        {
            Message = string.Empty;
            Commandes = new ObservableCollection<Commande>(
                await depotCommandes.ListerAsync());
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            SecureStorage.Default.Remove("jwt");
            await Shell.Current.GoToAsync("//connexion");
        }
        catch (HttpRequestException)
        {
            Message = "Serveur injoignable. Vérifiez votre connexion.";
        }
        catch (TaskCanceledException)
        {
            Message = "Le serveur ne répond pas (délai dépassé).";
        }
        finally
        {
            IsBusy = false;
        }
    }

    // Ouvrir la fiche d'une commande (route relative = empiler)
    [RelayCommand]
    private async Task OuvrirDetailAsync(Commande commande)
    {
        await Shell.Current.GoToAsync($"detailCommande?id={commande.Id}");
    }

    // Ouvrir la saisie d'une nouvelle commande
    [RelayCommand]
    private async Task NouvelleCommandeAsync()
    {
        await Shell.Current.GoToAsync("nouvelleCommande");
    }
}
