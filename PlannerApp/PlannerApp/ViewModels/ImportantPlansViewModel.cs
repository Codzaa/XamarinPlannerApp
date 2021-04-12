using PlannerApp.Models;
using PlannerApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PlannerApp.ViewModels
{
    public class ImportantPlansViewModel:BindableObject
    {
       
        public Command<Plan> ItemTapped { get; }

        public ImportantPlansViewModel()
        {
            ItemTapped = new Command<Plan>(OnPlanSelected);
        }

        public void OnAppearing()
        {

            //Get All Plans
            getPlans();
            //
            SelectedItem = null;
            //
            //ShowNotification();

        }
        //Public Variables
        private Plan _selectedItem;
        public Plan SelectedItem
        {
            get => _selectedItem;
            set
            {
                //SetProperty(ref _selectedItem, value);
                OnPlanSelected(value);
            }
        }
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
        public List<Plan> importantPlans = new List<Plan>();
        //Get All Plans
        public async void getPlans()
        {
            importantPlans = new List<Plan>();
            collections = await App.Database.GetPlansAsync();
            foreach (var plan in collections)
            {
                var timespan = plan.Date - DateTime.Now;
                plan.TimeSpan = timespan;
                
                if (plan.Priority == "high")
                {
                    importantPlans.Add(plan);
                }
            }
            PlansList = importantPlans;
            //
            Setup();
        }
        //On Item Tapped
        async void OnPlanSelected(Plan plan)
        {
            if (plan == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EditPlanPage)}?{nameof(EditPlanViewModel.PlanTitle)}={plan.PlanTitle}");
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        //
        public TimeSpan defaulttimer = new TimeSpan(0, 0, 1);
        private void Setup()
        {

            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                foreach (var plan in importantPlans)
                {
                    if(plan.TimeSpan >defaulttimer)
                    {
                        var timespan = plan.Date - DateTime.Now;
                        plan.TimeSpan = timespan;
                    }
                    
                }

                PlansList = null;
                PlansList = importantPlans;

            return true;
            });



        }
    }
}
