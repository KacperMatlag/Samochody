using Samochody.dtos;
using Samochody.models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Samochody.pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditPage :ContentPage {
        Vehicle vehicle=null;
        public AddEditPage() {
            InitializeComponent();
            Title = "Dodaj pojazd";
            Wykonaj.Text = "Dodaj";
            Usun.IsVisible = false;
        }
        public AddEditPage(Vehicle selectedVehicle)
        {
            InitializeComponent();
            vehicle = selectedVehicle;
            Model.Text = selectedVehicle.Model;
            Kolor.Text = selectedVehicle.Color;
            Rok.Text = selectedVehicle.Year.ToString();
            Przebieg.Text = selectedVehicle.Mileage.ToString();
            IloscMiejs.Text = selectedVehicle.SeatingCapacity.ToString();
            Nadwozie.Text = selectedVehicle.BodyType.ToString();
            Cena.Text = selectedVehicle.Price.ToString();


            Title = "Edytuj";
            Wykonaj.Text = "Edytuj";
            Wykonaj.Background = new SolidColorBrush(System.Drawing.Color.Blue);
            Wykonaj.Clicked -= Dodaj_Clicked;
            Wykonaj.Clicked += Edit_Clicked;

        }
        protected override async void OnAppearing() {
            base.OnAppearing();
            Silnik.ItemsSource = await App.Service.Engine.GetAllEnginesAsync();
            Silnik.ItemDisplayBinding = new Binding("DisplayEngineInfo");

            TypPojazdu.ItemsSource = await App.Service.VehicleTypeService.GetAllVehicleTypesAsync();
            TypPojazdu.ItemDisplayBinding = new Binding("Name");

            SkrzyniaBiegow.ItemsSource=await App.Service.GearboxService.GetAllGearboxesAsync();
            SkrzyniaBiegow.ItemDisplayBinding = new Binding("DisplayGearboxInfo");

            if(vehicle!=null) {
                Silnik.SelectedItem = Silnik.ItemsSource.Cast<Engine>().FirstOrDefault(engine => engine.EngineID == vehicle.EngineID);
                TypPojazdu.SelectedItem = TypPojazdu.ItemsSource.Cast<VehicleType>().FirstOrDefault(vt => vt.VehicleTypeID == vehicle.VehicleTypeID);
                SkrzyniaBiegow.SelectedItem = SkrzyniaBiegow.ItemsSource.Cast<Gearbox>().FirstOrDefault(g => g.GearboxID == vehicle.GearboxID);
            }
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
                GearboxID = (SkrzyniaBiegow.SelectedItem as Gearbox).GearboxID,
                VehicleTypeID = (TypPojazdu.SelectedItem as VehicleType).VehicleTypeID,
            };

            if(await App.Service.VehicleService.CreateVehicleAsync(vehicleDto) != null) {
                await DisplayAlert("Dodawanie pojazdu", "Pomyślnie dodano pojazd.", "OK");
                await Navigation.PopAsync();
            } else {
                await DisplayAlert("Dodawanie pojazdu","Coś poszło nie tak podczas usuwania pojazdu.","OK");
            }
        }
        private async void Usun_Clicked(object sender, EventArgs e) {
            if(vehicle == null)
                return;

            if(await App.Service.VehicleService.DeleteVehicleAsync(vehicle.VehicleID)) {
                await DisplayAlert("Usuwanie pojazdu", "Pojazd pomyślnie usunięty.", "Ok");
                await Navigation.PopAsync();
            } else {
                await DisplayAlert("Usuwanie pojazdu", "Coś poszło nie tak podczas usuwania.", "Ok");
            }
        }
        private async void Edit_Clicked(object sender, EventArgs e) {
            if(vehicle == null)
                return;

            VehicleDto dto = new VehicleDto {
                Model = Model.Text,
                Year = int.Parse(Rok.Text),
                Color = Kolor.Text,
                Mileage = double.Parse(Przebieg.Text),
                SeatingCapacity = int.Parse(IloscMiejs.Text),
                BodyType = Nadwozie.Text,
                Price = decimal.Parse(Cena.Text),
                EngineID = (Silnik.SelectedItem as Engine).EngineID,
                GearboxID = (SkrzyniaBiegow.SelectedItem as Gearbox).GearboxID,
                VehicleTypeID = (TypPojazdu.SelectedItem as VehicleType).VehicleTypeID
            };


            if(await App.Service.VehicleService.UpdateVehicleAsync(vehicle.VehicleID, dto)!=null) {
                await DisplayAlert("Edycja pojazdu", "Pojazd pomyślnie uaktualniony.", "Ok");
                await Navigation.PopAsync();
            } else {
                await DisplayAlert("Edycja pojazdu", "Błąd podczas edycji pojazdu.", "Ok");
            }
        }
    }
}