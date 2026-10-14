namespace GestionCommandes.Maui.Models;

public class Produit
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public decimal Prix { get; set; }

    public override string ToString() => $"{Nom} ({Prix:N2})";
}
