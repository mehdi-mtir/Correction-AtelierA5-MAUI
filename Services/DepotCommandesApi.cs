using GestionCommandes.Maui.Dtos;
using GestionCommandes.Maui.Models;

namespace GestionCommandes.Maui.Services;

public class DepotCommandesApi : IDepotCommandes
{
    private readonly ApiCommandesClient api;

    public DepotCommandesApi(ApiCommandesClient api)
    {
        this.api = api;
    }

    public async Task<List<Commande>> ListerAsync()
    {
        var dtos = await api.ListerCommandesAsync();
        return dtos.Select(d => d.VersModele())
                   .OrderByDescending(c => c.DateCommande)
                   .ToList();
    }

    public async Task<Commande?> ObtenirAsync(int id)
    {
        var dto = await api.ObtenirCommandeAsync(id);
        return dto?.VersModele();
    }
}
