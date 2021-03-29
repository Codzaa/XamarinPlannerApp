using PlannerApp.Models;
using System;
using System.Collections.Generic;
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
        //constructor
        public EditPlanViewModel()
        {
            CancelCommand = new Command(OnCancel);
            SaveCommand = new Command(OnSave);
            DeleteCommand = new Command(OnDelete);
        }

        //Public Variables
        private Plan planObj;
        public string planTitle;
        //
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
            var tempObj = new Plan();
            tempObj.PlanTitle = "Things Change";
            PlanObj.PlanTitle = tempObj.PlanTitle;
           
            await App.Database.EditPlanAsync(PlanObj);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        private async void OnDelete()
        {
           

            await App.Database.DeletePlanAsync(PlanObj);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
