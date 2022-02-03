using RankTracker.Models;
using RankTracker.Services;
using RankTracker.Static;
using RankTracker.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using Xamarin.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace RankTracker.ViewModels
{
    public class CreateMatchViewModel : BaseViewModel
    {
        private ObservableCollection<PlayerMatchCreate> players;


        public Command CreateMatchCommand { get; }

        public GameDataStore database;

        public ObservableCollection<PlayerMatchCreate> Players
        {
            get => players;
            set => SetProperty(ref players, value);
        }

        public CreateMatchViewModel()
        {
            players = new ObservableCollection<PlayerMatchCreate>();
            foreach (var p in Static.AppInfoStatic.currentPlayersInMatch)
            {
                PlayerMatchCreate player = new PlayerMatchCreate() { Name = p.name, points = "0", Id =p.id };
                players.Add(player);
            }
            CreateMatchCommand = new Command(OnCreateMatch, Validate);
            
        }

        private async void OnCreateMatch()
        {
            
            database = await GameDataStore.Instance;
            int K = setK();
            for (int i = 0; i < Players.Count; i++)
            {
                for (int j = i + 1; j < Players.Count; j++)
                {
                    if (Convert.ToInt32(Players[i].GetPoints()) > Convert.ToInt32(Players[j].points))
                    {
                        await EloRating(Players[i].Id, Players[j].Id, K, 1);
                    }
                    else if (Convert.ToInt32(Players[i].points) == Convert.ToInt32(Players[j].points))
                    {
                       await EloRating(Players[i].Id, Players[j].Id, K, 0.5);
                    }
                    else
                    {
                         await EloRating(Players[i].Id, Players[j].Id, K, -1);
                    }
                }
            }
            
            Match match = new Match() { Players = new List<PlayerMatchInfo>() };
            match.Date = DateTime.UtcNow;
            match.WinnnerName = setWinner(Players);
            match.GameId = AppInfoStatic.currentGame.Id;
            _ = await database.SaveMatchAsync(match);
            List<Match> matches = database.GetMatchesAsync(AppInfoStatic.currentGame.Id).Result;

            Match thisMatch = matches[(matches.Count-1)];
            foreach (var p in Players)
            {
                
                Player player = database.GetPlayerAsync(p.Id).Result;
                player.Rank += player.RankRated;
                bool isXDDD = player.RankRated > 0 ? true : false;
                string color;
                if (isXDDD)
                {
                    color = "Green";
                }
                else
                {
                    color = "Red";
                }
                PlayerHistory ph = new PlayerHistory() { Date = DateTime.UtcNow, RankHistory = player.RankRated, PlayerId = player.Id, fontColor=color };
                PlayerMatchInfo playerInfoForMatch = new PlayerMatchInfo() { PlayerName = player.Name, RankChange = player.RankRated.ToString(), Points = Int32.Parse(p.points),MatchId= thisMatch.Id};
                player.RankRated = 0;
                _ = await database.SavePlayerMatchInfoAsync(playerInfoForMatch);
                _ = await database.SavePlayerAsync(player);
                _ = await database.SavePlayerHistoryAsync(ph);
            }

            await Shell.Current.GoToAsync("../..");
        }
        private bool Validate()
        {
            int value = 0;
            foreach (var p in Players)

            {
                if (Int32.TryParse(p.points, out value))
                    continue;
                else
                    return false;

            }

            return true;

        }

        private async Task<bool> EloRating(int playerAid, int playerBid, int K, double win)
        {
            
            Player playera = await database.GetPlayerAsync(playerAid);
            Player playerb = await database.GetPlayerAsync(playerBid);
            double probplayera = Probability(playera.Rank, playerb.Rank);
            double probplayerb = Probability(playerb.Rank, playera.Rank);

          

            if (win == 1)
            {
                double rankrateda = K * (1 - probplayera);
                double rankratedb = K * (0 - probplayerb);
                if (rankrateda == 0)
                    rankrateda = 1;
                if (rankratedb == 0)
                    rankratedb = -1;
                playera.RankRated += Convert.ToInt32(rankrateda);
                playerb.RankRated += Convert.ToInt32(rankratedb);
            }
            else if (win == 0.5)
            {
                double rankrateda = K * (0.5 - probplayera);
                double rankratedb = K * (0.5 - probplayerb);
                playera.RankRated += Convert.ToInt32(rankrateda);
                playerb.RankRated += Convert.ToInt32(rankratedb);
            }
            else
            {
                double rankrateda = K * (0 - probplayera);
                double rankratedb = K * (1 - probplayerb);
                if (rankrateda == 0)
                    rankrateda = -1;
                if (rankratedb == 0)
                    rankratedb = 1;
                playera.RankRated += Convert.ToInt32(rankrateda);
                playerb.RankRated += Convert.ToInt32(rankratedb);
            }
            _ = await database.SavePlayerAsync(playera);
            _ = await database.SavePlayerAsync(playerb);
            return true;
        }

        private double Probability(double ranka, double rankb)
        {
            double prob = 1.0 * 1.0 / (1 + 1.0 * Math.Pow(10, 1.0 * (rankb - ranka) / 400));
            return prob;
        }
        private int setK()
        {
            int k;
            if (players.Count < 3)
            {
                k = 30;
            }
            else if (players.Count >= 2 && players.Count < 6)
            {
                k = 24;
            }
            else if (players.Count >= 6 && players.Count < 11)
            {
                k = 18;
            }
            else
            {
                k = 10;
            }
            return k;
        }

        private string setWinner(ObservableCollection<PlayerMatchCreate> players)
        {
            int points = int.MinValue;
            string winner = "";
            for (int i = 0; i < players.Count; i++)
            {
                if (Int32.Parse(players[i].points) > points)
                {
                    winner = players[i].Name;
                    points = Int32.Parse(players[i].points);
                }
            }
            return winner;
        }

    }
}
    
