using Expense_Tracker.ViewModels;
using OxyPlot;
using OxyPlot.Series;
using Xamarin.Forms;

namespace Expense_Tracker.Views
{
    public partial class SummaryPage : ContentPage
    {
        ExpensesViewModel _viewModel;

        public SummaryPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ExpensesViewModel("Summary");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}