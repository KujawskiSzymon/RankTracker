using RankTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RankTracker.Views
{
    
    public partial class SelectPlayersPage : ContentPage
    {
        public SelectPlayersPage()
        {
            InitializeComponent();
            BindingContext = new SelectPlayersViewModel();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            SelectPlayersViewModel vm = (SelectPlayersViewModel)BindingContext;
            vm.CreateMatchCommand.ChangeCanExecute();
        }
    }
}