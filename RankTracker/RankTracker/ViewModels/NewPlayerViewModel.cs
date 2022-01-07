using RankTracker.Models;
using RankTracker.Services;
using RankTracker.Static;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RankTracker.ViewModels
{
    class NewPlayerViewModel : BaseViewModel
    {
            private string name;
            private string rank;
            public Game game;

            public NewPlayerViewModel(Game game)
            {
                this.game = game;
                SaveCommand = new Command(OnSave, ValidateSave);
                CancelCommand = new Command(OnCancel);
                this.PropertyChanged +=
                    (_, __) => SaveCommand.ChangeCanExecute();
            }

            private bool ValidateSave()
            {
                int value=0;
                Int32.TryParse(Rank, out value);
                return !String.IsNullOrWhiteSpace(name) && value!=0;

            }

            public string Rank
            {
                get => rank;
                set => SetProperty(ref rank, value);

            }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);

        }

        public Command SaveCommand { get; }
            public Command CancelCommand { get; }

            private async void OnCancel()
            {
                // This will pop the current page off the navigation stack
                await Shell.Current.GoToAsync("..");
            }

            private async void OnSave()
            {
            Player newItem = new Player()
            {

                Name = Name,
                Rank = Int32.Parse(Rank),
                GameId = AppInfoStatic.currentGame.Id
                };
            GameDataStore database = await GameDataStore.Instance;
            await database.SavePlayerAsync(newItem);
            

                // This will pop the current page off the navigation stack
                await Shell.Current.GoToAsync("..");
            }
        }
    }



