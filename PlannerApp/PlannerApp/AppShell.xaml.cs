using PlannerApp.Models;
using PlannerApp.ViewModels;
using PlannerApp.Views;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace PlannerApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            //New Routing
            Routing.RegisterRoute(nameof(NewPlanPage), typeof(NewPlanPage));
            Routing.RegisterRoute(nameof(EditPlanPage), typeof(EditPlanPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
