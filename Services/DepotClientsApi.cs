using GestionCommandes.Maui.Dtos;
using GestionCommandes.Maui.Models;

namespace GestionCommandes.Maui.Services;

// Même contrat que DepotClientsFactice, mais les données viennent de l'API
public class DepotClientsApi : IDepotClients
{
    private readonly ApiCommandesClient api;

    public DepotClientsApi(ApiCommandesClient api)
    {
        this.api = api;
    }

    public async Task<List<Client>> ListerAsync()
    {
        var dtos = await api.ListerClientsAsync();
        return dtos.Select(d => d.VersModele()).ToList();
    }
}
