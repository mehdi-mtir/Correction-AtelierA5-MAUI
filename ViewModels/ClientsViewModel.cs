using System.Collections.ObjectModel;
using System.Net;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionCommandes.Maui.Models;
using GestionCommandes.Maui.Services;

namespace GestionCommandes.Maui.ViewModels;

public partial class ClientsViewModel : ObservableObject
{
    private readonly IDepotClients depotClients;

    // Le dépôt est injecté : le ViewModel ne sait pas d'où viennent les données
    public ClientsViewModel(IDepotClients depotClients)
    {
        this.depotClients = depotClients;
    }

    [ObservableProperty]
    private ObservableCollection<Client> clients = new();

    [ObservableProperty]
    private bool isBusy;

    // Message d'erreur affiché sous le titre
    [ObservableProperty]
    private string message = string.Empty;

    [RelayCommand]
    private async Task ChargerClientsAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        try
        {
            Message = string.Empty;
            var liste = await depotClients.ListerAsync();
            Clients = new ObservableCollection<Client>(liste);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            // Jeton absent, invalide ou expiré : retour à la connexion
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

    // TEST UNIQUEMENT : remplace le jeton par une valeur invalide (supprimé à l'A9)
    [RelayCommand]
    private async Task InvaliderJetonAsync()
    {
        await SecureStorage.Default.SetAsync("jwt", "jeton-invalide");
    }

    // Extension : déconnexion, le jeton ne doit pas survivre
    [RelayCommand]
    private async Task SeDeconnecterAsync()
    {
        SecureStorage.Default.Remove("jwt");
        await Shell.Current.GoToAsync("//connexion");
    }
}
