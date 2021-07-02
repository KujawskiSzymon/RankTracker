using RankTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RankTracker.Services
{
    public class MockDataStore : IDataStore<Game>
    {
        readonly List<Game> games;

        public MockDataStore()
        {
            games = new List<Game>()
            {
                new Game { Id = Guid.NewGuid().ToString(), Name = "Crash Team Racing", Players= new List<Player>()
                { new Player() {Id = Guid.NewGuid().ToString(), Name = "KalibeRaziel", Rank = 1200, PlayerHistory = new PlayerHistory(), Title = "TBA" } } },
                
            };
        }

        public async Task<bool> AddGameAsync(Game game)
        {
            games.Add(game);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateGameAsync(Game game)
        {
            var oldGame = games.Where((Game arg) => arg.Id == game.Id).FirstOrDefault();
            games.Remove(oldGame);
            games.Add(game);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteGameAsync(string id)
        {
            var oldGame = games.Where((Game arg) => arg.Id == id).FirstOrDefault();
            games.Remove(oldGame);

            return await Task.FromResult(true);
        }

        public async Task<Game> GetGameAsync(string id)
        {
            return await Task.FromResult(games.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Game>> GetGamesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(games);
        }
    }
}