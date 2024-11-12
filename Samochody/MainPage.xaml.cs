using Samochody.api;
using Samochody.dtos;
using Samochody.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Samochody {
    public partial class MainPage :TabbedPage {
        Vehicle selectedVehicle = null;
        public MainPage() {
            InitializeComponent();
            CarList.RefreshCommand = new Command(async () => {
                CarList.ItemsSource = await App.Service.VehicleService.GetAllVehiclesAsync();
                CarList.IsRefreshing = false;
            });



        }
        protected override async void OnAppearing() {
            CarList.ItemsSource = (await App.Service.VehicleService.GetAllVehiclesAsync());
            Silnik.ItemsSource = await App.Service.Engine.GetAllEnginesAsync();
            Silnik.ItemDisplayBinding = new Binding("DisplayName");

            TypPojazdu.ItemsSource = await App.Service.VehicleTypeService.GetAllVehicleTypesAsync();
            TypPojazdu.ItemDisplayBinding = new Binding("Name");
            Console.WriteLine();
            base.OnAppearing();
        }

        private async void Dodaj_Clicked(object sender, EventArgs e) {

            VehicleDto vehicleDto = new VehicleDto() {
                BodyType = Nadwozie.Text,
                Color = Kolor.Text,
                Mileage = double.Parse(Przebieg.Text),
                Model = Model.Text,
                Price = decimal.Parse(Cena.Text),
                SeatingCapacity = int.Parse(IloscMiejs.Text),
                Year = int.Parse(Rok.Text),
                EngineID = (Silnik.SelectedItem as Engine).EngineID,
                GearboxID = 1,
                VehicleTypeID = (TypPojazdu.SelectedItem as VehicleType).VehicleTypeID,
            };

            if(selectedVehicle != null) {
                if(await App.Service.VehicleService.UpdateVehicleAsync(selectedVehicle.VehicleID,vehicleDto) != null) {
                    await DisplayAlert("Pomyslnie uaktualniono", "ok", "OK");
                }
                return;
            }


            if(await App.Service.VehicleService.CreateVehicleAsync(vehicleDto) != null) {
                await DisplayAlert("Pomyslnie dodano", "ok", "OK");
            }
            EmptyInputs();
        }

        private void CarList_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            if(e.SelectedItem as Vehicle == null)
                return;
            selectedVehicle = e.SelectedItem as Vehicle;

            Model.Text = selectedVehicle.Model;
            Kolor.Text = selectedVehicle.Color;
            Rok.Text = selectedVehicle.Year.ToString();
            Przebieg.Text = selectedVehicle.Mileage.ToString();
            IloscMiejs.Text = selectedVehicle.SeatingCapacity.ToString();
            Nadwozie.Text = selectedVehicle.BodyType.ToString();
            Cena.Text = selectedVehicle.Price.ToString();

            Silnik.SelectedItem = Silnik.ItemsSource.Cast<Engine>().FirstOrDefault(engine => engine.EngineID == selectedVehicle.EngineID);
            TypPojazdu.SelectedItem = TypPojazdu.ItemsSource.Cast<VehicleType>().FirstOrDefault(vt => vt.VehicleTypeID == selectedVehicle.VehicleTypeID);

        }

        private void Anuluj_Clicked(object sender, EventArgs e) {
            selectedVehicle = null;
            CarList.SelectedItem = null;
            EmptyInputs();
        }

        private async void Usun_Clicked(object sender, EventArgs e) {
            if(selectedVehicle == null)
                return;

            if(await App.Service.VehicleService.DeleteVehicleAsync(selectedVehicle.VehicleID)) {
                await DisplayAlert("Pomyślnie usunieto", "Ok", "Ok");
            } else {
                await DisplayAlert("Coś poszlo nie tak", "Ok", "Ok");
            }

            EmptyInputs();

            selectedVehicle = null;
            CarList.SelectedItem = null;
        }
        void EmptyInputs() {
            foreach(var item in Formularz.Children.OfType<Entry>()) {
                item.Text = "";
            }

            Silnik.SelectedItem = null;
            TypPojazdu.SelectedItem = null;
        }
    }
}
