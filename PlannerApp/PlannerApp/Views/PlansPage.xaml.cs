using PlannerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlannerApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlansPage : ContentPage
    {
        public PlansPage()
        {
            InitializeComponent();
        }

        //When the Page Appears or comes into view
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //
            var vm = BindingContext as PlansViewModel;
            //
            vm.OnAppearing();
        }
    }
}