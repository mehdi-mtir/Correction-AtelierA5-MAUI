using GestionCommandes.Maui.Models;

namespace GestionCommandes.Maui.Services;

// Données en mémoire : remplacées par l'API à l'atelier A5
public class DepotCommandesFactice : IDepotCommandes
{
    private readonly List<Commande> commandes = new()
    {
        Cmd(1, 1, "Alice Martin", -12, "Livree",
            Ligne("Clavier", 2, 45.90m)),
        Cmd(2, 2, "Karim Ben Salah", -5, "EnCours",
            Ligne("Écran 24 pouces", 1, 180m), Ligne("Souris", 3, 19.90m)),
        Cmd(3, 3, "Sarra Trabelsi", -2, "EnCours",
            Ligne("Souris", 10, 19.90m)),
        Cmd(4, 1, "Alice Martin", -1, "Annulee",
            Ligne("Écran 24 pouces", 2, 180m)),
    };

    // Petites méthodes utilitaires pour écrire les données de test
    private static LigneCommande Ligne(string produit, int quantite, decimal prix) =>
        new() { ProduitNom = produit, Quantite = quantite, PrixUnitaire = prix };

    private static Commande Cmd(int id, int clientId, string clientNom,
                                int ilYaJours, string statut,
                                params LigneCommande[] lignes) =>
        new()
        {
            Id = id, ClientId = clientId, ClientNom = clientNom,
            DateCommande = DateTime.Today.AddDays(ilYaJours),
            Statut = statut, Lignes = lignes.ToList()
        };

    public async Task<List<Commande>> ListerAsync()
    {
        await Task.Delay(500);   // simule le réseau
        return commandes.OrderByDescending(c => c.DateCommande).ToList();
    }

    public async Task<Commande?> ObtenirAsync(int id)
    {
        await Task.Delay(300);
        return commandes.FirstOrDefault(c => c.Id == id);
    }
}
