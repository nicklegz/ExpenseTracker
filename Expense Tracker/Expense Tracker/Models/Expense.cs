using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker.Models
{
    public class Expense : IExpense
    {
        public string Id { get; set; }
        public string User { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

    }
}
