namespace TestMauiMap.Models
{
    public class RequestModel
    {
        public double Easting { get; set; }
        public double Northing { get; set; }
        public List<string> Layers { get; set; } = new List<string>();
        public bool CheckPrivateLand { get; set; }
        public bool CheckSchoolLand { get; set; }
        public bool CheckHousingLand { get; set; }
        public bool CheckHighspeedRoutes { get; set; }
        public bool CheckParksLand { get; set; }
        public bool CheckAdoptedHighway { get; set; }
        public bool CheckM4 { get; set; }
        public bool CheckTrafficOrder { get; set; }
        public bool CheckOutsideCardiff { get; set; }
        public List<string> PrivateExceptions { get; set; }
    }
}
