using CryptoTrackerApp.Classes;

public interface ICryptoRepository
{
    Task<List<FavoriteCryptos>> GetFavoriteCryptos(string userId);
    Task AddFavoriteCrypto(string userId,string favoriteCrypto);
    Task RemoveFavoriteCrypto(string userId, string cryptoId);
    Task<float> GetLimit(string userId, string cryptoId);
    Task UpdateLimit(string userId, string cryptoId, float newLimit);

}