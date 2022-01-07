using RankTracker.Models;
using RankTracker.Services;
using RankTracker.Static;
using RankTracker.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RankTracker.ViewModels
{
    public class GamesViewModel : BaseViewModel
    {
        private Game _selectedGame;

        public ObservableCollection<Game> Games { get; }
        public Command LoadGamesCommand { get; }
        public Command AddGameCommand { get; }
        public Command<Game> GameTapped { get; }

        public GamesViewModel()
        {
            Title = "Games";
             Games = new ObservableCollection<Game>();
            LoadGamesCommand = new Command(async () => await ExecuteLoadGamesCommand());

            GameTapped = new Command<Game>(OnGameSelected);

            AddGameCommand = new Command(OnAddGame);
        }

        async Task ExecuteLoadGamesCommand()
        {
            IsBusy = true;

            try
            {
                Games.Clear();
                GameDataStore dataStore = await GameDataStore.Instance;
                List<Game> games = await dataStore.GetGamesAsync();
                foreach (var item in games)
                {
                    Games.Add(item);
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
            SelectedGame = null;
        }

        public Game SelectedGame
        {
            get => _selectedGame;
            set
            {
                SetProperty(ref _selectedGame, value);
                OnGameSelected(value);
            }
        }

        private async void OnAddGame(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewGamePage));
        }

        async void OnGameSelected(Game item)
        {
            AppInfoStatic.currentGame = item;
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(GameDetailPage)}?{nameof(GameDetailViewModel.GameId)}={item.Id}");
        }
    }
}