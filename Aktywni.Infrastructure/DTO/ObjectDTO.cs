namespace Aktywni.Infrastructure.DTO
{
    public class ObjectDTO
    {
        public int ObjectId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public int Rating { get; set; }
        public int NumOfRating { get; set; }
        public string GeographicalCoordinates { get; set; }
    }
}