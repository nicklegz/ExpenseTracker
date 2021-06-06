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
        readonly List<string> categories;

        public MockExpenseStore()
        {
            friendExpenses = new List<FriendExpense>()
            {
                new FriendExpense{Id = Guid.NewGuid().ToString(), User = "Nick", Description = "Internet", Category = "Utility", Amount = 88.88m, Date = DateTime.Now.AddHours(-24)},
                new FriendExpense{Id = Guid.NewGuid().ToString(), User = "Nick", Description = "Hydro", Category = "Utility", Amount = 60.90m, Date = DateTime.Now.AddHours(-10)},
                new FriendExpense{Id = Guid.NewGuid().ToString(), User = "Amanda", Description = "Enbridge", Category = "Utility", Amount = 20.88m , Date = DateTime.Now.AddHours(-110)},
                new FriendExpense{Id = Guid.NewGuid().ToString(), User = "Amanda", Description = "Farm Boy", Category = "Food", Amount = 188.88m, Date = DateTime.Now.AddHours(-210)},
            }; 
            
            groupExpenses = new List<GroupExpense>()
            {
                new GroupExpense{Id = Guid.NewGuid().ToString(), User = "Nick", Description = "Internet", Category = "Utility", Amount = 88.88m, Date = DateTime.Now.AddHours(-24)},
                new GroupExpense{Id = Guid.NewGuid().ToString(), User = "Nick", Description = "Hydro", Category = "Utility", Amount = 60.90m, Date = DateTime.Now.AddHours(-10)},
                new GroupExpense{Id = Guid.NewGuid().ToString(), User = "Amanda", Description = "Enbridge", Category = "Utility", Amount = 20.88m , Date = DateTime.Now.AddHours(-110)},
                new GroupExpense{Id = Guid.NewGuid().ToString(), User = "Amanda", Description = "Farm Boy", Category = "Food", Amount = 188.88m, Date = DateTime.Now.AddHours(-210)},
            };

            categories = new List<string>() { "Utility", "Entertainment", "Gas", "Food" };
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
    }
}
