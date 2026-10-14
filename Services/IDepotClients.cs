using GestionCommandes.Maui.Models;

namespace GestionCommandes.Maui.Services;

// Le contrat : ce que l'on sait faire avec les clients
public interface IDepotClients
{
    Task<List<Client>> ListerAsync();
}
