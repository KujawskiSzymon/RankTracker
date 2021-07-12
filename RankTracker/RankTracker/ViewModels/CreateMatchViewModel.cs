using RankTracker.Models;
using RankTracker.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace RankTracker.ViewModels
{
    public class CreateMatchViewModel : BaseViewModel
    {
        private ObservableCollection<PlayerMatchCreate> players;
        public Command CreateMatchCommand { get; }

        public CreateMatchViewModel()
        {
            players = new ObservableCollection<PlayerMatchCreate>();
            foreach(var p in Static.AppInfoStatic.currentPlayersInMatch)
            {
                PlayerMatchCreate player = new PlayerMatchCreate() { Name = p.name, points = "0" };
                players.Add(player);
            }
            CreateMatchCommand = new Command(OnCreateMatch,Validate);
        }

        public ObservableCollection<PlayerMatchCreate> Players
        {
            get => players;
            set => SetProperty(ref players, value);
        }

        private async void OnCreateMatch()
        {
            int K = setK();
           // Static.AppInfoStatic.PlayersInMatch = new List<Player>();
            for (int i = 0; i < Players.Count;i++)
            {
                for (int j = i + 1; j < Players.Count; j++)
                {
                    if (Convert.ToInt32(Players[i].points) > Convert.ToInt32(Players[j].points))
                    {
                        EloRating(Players[i].Name, Players[j].Name,K,1);
                    }
                    else if (Convert.ToInt32(Players[i].points) == Convert.ToInt32(Players[j].points))
                    {
                        EloRating(Players[i].Name, Players[j].Name, K, 0.5);
                    }
                    else
                    {
                        EloRating(Players[i].Name, Players[j].Name, K, -1);
                    }
                }
            }
            Match match = new Match() { Id = Guid.NewGuid().ToString(), Players = new List<PlayerMatchInfo>() };
            foreach (var p in Players)
            {
               Player player = await GamesStore.GetPlayerByNameAsync(Static.AppInfoStatic.currentGame, p.Name);
                player.Rank += player.RankRated;
                PlayerHistory ph = new PlayerHistory() { Date = DateTime.UtcNow, RankHistory = player.RankRated };
                player.PlayerHistory.Add(ph);
                PlayerMatchInfo playerInfoForMatch = new PlayerMatchInfo() { PlayerName = player.Name, RankChange = player.RankRated.ToString() ,Points = Int32.Parse(p.points)};
                player.RankRated = 0;

                match.Players.Add(playerInfoForMatch);


                //  await GamesStore.UpdatePlayerAsync(player);
            }
            
            match.Date = DateTime.UtcNow;
            match.WinnnerName = setWinner(Players);
            Game game = await GamesStore.GetGameAsync(Static.AppInfoStatic.currentGame.Id);
            game.Matches.Add(match);
            await GamesStore.UpdateGameAsync(game);
            await Shell.Current.GoToAsync("../..");
        }
        private bool Validate()
        {
            int value = 0;
            foreach(var p in Players)
                
            {
                if (Int32.TryParse(p.points, out value))
                    continue;
                else
                    return false;

            }
           
                return true;

        }

        private async void EloRating(string playerNameA,string playerNameB,int K, double win)
        {
            Player PlayerA = await GamesStore.GetPlayerByNameAsync(Static.AppInfoStatic.currentGame,playerNameA);
            Player PlayerB = await GamesStore.GetPlayerByNameAsync(Static.AppInfoStatic.currentGame,playerNameB);
            double ProbPlayerA = Probability(PlayerA.Rank,PlayerB.Rank);
            double ProbPlayerB = Probability(PlayerB.Rank, PlayerA.Rank);

            if (win == 1)
            {
                double rankRatedA =  K * (1 - ProbPlayerA);
                double rankRatedB =   K * (0 - ProbPlayerB);
                PlayerA.RankRated += Convert.ToInt32(rankRatedA);
                PlayerB.RankRated += Convert.ToInt32(rankRatedB);
            }
            else if (win == 0.5) {
                double rankRatedA =  K * (0.5 - ProbPlayerA);
                double rankRatedB = K * (0.5 - ProbPlayerB);
                PlayerA.RankRated += Convert.ToInt32(rankRatedA);
                PlayerB.RankRated += Convert.ToInt32(rankRatedB);
            }
            else
            {
                double rankRatedA =  K * (0 - ProbPlayerA);
                double rankRatedB =  K * (1 - ProbPlayerB);
                PlayerA.RankRated += Convert.ToInt32(rankRatedA);
                PlayerB.RankRated += Convert.ToInt32(rankRatedB);
            }
        }

        private double Probability(double rankA, double rankB)
        {
            double prob = 1.0 * 1.0 / ( 1+ 1.0 * Math.Pow(10, 1.0 * (rankB - rankA) / 400));
            return prob;
        }
        private int setK()
        {
            int K;
            if (Players.Count < 3)
            {
                K = 30;
            }
            else if (Players.Count >= 2 && Players.Count < 6)
            {
                K = 24;
            }
            else if (Players.Count >= 6 && Players.Count < 11)
            {
                K = 18;
            }
            else
            {
                K = 10;
            }
            return K;
        }

        private string setWinner(ObservableCollection<PlayerMatchCreate> Players)
        {
            int points = int.MinValue;
            string winner = "";
            for (int i = 0; i < Players.Count; i++)
            {
                if (Int32.Parse(Players[i].points) > points)
                {
                    winner = Players[i].Name;
                    points = Int32.Parse(Players[i].points);
                }
            }
            return winner;
        }

    }
}
