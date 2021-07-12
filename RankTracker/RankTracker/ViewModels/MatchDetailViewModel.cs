using RankTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace RankTracker.ViewModels
{
    [QueryProperty(nameof(MatchId), nameof(MatchId))]
    public class MatchDetailViewModel : BaseViewModel
    {
        private string matchId;
        public string Id { get; set; }
        private ObservableCollection<PlayerMatchInfo> matches;

        public MatchDetailViewModel()
        {
            Matches = new ObservableCollection<PlayerMatchInfo>();
        }

        public ObservableCollection<PlayerMatchInfo> Matches
        {
            get => matches;
            set => SetProperty(ref matches, value);
        }

        public string MatchId
        {
            get
            {
                return matchId;
            }
            set
            {
                matchId = value;
                LoadMatchId(value);

            }
        }

        public async void LoadMatchId(string itemId)
        {
            try
            {
                Matches.Clear();
                Match item = await GamesStore.GetMatchAsync(Static.AppInfoStatic.currentGame,itemId);
                Id = item.Id;
                
                var p = item.Players;
                foreach (var player in p)
                {
                    Matches.Add(player);
                }
                
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
