using ESCde.Cars.MobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ESCde.Cars.MobileApp
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel vm;
        public MainPage()
        {
            InitializeComponent();
            vm = BindingContext as MainPageViewModel;
            CarList.ItemsSource = vm.Cars;
        }

        private async void Refresh(object sender, EventArgs e)
        {
            await vm.RefreshFromServerAsync();
            //load and refresh
        }
    }
}
