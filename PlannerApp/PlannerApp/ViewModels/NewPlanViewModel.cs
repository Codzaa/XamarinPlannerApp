using PlannerApp.Models;
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
                PlanTitle= "HelpMePlease"
            };

            await App.Database.SavePlanAsync(newPlan);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
