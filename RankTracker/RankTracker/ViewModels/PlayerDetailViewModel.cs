using RankTracker.Models;
using RankTracker.Services;
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
        private int playerid;
        private string name;
        
        
        public ObservableCollection<PlayerHistory> PlayerHistory { get;  }
        public int Id { get; set; }
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

        public int PlayerId
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
                GameDataStore dataStore = await GameDataStore.Instance;
                Player player = await dataStore.GetPlayerAsync(PlayerId);
                
                


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

        public async void LoadPlayerId(int itemId)
        {
            try
            {

                GameDataStore dataStore = await GameDataStore.Instance;
                Player player = await dataStore.GetPlayerAsync(itemId);
                Id = player.Id;
                Name = player.Name;
                Static.AppInfoStatic.currentPlayer = player;
                List<PlayerHistory> playerHistory = await dataStore.GetPlayerHistoryAsync(itemId);
                foreach (var ph in playerHistory)
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
