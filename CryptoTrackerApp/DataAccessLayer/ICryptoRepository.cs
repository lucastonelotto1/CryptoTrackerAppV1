using CryptoTrackerApp.Classes;

public interface ICryptoRepository
{
    Task<List<FavoriteCryptos>> GetFavoriteCryptos(Guid userId);
    Task AddFavoriteCrypto(FavoriteCryptos favoriteCrypto);
    Task RemoveFavoriteCrypto(Guid userId, string cryptoId);
    Task<float> GetLimit(Guid userId, string cryptoId);
    Task UpdateLimit(Guid userId, string cryptoId, float newLimit);
}