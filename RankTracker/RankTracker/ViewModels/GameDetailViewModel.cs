using RankTracker.Models;
using RankTracker.Services;
using RankTracker.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RankTracker.ViewModels
{
    [QueryProperty(nameof(GameId), nameof(GameId))]
    public class GameDetailViewModel : BaseViewModel
    {
        private int gameId;
        private string name;
        private ObservableCollection<Player> players;
        public int Id { get; set; }

        public Command AddPlayerCommand { get; }
        public Command CreateMatchCommand { get; }
        public Command MatchHistoryCommand { get; }
        public Command LoadPlayersCommand { get; }

        public Command<Player> PlayerTapped { get; }

        public GameDetailViewModel()
        {
            Players = new ObservableCollection<Player>();
            AddPlayerCommand = new Command(OnAddPlayer);
            CreateMatchCommand = new Command(OnCreateMatch);
            LoadPlayersCommand = new Command(async () => await ExecuteLoadGamesCommand());
            MatchHistoryCommand = new Command(OnCheckHistory);
            PlayerTapped = new Command<Player>(OnPlayerSelected);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public ObservableCollection<Player> Players
        {
            get => players;
            set => SetProperty(ref players, value);
        }

        public int GameId
        {
            get
            {
                return gameId;
            }
            set
            {
                gameId = value;
                LoadGameId(value);
               
            }
        }

        async Task ExecuteLoadGamesCommand()
        {
            IsBusy = true;

            try
            {
                Players.Clear();
                GameDataStore dataStore = await GameDataStore.Instance; 
                var players = await dataStore.GetPlayersAsync(Static.AppInfoStatic.currentGame.Id);
                foreach (var p in players)
                {
                    Players.Add(p);
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

        async void OnPlayerSelected(Player item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(PlayerDetailPage)}?{nameof(PlayerDetailViewModel.PlayerId)}={item.Id}");
        }

        public async void LoadGameId(int itemId)
        {
            try
            {
                Players.Clear();
                GameDataStore dataStore = await GameDataStore.Instance;
                var players = await dataStore.GetPlayersAsync(itemId);
                foreach (var p in players)
                {
                    Players.Add(p);
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
        private async void OnAddPlayer(object obj)
        { 
            await Shell.Current.GoToAsync(nameof(NewPlayerPage));
        }
        private async void OnCreateMatch(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(SelectPlayersPage)}?{nameof(SelectPlayersViewModel.GameId)}={Static.AppInfoStatic.currentGame.Id}");
        }
        private async void OnCheckHistory(object obj)
        {
            await Shell.Current.GoToAsync(nameof(MatchHistoryPage));
        }
    }
}
