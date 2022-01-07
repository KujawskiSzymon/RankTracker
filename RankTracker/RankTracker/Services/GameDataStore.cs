using RankTracker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace RankTracker.Services
{
  public  class GameDataStore
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<GameDataStore> Instance = new AsyncLazy<GameDataStore>(async () =>
        {
            var instance = new GameDataStore();
            CreateTableResult result = await Database.CreateTableAsync<Game>();
            CreateTableResult resultPlayer = await Database.CreateTableAsync<Player>();
            CreateTableResult resultPlayer2 = await Database.CreateTableAsync<PlayerHistory>();
            CreateTableResult resultPlayer3 = await Database.CreateTableAsync<Match>();
            CreateTableResult resultPlayer4 = await Database.CreateTableAsync<PlayerMatchInfo>();
            return instance;
        });

        public GameDataStore()
        {
            Database = new SQLiteAsyncConnection(Static.AppInfoStatic.DatabasePath, Static.AppInfoStatic.Flags);
        }
        public Task<List<Game>> GetGamesAsync()
        {
            return Database.Table<Game>().ToListAsync();
        }

        public Task<List<Game>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<Game>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<Game> GetGameAsync(int id)
        {
            return Database.Table<Game>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<Match> GetMatchAsync(int idmatch)
        {
            return  Database.Table<Match>().Where(i => i.Id == idmatch).FirstOrDefaultAsync();
           
        }

        public  Task<Player> GetPlayerAsync(int idplayer)
        {
            try
            {
                return Database.Table<Player>().Where(i => i.Id == idplayer).FirstOrDefaultAsync();
               
            }
            catch(Exception ex)
            {
                throw new Exception("No Game was found");
            }
            
            
        }


        public Task<List<PlayerHistory>> GetPlayerHistoryAsync(int idplayer)
        {
            try
            {
                return Database.QueryAsync<PlayerHistory>("SELECT * FROM [PlayerHistory] WHERE [PlayerId] = ?", idplayer);

            }
            catch (Exception ex)
            {
                throw new Exception("No Game was found");
            }


        }
        public Task<List<PlayerMatchInfo>> GetPlayerMatchInfosAsync(int MatchId)
        {
            try
            {
                return Database.QueryAsync<PlayerMatchInfo>("SELECT * FROM [PlayerMatchInfo] WHERE [MatchId] = ?", MatchId);

            }
            catch (Exception ex)
            {
                throw new Exception("No Game was found");
            }


        }

        public Task<List<Match>> GetMatchesAsync(int GameId)
        {
            try
            {
                return Database.QueryAsync<Match>("SELECT * FROM [Match] WHERE [GameId] = ?", GameId);

            }
            catch (Exception ex)
            {
                throw new Exception("No Game was found");
            }


        }

        public  Task<List<Player>> GetPlayersAsync(int idgame)
        {
            try
            {
                return Database.QueryAsync<Player>("SELECT * FROM [Player] WHERE [GameId] = ?",idgame);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
            
        }

            public Task<int> SaveGameAsync(Game item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }
        public Task<int> SavePlayerAsync(Player item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }
        public Task<int> SavePlayerMatchInfoAsync(PlayerMatchInfo item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }
        public Task<int> SavePlayerHistoryAsync(PlayerHistory item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }
        public Task<int> SaveMatchAsync(Match item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Game item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
