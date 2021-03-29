using PlannerApp.Models;
using PlannerApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PlannerApp.ViewModels
{
    public class NewPlanViewModel:BindableObject
    {
        //Commands
        public Command CancelCommand { get; }
        public Command SaveCommand { get; }
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
        }

        //
        //
        public INotificationManager notificationManager;
        public int notificationNumber = 0;
        //
        public string newTitle;
        public string NewTitle
        {

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
            Plan newPlan = new Plan()
            {
                PlanTitle= "Help",
                PlanDescription = "The Maddhatttttaaaa",
                BgColor = "#EB9999",
                Date = new DateTime(DateTime.Now.Ticks + new TimeSpan(0, 0, 0, 20).Ticks)
            };
            //
            ShowNotification(newPlan.Date);
            await App.Database.SavePlanAsync(newPlan);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        ///
        public void ShowNotification(DateTime dtime)
        {
            notificationNumber++;
            string title = $"Local Notification #{notificationNumber}";
            string message = $"You have now received {notificationNumber} notifications!";
            var dat = new DateTime(dtime.Ticks);
            //
            notificationManager.SendNotification(title, message, dat);
        }


    }
}
