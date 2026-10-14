using System.Net.Http.Headers;

namespace GestionCommandes.Maui.Services;

// Intermédiaire placé devant chaque requête de ApiCommandesClient
public class JetonHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage requete, CancellationToken annulation)
    {
        var jeton = await SecureStorage.Default.GetAsync("jwt");

        if (!string.IsNullOrEmpty(jeton))
            requete.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", jeton);   // « Bearer eyJ… »

        // On laisse la requête continuer son chemin vers l'API
        return await base.SendAsync(requete, annulation);
    }
}
