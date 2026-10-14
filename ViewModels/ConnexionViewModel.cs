using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionCommandes.Maui.Services;

namespace GestionCommandes.Maui.ViewModels;

public partial class ConnexionViewModel : ObservableObject
{
    private readonly ApiCommandesClient api;

    public ConnexionViewModel(ApiCommandesClient api)
    {
        this.api = api;
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SeConnecterCommand))]
    private string identifiant = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SeConnecterCommand))]
    private string motDePasse = string.Empty;

    [ObservableProperty]
    private string erreur = string.Empty;

    [ObservableProperty]
    private bool isBusy;

    private bool PeutSeConnecter() =>
        !string.IsNullOrWhiteSpace(Identifiant) && !string.IsNullOrWhiteSpace(MotDePasse);

    [RelayCommand(CanExecute = nameof(PeutSeConnecter))]
    private async Task SeConnecterAsync()
    {
        Erreur = string.Empty;
        IsBusy = true;
        try
        {
            var jeton = await api.SeConnecterAsync(Identifiant, MotDePasse);
            if (jeton is null)
            {
                Erreur = "Identifiant ou mot de passe incorrect.";
                return;
            }

            // Le jeton est rangé dans le coffre chiffré du système
            await SecureStorage.Default.SetAsync("jwt", jeton);

            MotDePasse = string.Empty;                 // ne pas garder le mot de passe
            await Shell.Current.GoToAsync("//clients");
        }
        catch (HttpRequestException ex)
        {
            // Provisoire : code HTTP (vide si le serveur n'a pas répondu) et message technique
            Erreur = $"{(int?)ex.StatusCode} {ex.Message} {ex.InnerException?.Message}";
        }
        /*catch (HttpRequestException)
        {
            Erreur = "Serveur injoignable. Vérifiez que l'API est démarrée.";
        }
        
        catch (TaskCanceledException)
        {
            Erreur = "Le serveur ne répond pas (délai dépassé).";
        }*/
        finally
        {
            IsBusy = false;
        }
    }
}
