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
        //Get All Plans
        public async void getPlans()
        {
            nonfinishedPlans = new List<Plan>();
            collections = await App.Database.GetPlansAsync();
            foreach(var plan in collections)
            {
                if (!plan.Finished)
                {
                    nonfinishedPlans.Add(plan);
                }
            }
            PlansList = nonfinishedPlans;
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
        private void Setup()
        {

            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                foreach (var plan in collections)
                {
                    var timespan = plan.Date - DateTime.Now;
                    plan.TimeSpan = timespan;
                }

                PlansList = null;
                PlansList = nonfinishedPlans;
          
                return true;
            });

        }






    }
}
