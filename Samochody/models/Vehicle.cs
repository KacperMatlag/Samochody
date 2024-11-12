namespace Samochody.models {
    public class Vehicle {
        public int VehicleID { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int GearboxID { get; set; }
        public Gearbox Gearbox { get; set; } = null;
        public double Mileage { get; set; }
        public int EngineID { get; set; }
        public Engine Engine { get; set; } = null;
        public int SeatingCapacity { get; set; }
        public string BodyType { get; set; }
        public string VIN { get; set; }
        public decimal Price { get; set; }

        public int VehicleTypeID { get; set; }
        public VehicleType VehicleType { get; set; } = null;

        public string DisplayText => $"{Model} - {Color} - {BodyType}";
    }
}
