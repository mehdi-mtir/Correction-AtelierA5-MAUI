using GestionCommandes.Maui.Models;

namespace GestionCommandes.Maui.Services;

// Implémentation factice, en mémoire : remplacée par l'API à l'atelier A5
public class DepotClientsFactice : IDepotClients
{
    private readonly List<Client> clients = new()
    {
        new() { Id = 1, Nom = "Alice Martin",    Email = "alice@exemple.com" },
        new() { Id = 2, Nom = "Karim Ben Salah", Email = "karim@exemple.com" },
        new() { Id = 3, Nom = "Sarra Trabelsi",  Email = "sarra@exemple.com" },
    };

    public async Task<List<Client>> ListerAsync()
    {
        await Task.Delay(800);        // simule le temps de réponse d'un serveur
        return clients.ToList();      // renvoie une copie de la liste
    }
}
