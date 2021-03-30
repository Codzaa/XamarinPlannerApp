using PlannerApp.Models;
using PlannerApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PlannerApp.ViewModels
{
    [QueryProperty(nameof(PlanTitle), nameof(PlanTitle))]
    public class EditPlanViewModel:BindableObject
    {
        //Commands
        public Command CancelCommand { get; }
        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }
        public Command CompleteCommand { get; }
        //constructor
        public EditPlanViewModel()
        {
            CancelCommand = new Command(OnCancel);
            SaveCommand = new Command(OnSave);
            DeleteCommand = new Command(OnDelete);
            CompleteCommand = new Command(OnComplete);
            //
            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                //ShowNotification(evtData.Title, evtData.Message);
            };
            //
            LevelsList = new ObservableCollection<Levels>();
            LevelsList.Add(new Levels { Levelvalue = "high" });
            LevelsList.Add(new Levels { Levelvalue = "medium" });
            LevelsList.Add(new Levels { Levelvalue = "normal" });
        }

        //Public Variables
        private Plan planObj;
        public string planTitle;
        public INotificationManager notificationManager;
        public int notificationNumber = 0;
        public IList<Levels> LevelsList { get; set; }
        //
        public Levels priorityLevel;
        public Levels PriorityLevel
        {
            get => priorityLevel;
            set
            {
                if (value == priorityLevel) { return; }
                priorityLevel = value;
                OnPropertyChanged();
            }
        }
        public string newTitle;
        public string NewTitle
        {
            get => newTitle;
            set
            {
                if (value == newTitle) { return; }
                newTitle = value;
                OnPropertyChanged();
            }
        }
        //
        public string newDescription;
        public string NewDescription
        {
            get => newDescription;
            set
            {
                if (value == newDescription) { return; }
                newDescription = value;
                OnPropertyChanged();
            }
        }
        //
        public DateTime newDate;
        public DateTime NewDate
        {
            get => newDate;
            set
            {
                if (value == newDate) { return; }
                newDate = value;
                OnPropertyChanged();
            }
        }
        //
        public TimeSpan newTime;
        public TimeSpan NewTime
        {
            get => newTime;
            set
            {
                if (value == newTime) { return; }
                newTime = value;
            }
        }
        public string PlanTitle
        {
            get => planTitle;
            set
            {
                //if(value == planTitle) { return; }
                planTitle = value;
                LoadPlan(value);
                OnPropertyChanged();
            }

        }
        public Plan PlanObj
        {
            get => planObj;
            set
            {
                planObj = value;
                //LoadPlan(value);
            }
        }

        //
        public async void LoadPlan(string pTitle)
        {
            //
            var result = await App.Database.SearchPlanAsync(pTitle);
            //Console.WriteLine(result.PlanTitle);
            PlanObj = result;
            NewTitle = PlanObj.PlanTitle;
            NewDescription = PlanObj.PlanDescription;
            NewDate = PlanObj.Date;
            NewTime = PlanObj.TimeSpan;
            //PriorityLevel.Levelvalue = PlanObj.Priority;
        }

        //On Cancel Functions
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        //Save Button Event Listener(Command)
        private async void OnSave()
        {
            if (NewTitle == null) { return; }
            if (NewDescription == null) { return; }
            if (NewDate == null) { return; }
            if (NewTime == null) { return; }
            if (PriorityLevel == null) { return; }
            //
            DateTime defaultDate = DateTime.Now;
            DateTime dt2 = newDate;
            //
            string bgColor;
            if (PriorityLevel.Levelvalue == "high")
            {
                bgColor = "#EB9999";
            }
            else if (PriorityLevel.Levelvalue == "medium")
            {
                bgColor = "#d6d636";
            }
            else
            {
                bgColor = "#ceedd9";
            }
            //
            //
            //ShowNotification(newPlan.Date);
           // await App.Database.SavePlanAsync(newPlan);
           // NewTitle = null;
            //NewDescription = null;
            //NewDate = new DateTime(NewDate.Ticks  + newTime.Ticks);
            // This will pop the current page off the navigation stack
            //await Shell.Current.GoToAsync("..");
            
            var tempObj = new Plan();
            PlanObj.PlanTitle = tempObj.PlanTitle = newTitle;
            PlanObj.PlanDescription = tempObj.PlanDescription = NewDescription;
            PlanObj.Priority = tempObj.Priority = PriorityLevel.Levelvalue;
            PlanObj.BgColor = tempObj.BgColor = bgColor;
            PlanObj.TimeSpan = tempObj.TimeSpan = newTime;
            PlanObj.Date = tempObj.Date = new DateTime(newDate.Ticks + newTime.Ticks);
            //PlanObj = tempObj;
            //
            //Console.WriteLine(PlanTitle)
            ShowNotification(PlanObj.Date);
            //
            await App.Database.EditPlanAsync(PlanObj);
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        //
        private async void OnDelete()
        {
            await App.Database.DeletePlanAsync(PlanObj);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        //
        private async void OnComplete()
        {
            //
            PlanObj.Finished = true;
            PlanObj.BgColor = "#959695";
            await App.Database.EditPlanAsync(PlanObj);
            //
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        //
        public void ShowNotification(DateTime dtime)
        {
            //
            notificationNumber++;
            string title = $"Local Notification #{notificationNumber}";
            string message = $"You have now received {notificationNumber} notifications!";
            var dat = new DateTime(dtime.Ticks);
            //
            notificationManager.SendNotification(title, message, dat);
        }
    }
}
