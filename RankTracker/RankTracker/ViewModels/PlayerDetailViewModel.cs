using RankTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RankTracker.ViewModels
{
    [QueryProperty(nameof(PlayerId), nameof(PlayerId))]
   public class PlayerDetailViewModel : BaseViewModel
    {
        private string playerid;
        private string name;
        
        
        public ObservableCollection<PlayerHistory> PlayerHistory { get;  }
        public string Id { get; set; }
        public Command LoadPlayerHistoryCommand { get; set; }

        public PlayerDetailViewModel()
        {
            PlayerHistory = new ObservableCollection<PlayerHistory>();
            LoadPlayerHistoryCommand = new Command(async () => await ExecuteLoadGamesCommand());
        }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string PlayerId
        {
            get
            {
                return playerid;
            }
            set
            {
                playerid = value;
                LoadPlayerId(value);
            }
        }

        async Task ExecuteLoadGamesCommand()
        {
            IsBusy = true;

            try
            {
                PlayerHistory.Clear();   
                Player p = await GamesStore.GetPlayerAsync(Static.AppInfoStatic.currentGame, Static.AppInfoStatic.currentPlayer.Id);
                var playerhistory = p.PlayerHistory;
                foreach( var ph in playerhistory)
                {
                    PlayerHistory.Add(ph);
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        public async void LoadPlayerId(string itemId)
        {
            try
            {
               
                var item = await GamesStore.GetPlayerAsync(Static.AppInfoStatic.currentGame, itemId);
                Id = item.Id;
                Name = item.Name;
                Static.AppInfoStatic.currentPlayer = item;
                var playerhistory = item.PlayerHistory;
                foreach (var ph in playerhistory)
                {
                    PlayerHistory.Add(ph);
                }

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
