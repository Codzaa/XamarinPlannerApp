using PlannerApp.Models;
using PlannerApp.Services;
using PlannerApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlannerApp.ViewModels
{
    public class PlansViewModel:BindableObject
    {
        //Commands
        public Command AddPlanCommand { get; }
        public Command EditPlanCommand { get; }
        //Constructor
        public PlansViewModel()
        {
            AddPlanCommand = new Command(AddPlanAsync);
            EditPlanCommand = new Command(EditPlanAsync);
            //
            ItemTapped = new Command<Plan>(OnPlanSelected);
            //
            
        }

        //Public Variables
        private Plan _selectedItem;
        public Command<Plan> ItemTapped { get; }



        //
        public void OnAppearing()
        {

            //Get All Plans
            getPlans();
            //
            SelectedItem = null;
            //
            //ShowNotification();

        }
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
                if(value == plansList) { return; }
                plansList = value;
                OnPropertyChanged();
            }
        }
        //
        public List<Plan> collections = new List<Plan>();
        public List<Plan> nonfinishedPlans = new List<Plan>();
        public TimeSpan defaulttimer = new TimeSpan(0, 0, 1);
        //Get All Plans
        public async void getPlans()
        {

            nonfinishedPlans = new List<Plan>();
            collections = await App.Database.GetPlansAsync();
            //

            //
            foreach (var plan in collections)
            {
                //
                var timespan = plan.Date - DateTime.Now;
                plan.TimeSpan = timespan;
                //
                if (!plan.Finished)
                {
                    if(plan.TimeSpan > defaulttimer)
                      {
                        nonfinishedPlans.Add(plan);
                    }
                    else
                    {
                        UpdateDataBase(plan);
                    }
                        
                }
            }
            PlansList = nonfinishedPlans;
            //updatedPlans = nonfinishedPlans;
            //
            Setup();
        }

        //Add Plan
        public async void AddPlanAsync()
        {

            //await App.Database.SavePlanAsync(tempAlbumObj);
            await Shell.Current.GoToAsync(nameof(NewPlanPage));
        }

        //Delete Plan
        public async void EditPlanAsync()
        {

            //
            await Shell.Current.GoToAsync(nameof(EditPlanPage));
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
        public List<Plan> updatedPlans = new List<Plan>();
        private void Setup()
        {

            //updatedPlans = nonfinishedPlans;
            
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                
                Device.BeginInvokeOnMainThread(() =>
                {
                    // interact with UI elements
                    //PlansList = null;  
                    foreach (var plan in nonfinishedPlans)
                    {
                        
                        //updatedPlans.Add(plan);
                        //
                        if (plan.TimeSpan < defaulttimer)
                        {
                            //updatedPlans.Add(plan);
                            plan.BgColor = "#959695";
                            plan.Finished = true;
                            plan.Priority = null;
                            //Break;
                            //await App.Database.EditPlanAsync(PlanObj);
                            UpdateDataBase(plan);

                        }
                        else
                        {
                            var timespan = plan.Date - DateTime.Now;
                            plan.TimeSpan = timespan;
                        }
                        
                    }

                    PlansList = null;
                    PlansList = nonfinishedPlans;
                    //updatedPlans = null;
                });



                return true;
            });
        }

        //public Plan tempPlanObj;
        public async void UpdateDataBase(Plan tempPlanObj)
        {

            await App.Database.EditPlanAsync(tempPlanObj);
            nonfinishedPlans.Remove(tempPlanObj);

        }






    }
}
