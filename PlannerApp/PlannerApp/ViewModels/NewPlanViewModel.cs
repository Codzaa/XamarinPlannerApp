using PlannerApp.Models;
using PlannerApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PlannerApp.ViewModels
{
    public class NewPlanViewModel : BindableObject
    {
        //Commands
        public Command CancelCommand { get; }
        public Command SaveCommand { get; }
 
        //
        public IList<Levels> LevelsList { get; set; }
        public NewPlanViewModel()
        {
            CancelCommand = new Command(OnCancel);
            SaveCommand = new Command(OnSave);
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

        //
        //
        public INotificationManager notificationManager;
        public int notificationNumber = 0;
        //

        public Levels priorityLevel;
        public Levels PriorityLevel
        {
            get => priorityLevel;
            set
            {
                if(value == priorityLevel) { return; }
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
                if(value == newTitle) { return; }
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
                if(value == newDescription) { return; }
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
                if(value == newDate) { return; }
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
                if(value == newTime) { return; }
                newTime = value;
            }
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
            if(PriorityLevel.Levelvalue == "high") 
            { 
                bgColor = "#EB9999"; 
            } else if(PriorityLevel.Levelvalue == "medium") 
            {
                bgColor = "#d6d636";
            }
            else
            {
                bgColor = "#ceedd9";
            }
            //

            Plan newPlan = new Plan()
            {

                PlanTitle = NewTitle,
                PlanDescription = NewDescription,
                Priority = PriorityLevel.Levelvalue,
                BgColor = bgColor,
                //Date = new DateTime(DateTime.Now.Ticks + new TimeSpan(0,0,0,10).Ticks)
                Date = new DateTime(newDate.Ticks + newTime.Ticks)
                
            };
            //
            ShowNotification(newPlan.Date);
            await App.Database.SavePlanAsync(newPlan);
            NewTitle = null;
            NewDescription = null;
            //NewDate = new DateTime(NewDate.Ticks  + newTime.Ticks);
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        ///
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
