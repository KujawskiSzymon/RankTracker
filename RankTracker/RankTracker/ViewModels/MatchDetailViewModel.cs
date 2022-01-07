using RankTracker.Models;
using RankTracker.Services;
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
        private int matchId;
        public int Id { get; set; }
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

        public int MatchId
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

        public async void LoadMatchId(int itemId)
        {
            try
            {
                Matches.Clear();
                GameDataStore dataStore = await GameDataStore.Instance;
                Match item = await dataStore.GetMatchAsync(itemId);
                List<PlayerMatchInfo> pmi = await dataStore.GetPlayerMatchInfosAsync(item.Id);

                
                foreach (var player in pmi)
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
