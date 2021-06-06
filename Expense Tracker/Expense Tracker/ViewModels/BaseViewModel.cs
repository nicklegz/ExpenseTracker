using Expense_Tracker.Models;
using Expense_Tracker.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Expense_Tracker.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IGroupDataStore<GroupExpense> GroupDataStore => DependencyService.Get<IGroupDataStore<GroupExpense>>();
        public IFriendDataStore<FriendExpense> FriendDataStore => DependencyService.Get<IFriendDataStore<FriendExpense>>();

        public string _description;
        public string _category;
        public decimal _amount;
        public DateTime _date;

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public decimal Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

        public DateTime Date 
        { 
            get => _date; 
            set => SetProperty(ref _date, value);
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
