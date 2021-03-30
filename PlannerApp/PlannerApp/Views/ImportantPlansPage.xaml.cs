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
    public partial class ImportantPlansPage : ContentPage
    {
        public ImportantPlansPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //
            var vm = BindingContext as ImportantPlansViewModel;
            //
            vm.OnAppearing();
        }
    }
}