using Expense_Tracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Expense_Tracker.Views
{
    public partial class Friends : ContentPage
    {
        FriendsViewModel _viewModel;
        public Friends()
        {
            InitializeComponent();
            BindingContext = _viewModel = new FriendsViewModel("Friends");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}