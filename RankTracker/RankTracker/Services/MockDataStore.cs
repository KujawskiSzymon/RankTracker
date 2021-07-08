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
                { new Player() {Id = Guid.NewGuid().ToString(), Name = "KalibeRaziel", Rank = 1200, PlayerHistory=new List<PlayerHistory>(), Title = "TBA" }, new Player(){Id = Guid.NewGuid().ToString(), Name = "Test", Rank = 1200, PlayerHistory=new List<PlayerHistory>(), Title = "TBA" },new Player(){Id = Guid.NewGuid().ToString(), Name = "Leszek", Rank = 1000, PlayerHistory=new List<PlayerHistory>(), Title = "TBA" }},Matches = new List<Match>() },
                
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
        public async Task<bool> UpdatePlayerAsync(Player p)
        {
            var currentGame = games.Where((Game arg) => arg.Id == Static.AppInfoStatic.currentGame.Id).FirstOrDefault();
            foreach(var player in currentGame.Players)
            {
                if (p.Id == player.Id)
                {
                    player.PlayerHistory = p.PlayerHistory;
                    player.Rank = p.Rank;
                    
                }
            }
            Console.WriteLine("UPDATED PLAYER: "+p.Name);

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
        public async Task<Player> GetPlayerByNameAsync(Game game, string name)
        {
            return await Task.FromResult(game.Players.FirstOrDefault(s => s.Name == name));
        }

        public async Task<IEnumerable<Game>> GetGamesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(games);
        }
    }
}