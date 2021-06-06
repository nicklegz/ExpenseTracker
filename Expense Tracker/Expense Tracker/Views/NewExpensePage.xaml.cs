using Expense_Tracker.Models;
using Expense_Tracker.ViewModels;
using Xamarin.Forms;

namespace Expense_Tracker.Views
{
    public partial class NewExpensePage : ContentPage
    {
        NewExpenseViewModel _viewModel;
        public FriendExpense FriendExpense { get; set; }
        public GroupExpense GroupExpense { get; set; }

        public NewExpensePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new NewExpenseViewModel("New Expense");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}