using Expense_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expense_Tracker.Services
{
    public interface IGroupDataStore<GroupExpense>
    {
        Task<bool> AddGroupExpenseAsync(GroupExpense item);
        Task<bool> UpdateGroupExpenseAsync(GroupExpense item);
        Task<bool> DeleteGroupExpenseAsync(string id);
        Task<GroupExpense> GetGroupExpenseAsync(string id);
        Task<IEnumerable<GroupExpense>> GetGroupExpensesAsync(bool forceRefresh = false);
        Task<IEnumerable<string>> GetCategoriesAsync(bool forceRefresh = false);
    }
}
