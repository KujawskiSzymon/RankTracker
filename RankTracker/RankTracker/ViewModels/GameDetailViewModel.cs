using RankTracker.Models;
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
        private string gameId;
        private string name;
        private ObservableCollection<Player> players;
        public string Id { get; set; }

        public Command AddPlayerCommand { get; }
        public Command CreateMatchCommand { get; }
        public Command LoadPlayersCommand { get; }

        public Command<Player> PlayerTapped { get; }

        public GameDetailViewModel()
        {
            Players = new ObservableCollection<Player>();
            AddPlayerCommand = new Command(OnAddPlayer);
            CreateMatchCommand = new Command(OnCreateMatch);
            LoadPlayersCommand = new Command(async () => await ExecuteLoadGamesCommand());
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

        public string GameId
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
                Game game = await GamesStore.GetGameAsync(Static.AppInfoStatic.currentGame.Id);
                var players = game.Players;
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

        public async void LoadGameId(string itemId)
        {
            try
            {
                Players.Clear();
                var item = await GamesStore.GetGameAsync(itemId);
                Id = item.Id;
                Name = item.Name;
               var p = item.Players;
                foreach (var player in p)
                {
                    Players.Add(player);
                }
                Static.AppInfoStatic.currentGame = item;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
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
    }
}
