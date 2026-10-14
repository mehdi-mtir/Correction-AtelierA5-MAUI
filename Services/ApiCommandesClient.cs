using System.Net;
using System.Net.Http.Json;
using GestionCommandes.Maui.Dtos;

namespace GestionCommandes.Maui.Services;

// Client « typé » : un HttpClient déjà configuré lui est fourni par injection
public class ApiCommandesClient
{
    private readonly HttpClient http;

    public ApiCommandesClient(HttpClient http)
    {
        this.http = http;
    }

    // Renvoie le jeton, ou null si l'identifiant ou le mot de passe est refusé
    public async Task<string?> SeConnecterAsync(string nomUtilisateur, string motDePasse)
    {
        var reponse = await http.PostAsJsonAsync("api/auth/login",
            new ConnexionDto(nomUtilisateur, motDePasse));

        if (reponse.StatusCode == HttpStatusCode.Unauthorized)
            return null;

        reponse.EnsureSuccessStatusCode();
        var resultat = await reponse.Content.ReadFromJsonAsync<ReponseConnexionDto>();
        return resultat?.Jeton;
    }

    public async Task<List<ClientDto>> ListerClientsAsync()
    {
        var reponse = await http.GetAsync("api/clients");
        reponse.EnsureSuccessStatusCode();          // exception si code ≠ 2xx
        return await reponse.Content.ReadFromJsonAsync<List<ClientDto>>() ?? new();
    }

    public async Task<List<CommandeDto>> ListerCommandesAsync()
    {
        var reponse = await http.GetAsync("api/commandes");
        reponse.EnsureSuccessStatusCode();
        return await reponse.Content.ReadFromJsonAsync<List<CommandeDto>>() ?? new();
    }

    public async Task<CommandeDto?> ObtenirCommandeAsync(int id)
    {
        var reponse = await http.GetAsync($"api/commandes/{id}");
        if (reponse.StatusCode == HttpStatusCode.NotFound)
            return null;                             // commande inexistante
        reponse.EnsureSuccessStatusCode();
        return await reponse.Content.ReadFromJsonAsync<CommandeDto>();
    }

    // Extension : produits de la base de l'API
    public async Task<List<ProduitDto>> ListerProduitsAsync()
    {
        var reponse = await http.GetAsync("api/produits");
        reponse.EnsureSuccessStatusCode();
        return await reponse.Content.ReadFromJsonAsync<List<ProduitDto>>() ?? new();
    }
}
