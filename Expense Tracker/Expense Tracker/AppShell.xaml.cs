using Expense_Tracker.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Expense_Tracker
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewExpensePage), typeof(NewExpensePage));
        }

    }
}
