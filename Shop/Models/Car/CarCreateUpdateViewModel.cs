namespace Shop.Models.Car
{
    public class CarCreateUpdateViewModel
    {
        public Guid? Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string NameOfOwner { get; set; }
        public int Price { get; set; }
        public int Weight { get; set; }
        public int EnginePower { get; set; }
        public int Mileage { get; set; }
        public DateTime BuiltDate { get; set; }
        public DateTime MaintanceDate { get; set; }
		public List<IFormFile> Files { get; set; }
		public List<ImageViewModel> Image { get; set; } = new List<ImageViewModel>();
	}
}
