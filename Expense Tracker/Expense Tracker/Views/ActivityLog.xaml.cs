using Expense_Tracker.ViewModels;
using Xamarin.Forms;

namespace Expense_Tracker.Views
{
    public partial class ActivityLog : ContentPage
    {
        ActivityViewModel _viewModel;
        public ActivityLog()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ActivityViewModel("Activity");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
    
}