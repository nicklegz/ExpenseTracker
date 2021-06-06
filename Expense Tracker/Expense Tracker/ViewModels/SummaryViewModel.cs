using Expense_Tracker.Models;
using Expense_Tracker.Views;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Expense_Tracker.ViewModels
{ 
    public class SummaryViewModel : BaseViewModel
    {
        public ObservableCollection<FriendExpense> FriendExpenses { get; }
        public ObservableCollection<GroupExpense> GroupExpenses { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public PlotModel PieModel { get; set; }

        private Dictionary<string, double> _pieCategories;

        private decimal _totalExpense;

        public SummaryViewModel(string title)
        {
            Title = title;
            FriendExpenses = new ObservableCollection<FriendExpense>();
            GroupExpenses = new ObservableCollection<GroupExpense>();
            PieCategories = new Dictionary<string, double>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AddItemCommand = new Command(OnAddItem);
            TotalExpenses = _totalExpense;
            PieModel = new PlotModel();
        }

        public Dictionary<string, double> PieCategories
        {
            get => _pieCategories;
            set => SetProperty(ref _pieCategories, value);
        }

        public decimal TotalExpenses
        {
            get => _totalExpense;
            set => SetProperty(ref _totalExpense, value);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                FriendExpenses.Clear();
                GroupExpenses.Clear();
                TotalExpenses = 0;

                var friendExp = await FriendDataStore.GetFriendExpensesAsync(true);
                foreach (var exp in friendExp)
                {
                    FriendExpenses.Add(exp);
                    TotalExpenses += exp.Amount;
                }

                var groupExp = await GroupDataStore.GetGroupExpensesAsync(true);
                foreach(var exp in groupExp)
                {
                    GroupExpenses.Add(exp);
                    TotalExpenses += exp.Amount;
                }

                CreatePieChart();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewExpensePage));
        }

        public void CreatePieChart()
        {
            PieCategories.Clear();
            SetPieCategories();
            PieModel.InvalidatePlot(true);
            PieModel.Series.Clear();
            PieModel.LegendFontSize = 12;
            PieModel.PlotMargins = new OxyThickness(5);
            PieModel.DefaultColors = new List<OxyColor>
            {
                OxyColor.FromRgb(109, 121, 115),
                OxyColor.FromRgb(203, 145, 144),
                OxyColor.FromRgb(186, 73, 72),
                OxyColor.FromRgb(255, 232, 226),
            };

            var ps = new PieSeries
            {
                AngleSpan = 360,
                StartAngle = 0
            };


            foreach(var c in PieCategories)
            {
                ps.Slices.Add(new PieSlice(c.Key, c.Value) { IsExploded = true });
            }

            PieModel.Series.Add(ps);
        }

        async void SetPieCategories()
        {
            var categories = await FriendDataStore.GetCategoriesAsync(true);
            foreach (var c in categories)
            {
                var friendCount = Convert.ToDouble(FriendExpenses.Where(x => x.Category == c).Count());
                var groupCount = Convert.ToDouble(GroupExpenses.Where(x => x.Category == c).Count());

                var totalCount = friendCount + groupCount;

                if (totalCount != 0)
                {
                    PieCategories.Add(c, totalCount);
                }
            }
        }
    }
}
