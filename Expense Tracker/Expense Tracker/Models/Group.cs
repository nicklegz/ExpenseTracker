using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker.Models
{
    public class Group
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<User> Users { get; set; }
        public IList<GroupExpense> Expenses { get; set; }
    }
}
