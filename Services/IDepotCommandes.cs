using GestionCommandes.Maui.Models;

namespace GestionCommandes.Maui.Services;

public interface IDepotCommandes
{
    Task<List<Commande>> ListerAsync();
    Task<Commande?> ObtenirAsync(int id);   // null si la commande n'existe pas
}
