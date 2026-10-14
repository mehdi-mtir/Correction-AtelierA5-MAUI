namespace GestionCommandes.Maui.Models;

public class Client
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // Texte affiché par défaut, par exemple dans une liste déroulante
    public override string ToString() => Nom;
}
