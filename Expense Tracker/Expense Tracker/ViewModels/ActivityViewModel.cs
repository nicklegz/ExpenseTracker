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

        public ActivityViewModel(string title)
        {
            Title = title;
            Expenses = new ObservableCollection<Expense>();
            FriendExpenses = new ObservableCollection<FriendExpense>();
            GroupExpenses = new ObservableCollection<GroupExpense>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
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
                    Expenses.Add(new Expense
                    {
                        Id = exp.Id,
                        User = exp.User,
                        Description = exp.Description,
                        Category = exp.Category,
                        Amount = exp.Amount,
                        Date = exp.Date
                    });
                }

                var groupExp = await GroupDataStore.GetGroupExpensesAsync(true);
                foreach (var exp in groupExp)
                {
                    Expenses.Add(new Expense
                    {
                        Id = exp.Id,
                        User = exp.User,
                        Description = exp.Description,
                        Category = exp.Category,
                        Amount = exp.Amount,
                        Date = exp.Date
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
