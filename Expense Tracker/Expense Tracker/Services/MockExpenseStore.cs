using Expense_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Expense_Tracker.Services
{
    public class MockExpenseStore : IGroupDataStore<GroupExpense>, IFriendDataStore<FriendExpense> 
    {
        readonly List<FriendExpense> friendExpenses;
        readonly List<GroupExpense> groupExpenses;
        readonly List<Group> groups;
        readonly List<User> friends;
        readonly List<string> categories;
        readonly User nick = new User() { Id = "7a4c26c4-8de9-4a6f-8171-7de5d9c52193", FirstName = "Nick", LastName = "Legz" };
        readonly User amanda = new User() { Id = "06371d48-b34b-4bab-bddd-ea7ea7460159", FirstName = "Amanda", LastName = "Lavallee" };
        readonly GroupExpense gExp2 = new GroupExpense()
        {
            Id = Guid.NewGuid().ToString(),
            UserId = "7a4c26c4-8de9-4a6f-8171-7de5d9c52193",
            GroupId = "4b0d5cf0-2ecf-431a-bfca-8e4e7ca861f4",
            Description = "Pizza",
            Category = "Food",
            Amount = 60.90m,
            Date = DateTime.Now.AddHours(-10),
            ExpenseSplitPercentage = 0.5m
        };

        readonly GroupExpense gExp1 = new GroupExpense()
        {
            Id = Guid.NewGuid().ToString(),
            UserId = "7a4c26c4-8de9-4a6f-8171-7de5d9c52193",
            GroupId = "4b0d5cf0-2ecf-431a-bfca-8e4e7ca861f4",
            Description = "Coffee",
            Category = "Food",
            Amount = 88.88m,
            Date = DateTime.Now.AddHours(-24),
            ExpenseSplitPercentage = 0.5m
        };

        public MockExpenseStore()
        {

            friendExpenses = new List<FriendExpense>()
            {
                new FriendExpense
                {
                    Id = Guid.NewGuid().ToString(), 
                    UserId = "7a4c26c4-8de9-4a6f-8171-7de5d9c52193", 
                    FriendId = "06371d48-b34b-4bab-bddd-ea7ea7460159",
                    Description = "Internet", 
                    Category = "Utility", 
                    Amount = 88.88m, 
                    Date = DateTime.Now.AddHours(-24),
                    ExpenseSplitPercentage = 0.5m
                },
                new FriendExpense
                {
                    Id = Guid.NewGuid().ToString(), 
                    UserId = "7a4c26c4-8de9-4a6f-8171-7de5d9c52193", 
                    FriendId = "06371d48-b34b-4bab-bddd-ea7ea7460159",
                    Description = "Hydro", 
                    Category = "Utility", 
                    Amount = 60.90m, 
                    Date = DateTime.Now.AddHours(-10),
                    ExpenseSplitPercentage = 0.5m
                },
                new FriendExpense
                {
                    Id = Guid.NewGuid().ToString(), 
                    UserId = "06371d48-b34b-4bab-bddd-ea7ea7460159",
                    FriendId = "7a4c26c4-8de9-4a6f-8171-7de5d9c52193",
                    Description = "Enbridge", 
                    Category = "Utility", 
                    Amount = 20.88m ,                     
                    Date = DateTime.Now.AddHours(-110),
                    ExpenseSplitPercentage = 0.5m
                },
                new FriendExpense
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = "06371d48-b34b-4bab-bddd-ea7ea7460159",
                    FriendId = "7a4c26c4-8de9-4a6f-8171-7de5d9c52193",
                    Description = "Farm Boy",
                    Category = "Food",
                    Amount = 188.88m,
                    Date = DateTime.Now.AddHours(-210),
                    ExpenseSplitPercentage = 0.5m
                }
            };

            groupExpenses = new List<GroupExpense>()
            {
                gExp1,
                gExp2,
                new GroupExpense
                {
                    Id = Guid.NewGuid().ToString(), 
                    UserId = "Amanda",
                    GroupId = "4b0d5cf0-2ecf-431a-bfca-8e4e7ca861f4",
                    Description = "Alcatraz", 
                    Category = "Entertainment", 
                    Amount = 20.88m , 
                    Date = DateTime.Now.AddHours(-110),
                    ExpenseSplitPercentage = 0.5m
                },
                new GroupExpense
                {
                    Id = Guid.NewGuid().ToString(), 
                    UserId = "Amanda",
                    GroupId = "4b0d5cf0-2ecf-431a-bfca-8e4e7ca861f4",
                    Description = "Coffee", 
                    Category = "Food", 
                    Amount = 188.88m, 
                    Date = DateTime.Now.AddHours(-210),
                    ExpenseSplitPercentage = 0.5m
                },
            };

            categories = new List<string>() { "Utility", "Entertainment", "Gas", "Food" };

            groups = new List<Group>()
            {
                new Group { Id = "4b0d5cf0-2ecf-431a-bfca-8e4e7ca861f4", Expenses = new List<GroupExpense>() { gExp1, gExp2 }, Name = "San Fran", Users = new List<User>() { nick, amanda } }
            };

            friends = new List<User>()
            {
                nick,
                amanda
            };

        }

        public async Task<bool> AddFriendExpenseAsync(FriendExpense expense)
        {
            friendExpenses.Add(expense);

            return await Task.FromResult(true);
        }

        public async Task<bool> AddGroupExpenseAsync(GroupExpense expense)
        {
            groupExpenses.Add(expense);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateFriendExpenseAsync(FriendExpense expense)
        {
            var oldItem = friendExpenses.Where((FriendExpense arg) => arg.Id == expense.Id).FirstOrDefault();
            friendExpenses.Remove(oldItem);
            friendExpenses.Add(expense);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateGroupExpenseAsync(GroupExpense expense)
        {
            var oldItem = groupExpenses.Where((GroupExpense arg) => arg.Id == expense.Id).FirstOrDefault();
            groupExpenses.Remove(oldItem);
            groupExpenses.Add(expense);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteFriendExpenseAsync(string id)
        {
            var oldItem = friendExpenses.Where((FriendExpense arg) => arg.Id == id).FirstOrDefault();
            friendExpenses.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteGroupExpenseAsync(string id)
        {
            var oldItem = groupExpenses.Where((GroupExpense arg) => arg.Id == id).FirstOrDefault();
            groupExpenses.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<FriendExpense> GetFriendExpenseAsync(string id)
        {
            return await Task.FromResult(friendExpenses.FirstOrDefault(s => s.Id == id));
        }

        public async Task<GroupExpense> GetGroupExpenseAsync(string id)
        {
            return await Task.FromResult(groupExpenses.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<FriendExpense>> GetFriendExpensesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(friendExpenses);
        }

        public async Task<IEnumerable<GroupExpense>> GetGroupExpensesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(groupExpenses);
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(categories);
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(groups);
        }

        public async Task<Group> GetGroupAsync(string id)
        {
            return await Task.FromResult(groups.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<User>> GetFriendsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(friends);
        }

        public async Task<User> GetFriendAsync(string id)
        {
            return await Task.FromResult(friends.FirstOrDefault(s => s.Id == id));
        }

    }
}
