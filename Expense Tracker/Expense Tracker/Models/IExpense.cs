using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker.Models
{
    public interface IExpense
    {
        string Id { get; set; }
        string User { get; set; }
        string Description { get; set; }
        string Category { get; set; }
        decimal Amount { get; set; }
        DateTime Date { get; set; }
    }
}
