using PlannerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PlannerApp.ViewModels
{
    public class CompletedPlansViewModel:BindableObject
    {
        public CompletedPlansViewModel()
        {

        }

        public void OnAppearing()
        {
            //Get All Completed Plans
            getPlans();

        }
        //
        public List<Plan> plansList;
        public List<Plan> PlansList
        {
            get => plansList;
            set
            {
                if (value == plansList) { return; }
                plansList = value;
                OnPropertyChanged();
            }
        }
        //
        public List<Plan> collections = new List<Plan>();
        public List<Plan> finishedPlans = new List<Plan>();
        public async void getPlans()
        {
            collections = await App.Database.GetPlansAsync();
            finishedPlans = new List<Plan>();
            //PlansList = collections;
            foreach(var plan in collections)
            {
                if (plan.Finished)
                {
                    finishedPlans.Add(plan);
                }
            }
            //
            PlansList = finishedPlans;
            getSavedTime();
        }

        //Calculate Time Saved
        private void getSavedTime()
        {

            foreach (var plan in finishedPlans)
            {
                var timespan = plan.Date - DateTime.Now;
                plan.SavedTime = timespan.TotalHours.ToString();
            }

            //
            //var savedTimeSpan = plan.Date - DateTime.Now;

        }
    }
}
