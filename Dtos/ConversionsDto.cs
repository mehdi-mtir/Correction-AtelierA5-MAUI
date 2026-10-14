using GestionCommandes.Maui.Models;

namespace GestionCommandes.Maui.Dtos;

// Méthodes d'extension : dto.VersModele() convertit un DTO en modèle
public static class ConversionsDto
{
    public static Client VersModele(this ClientDto dto) =>
        new() { Id = dto.Id, Nom = dto.Nom, Email = dto.Email };

    public static Produit VersModele(this ProduitDto dto) =>
        new() { Id = dto.Id, Nom = dto.Nom, Prix = dto.Prix };

    public static Commande VersModele(this CommandeDto dto) => new()
    {
        Id = dto.Id,
        DateCommande = dto.DateCommande,
        Statut = dto.Statut,
        ClientId = dto.ClientId,
        ClientNom = dto.ClientNom,
        Lignes = dto.Lignes.Select(l => new LigneCommande
        {
            ProduitNom = l.ProduitNom,
            Quantite = l.Quantite,
            PrixUnitaire = l.PrixUnitaire
        }).ToList()
    };
}
