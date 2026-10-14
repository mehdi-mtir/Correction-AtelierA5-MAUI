namespace GestionCommandes.Maui.Models;

public class Commande
{
    public int Id { get; set; }
    public DateTime DateCommande { get; set; }
    public string Statut { get; set; } = "EnCours";   // EnCours, Livree ou Annulee
    public int ClientId { get; set; }
    public string ClientNom { get; set; } = string.Empty;
    public List<LigneCommande> Lignes { get; set; } = new();

    // Propriété calculée : somme des lignes
    public decimal Total => Lignes.Sum(l => l.Montant);
}
