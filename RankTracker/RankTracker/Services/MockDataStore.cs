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
            List<int> ranksMock = new List<int> { 10, 20, -39, 10 };
            List<DateTime> dateTimeMock = new List<DateTime>() { DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now };
            PlayerHistory p = new PlayerHistory { Date = dateTimeMock[0], RankHistory = ranksMock[0] };
            PlayerHistory p1 = new PlayerHistory { Date = dateTimeMock[1], RankHistory = ranksMock[1] };
            PlayerHistory p2 = new PlayerHistory { Date = dateTimeMock[2], RankHistory = ranksMock[2] };
            PlayerHistory p3= new PlayerHistory { Date = dateTimeMock[3], RankHistory = ranksMock[3] };
            List<PlayerHistory> playerHistories = new List<PlayerHistory> { p, p1, p2, p3 };
            games = new List<Game>()
            {
                new Game { Id = Guid.NewGuid().ToString(), Name = "Crash Team Racing", Players= new List<Player>()
                { new Player() {Id = Guid.NewGuid().ToString(), Name = "KalibeRaziel", Rank = 1200, PlayerHistory=playerHistories, Title = "TBA" } } },
                
            };
        }

        public async Task<bool> AddGameAsync(Game game)
        {
            games.Add(game);

            return await Task.FromResult(true);
        }
        public async Task<bool> AddPlayerAsync(Game game, Player p)
        {
            game.Players.Add(p);
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
        public async Task<Player> GetPlayerAsync(Game game, string id)
        {
            return await Task.FromResult(game.Players.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Game>> GetGamesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(games);
        }
    }
}