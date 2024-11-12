namespace Samochody.models {
    public class Engine {
        public int EngineID { get; set; }
        public double Capacity { get; set; }
        public int Horsepower { get; set; }
        public int Torque { get; set; }
        public int Cylinders { get; set; }
        public int FuelTypeID { get; set; }
        public FuelType FuelType { get; set; }
        public string DisplayName => $"{Horsepower}Hp - {Capacity}L - {FuelType.Name}";
    }
}
