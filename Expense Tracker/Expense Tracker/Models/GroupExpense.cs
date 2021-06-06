using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker.Models
{
    public class GroupExpense : IExpense
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string GroupId { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public decimal ExpenseSplitPercentage { get; set; }

    }
}
