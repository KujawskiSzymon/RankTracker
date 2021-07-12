﻿using RankTracker.Models;
using RankTracker.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RankTracker.ViewModels
{
   public class MatchHistoryViewModel:  BaseViewModel
    {
        private ObservableCollection<Match> matches;
        public Command<Match> MatchHistoryTapped { get; }
        public MatchHistoryViewModel()
        {
            Matches = new ObservableCollection<Match>();
            LoadMatchesCommand = new Command(async () => await ExecuteLoadGamesCommand());
            ExecuteLoadGamesCommand();
            MatchHistoryTapped = new Command<Match>(OnMatchSelected);
        }
        public Command LoadMatchesCommand { get; }
        public ObservableCollection<Match> Matches
        {
            get => matches;
            set => SetProperty(ref matches, value);
        }
        async Task ExecuteLoadGamesCommand()
        {
            IsBusy = true;

            try
            {
                Matches.Clear();
                Game game = await GamesStore.GetGameAsync(Static.AppInfoStatic.currentGame.Id);
                var matches = game.Matches;
                foreach (var p in matches)
                {
                    Matches.Add(p);
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
        async void OnMatchSelected(Match item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(MatchDetailPage)}?{nameof(MatchDetailViewModel.MatchId)}={item.Id}");
        }
    }
}
