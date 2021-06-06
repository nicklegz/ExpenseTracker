using Expense_Tracker.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Expense_Tracker.ViewModels
{
    public class NewExpenseViewModel : BaseViewModel
    {
        private string textAmount;
        public ObservableCollection<string> Categories { get; }
        public enum ExpenseTypes { Personal, Friend, Group }
        public ObservableCollection<ExpenseTypes> Types { get; }
        public Command LoadCategoriesCommand { get; }
        private string _expenseType;

        public NewExpenseViewModel(string title)
        {
            Title = title;
            Types = new ObservableCollection<ExpenseTypes>();
            Categories = new ObservableCollection<string>();
            LoadCategoriesCommand = new Command(async () => await ExecuteLoadCategoriesCommand());
            SaveCommand = new Command(OnSave, ValidateSave);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
     
        public string ExpenseType
        {
            get => _expenseType;
            set => SetProperty(ref _expenseType, value);
        }

        public string TextAmount
        {
            get => textAmount;
            set => SetProperty(ref textAmount, value);
        }

        async Task ExecuteLoadCategoriesCommand()
        {
            IsBusy = true;

            try
            {
                Categories.Clear();
                var categories = await FriendDataStore.GetCategoriesAsync(true);
                foreach (var c in categories)
                {
                    Categories.Add(c);
                }

                Types.Clear();
                Types.Add(ExpenseTypes.Friend);
                Types.Add(ExpenseTypes.Group);
                Types.Add(ExpenseTypes.Personal);
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
        public bool ValidateSave()
        {
            decimal decAmount;

            return !String.IsNullOrWhiteSpace(_description)
                && !String.IsNullOrWhiteSpace(_category)
                && Decimal.TryParse(textAmount, out decAmount);
        }

        public Command SaveCommand { get; }

        public async void OnSave()
        {
            ExpenseTypes type;

            bool success = Enum.TryParse<ExpenseTypes>(_expenseType, out type);

            if(!success)
            {
                //add something to throw exception
                return;
            }

            switch (type)
            {
                case ExpenseTypes.Friend:
                {
                    FriendExpense friendExpense = new FriendExpense()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Description = _description,
                        Category = _category,
                        Amount = Convert.ToDecimal(textAmount),
                        Date = DateTime.Now
                    };

                    await FriendDataStore.AddFriendExpenseAsync(friendExpense);
                    await Shell.Current.GoToAsync("..");
                    break;
                }

                case ExpenseTypes.Group:
                {
                    GroupExpense groupExpense = new GroupExpense()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Description = _description,
                        Category = _category,
                        Amount = Convert.ToDecimal(textAmount),
                        Date = DateTime.Now
                    };

                    await GroupDataStore.AddGroupExpenseAsync(groupExpense);
                    await Shell.Current.GoToAsync("..");
                    break;
                }
        }
        }
    }
}
