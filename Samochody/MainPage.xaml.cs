using Samochody.api;
using Samochody.dtos;
using Samochody.models;
using Samochody.pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Samochody {
    public partial class MainPage :ContentPage {
        public MainPage() {
            InitializeComponent();
            CarList.RefreshCommand = new Command(async () => {
                CarList.ItemsSource = await App.Service.VehicleService.GetAllVehiclesAsync();
                CarList.IsRefreshing = false;
            });



        }
        protected override async void OnAppearing() {
            CarList.ItemsSource = (await App.Service.VehicleService.GetAllVehiclesAsync());
        }

        private async void CarList_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            if(e.SelectedItem is Vehicle vehicle && vehicle != null) {
                await Navigation.PushAsync(new AddEditPage(vehicle));
            }
        }

        private async void AddVehicle_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new AddEditPage());
        }
    }
}
