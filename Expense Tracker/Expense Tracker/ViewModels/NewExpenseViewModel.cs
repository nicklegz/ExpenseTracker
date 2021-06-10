using Expense_Tracker.Models;
using Expense_Tracker.Views;
using System;
using System.Collections.Generic;
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
        public ObservableCollection<SearchResult> SearchResults { get; }
        public enum ExpenseTypes { Personal, Friend, Group }
        public ObservableCollection<ExpenseTypes> Types { get; }
        public Command LoadCategoriesCommand { get; }
        public Command SaveCommand { get; }
        public Command AddFriendCommand { get; }
        public Command AddGroupCommand { get; }

        private Command _searchCommand;
        private string _searchText;
        private string _expenseType;
        private string _result;
        private bool _isVisible;

        public NewExpenseViewModel(string title)
        {
            Title = title;
            IsVisible = false;
            SearchResults = new ObservableCollection<SearchResult>();
            Types = new ObservableCollection<ExpenseTypes>();
            Categories = new ObservableCollection<string>();
            LoadCategoriesCommand = new Command(async () => await ExecuteLoadCategoriesCommand());
            SaveCommand = new Command(OnSave, ValidateSave);
            SearchText = _searchText;
            AddFriendCommand = new Command(OnAddFriend);
            AddGroupCommand = new Command(OnAddGroup);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }


        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        public string Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        public Command SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command(async () => await ExecuteSearchCommand()));
            }
        }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private async Task ExecuteSearchCommand()
        {
            SearchResults.Clear();

            if (String.IsNullOrEmpty(SearchText))
            {
                IsVisible = false;
                return;
            }

            var groups = await GroupDataStore.GetGroupsAsync();
            foreach(var group in groups)
            {
                if(group.Name.StartsWith(SearchText))
                {
                    IsVisible = true;
                    SearchResults.Add(new SearchResult() { Result = group.Name });
                }
            }

            var friends = await FriendDataStore.GetFriendsAsync();
            foreach(var friend in friends)
            {
                if(friend.FirstName.StartsWith(SearchText))
                {
                    IsVisible = true;
                    SearchResults.Add(new SearchResult() { Result = friend.FirstName });
                }
            }
        }

        private async void OnAddFriend(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AddFriendPage));
        }

        private async void OnAddGroup(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AddGroupPage));
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
