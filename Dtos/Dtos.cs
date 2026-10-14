namespace GestionCommandes.Maui.Dtos;

// Ce que l'API renvoie (et reçoit) : un record par forme de JSON
public record ClientDto(int Id, string Nom, string Email);

public record ProduitDto(int Id, string Nom, decimal Prix, int Stock);

public record LigneCommandeDto(int ProduitId, string ProduitNom,
                               int Quantite, decimal PrixUnitaire);

public record CommandeDto(int Id, DateTime DateCommande, string Statut,
                          int ClientId, string ClientNom,
                          List<LigneCommandeDto> Lignes, decimal Total);

// Connexion : POST /api/auth/login
public record ConnexionDto(string NomUtilisateur, string MotDePasse);

public record ReponseConnexionDto(string Jeton);
