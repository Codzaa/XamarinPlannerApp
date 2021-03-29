using PlannerApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PlannerApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}