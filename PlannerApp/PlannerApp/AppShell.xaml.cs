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
            //New Routing
            Routing.RegisterRoute(nameof(NewPlanPage), typeof(NewPlanPage));
            Routing.RegisterRoute(nameof(EditPlanPage), typeof(EditPlanPage));
        }

    }
}
