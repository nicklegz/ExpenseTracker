using Expense_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expense_Tracker.Services
{
    public interface IFriendDataStore<T>
    {
        Task<bool> AddFriendExpenseAsync(FriendExpense item);
        Task<bool> UpdateFriendExpenseAsync(FriendExpense item);
        Task<bool> DeleteFriendExpenseAsync(string id);
        Task<FriendExpense> GetFriendExpenseAsync(string id);
        Task<IEnumerable<FriendExpense>> GetFriendExpensesAsync(bool forceRefresh = false);
        Task<IEnumerable<string>> GetCategoriesAsync(bool forceRefresh = false);
    }
}
