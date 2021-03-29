using PlannerApp.Models;
using PlannerApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PlannerApp.ViewModels
{
    
    public class TestViewModel:BindableObject
    {
        //Commands
        public Command BtnTest_Command { get; }
        //Public Variables
        public INotificationManager notificationManager;
        public int notificationNumber = 0;
        //Constructor
        public TestViewModel()
        {
            BtnTest_Command = new Command(ShowNotification);
            //
            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                //ShowNotification(evtData.Title, evtData.Message);
            };
        }
        
        //Function to Show all Notifications
        public void ShowNotification()
        {
            notificationNumber++;
            string title = $"Local Notification #{notificationNumber}";
            string message = $"You have now received {notificationNumber} notifications!";
            notificationManager.SendNotification(title, message, DateTime.Now.AddSeconds(10));
        }

        
        public void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
                //stackLayout.Children.Add(msg);
            });
        }
        
    }

}
