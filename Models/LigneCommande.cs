namespace GestionCommandes.Maui.Models;

public class LigneCommande
{
    public string ProduitNom { get; set; } = string.Empty;
    public int Quantite { get; set; }
    public decimal PrixUnitaire { get; set; }

    // Propriété calculée : montant de la ligne
    public decimal Montant => Quantite * PrixUnitaire;
}
