using Expense_Tracker.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Expense_Tracker.ViewModels
{
    public class ActivityViewModel : BaseViewModel
    {
        public ObservableCollection<FriendExpense> FriendExpenses { get; }
        public ObservableCollection<GroupExpense> GroupExpenses { get; }
        public ObservableCollection<Expense> Expenses { get; }
        public Command LoadItemsCommand { get; }
        private decimal _expenseSplit;
        private string _splitWithId;

        public ActivityViewModel(string title)
        {
            Title = title;
            Expenses = new ObservableCollection<Expense>();
            FriendExpenses = new ObservableCollection<FriendExpense>();
            GroupExpenses = new ObservableCollection<GroupExpense>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        public decimal ExpenseSplitPercentage
        {
            get => _expenseSplit;
            set => SetProperty(ref _expenseSplit, value);
        }

        public string SplitWith
        {
            get => _splitWithId;
            set => SetProperty(ref _splitWithId, value);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                FriendExpenses.Clear();
                GroupExpenses.Clear();
                Expenses.Clear();

                var friendExp = await FriendDataStore.GetFriendExpensesAsync(true);
                foreach (var exp in friendExp)
                {
                    var friend = await FriendDataStore.GetFriendAsync(exp.FriendId);

                    Expenses.Add(new Expense
                    {
                        Id = exp.Id,
                        UserId = exp.UserId,
                        Description = exp.Description,
                        Category = exp.Category,
                        Amount = exp.Amount,
                        Date = exp.Date,
                        ExpenseSplitPercentage = exp.ExpenseSplitPercentage * 100,
                        SplitWith = friend.FirstName,
                        ExpenseType = "Friend"
                    });
                }


                var groupExp = await GroupDataStore.GetGroupExpensesAsync(true);
                foreach (var exp in groupExp)
                {
                    var group = await GroupDataStore.GetGroupAsync(exp.GroupId);

                    Expenses.Add(new Expense
                    {
                        Id = exp.Id,
                        UserId = exp.UserId,
                        Description = exp.Description,
                        Category = exp.Category,
                        Amount = exp.Amount,
                        Date = exp.Date,
                        ExpenseSplitPercentage = exp.ExpenseSplitPercentage * 100,
                        SplitWith = group.Name,
                        ExpenseType = "Group"
                    });
                }
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
    }
}
