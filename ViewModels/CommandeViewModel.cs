using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionCommandes.Maui.Dtos;
using GestionCommandes.Maui.Models;
using GestionCommandes.Maui.Services;

namespace GestionCommandes.Maui.ViewModels;

// ObservableValidator = ObservableObject + validation par attributs
public partial class CommandeViewModel : ObservableValidator
{
    private readonly IDepotClients depotClients;
    private readonly ApiCommandesClient api;

    public CommandeViewModel(IDepotClients depotClients, ApiCommandesClient api)
    {
        this.depotClients = depotClients;
        this.api = api;

        // À chaque changement d'erreurs : mettre à jour les messages et le bouton
        ErrorsChanged += (_, _) =>
        {
            Erreurs = string.Join(Environment.NewLine,
                                  GetErrors().Select(e => e.ErrorMessage));
            EnregistrerCommand.NotifyCanExecuteChanged();
        };
    }

    // ----- Listes proposées dans les Picker -----
    public ObservableCollection<Client> Clients { get; } = new();

    // Extension : les produits viennent de l'API (remplis dans ChargerAsync)
    public ObservableCollection<Produit> Produits { get; } = new();

    // ----- Champs saisis, avec leurs règles -----
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyCanExecuteChangedFor(nameof(EnregistrerCommand))]
    [Required(ErrorMessage = "Choisissez un client.")]
    private Client? clientChoisi;

    [ObservableProperty]
    private DateTime dateCommande = DateTime.Today;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(Total))]
    [NotifyCanExecuteChangedFor(nameof(EnregistrerCommand))]
    [Required(ErrorMessage = "Choisissez un produit.")]
    private Produit? produitChoisi;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(Total))]
    [NotifyCanExecuteChangedFor(nameof(EnregistrerCommand))]
    [Range(1, 100, ErrorMessage = "La quantité doit être comprise entre 1 et 100.")]
    private int quantite = 1;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(EnregistrerCommand))]
    private bool estConfirmee;

    // ----- Messages affichés sous le formulaire -----
    [ObservableProperty]
    private string erreurs = string.Empty;

    [ObservableProperty]
    private string message = string.Empty;

    // Propriété calculée : jamais stockée, toujours recalculée
    public decimal Total => (ProduitChoisi?.Prix ?? 0m) * Quantite;

    [RelayCommand]
    private async Task ChargerAsync()
    {
        if (Clients.Count == 0)
            foreach (var client in await depotClients.ListerAsync())
                Clients.Add(client);

        if (Produits.Count == 0)
            foreach (var dto in await api.ListerProduitsAsync())
                Produits.Add(dto.VersModele());
    }

    // Le bouton Enregistrer n'est actif que si cette méthode renvoie true
    private bool PeutEnregistrer() =>
        ClientChoisi is not null && ProduitChoisi is not null
        && EstConfirmee && !HasErrors;

    [RelayCommand(CanExecute = nameof(PeutEnregistrer))]
    private void Enregistrer()
    {
        ValidateAllProperties();           // dernière vérification de toutes les règles
        if (HasErrors) return;

        Message = $"Commande enregistrée pour {ClientChoisi!.Nom} : {Total:N2}";
        // Simulation : l'envoi réel à l'API arrivera à l'atelier A5
    }
}
