using RankTracker.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RankTracker.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddGameAsync(T item);
        Task<bool> AddPlayerAsync(T item, Player p);
        Task<bool> UpdateGameAsync(T item);
        Task<bool> DeleteGameAsync(string id);
        Task<T> GetGameAsync(string id);
        Task<Player> GetPlayerAsync(Game game,string id);
        Task<IEnumerable<T>> GetGamesAsync(bool forceRefresh = false);
    }
}
