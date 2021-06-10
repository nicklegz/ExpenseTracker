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
    public partial class AddFriendPage : ContentPage
    {
        AddFriendViewModel _viewModel;

        public AddFriendPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new AddFriendViewModel("Add Friend");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}