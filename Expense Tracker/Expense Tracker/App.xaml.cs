using Expense_Tracker.Services;
using Expense_Tracker.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Expense_Tracker
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockExpenseStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
