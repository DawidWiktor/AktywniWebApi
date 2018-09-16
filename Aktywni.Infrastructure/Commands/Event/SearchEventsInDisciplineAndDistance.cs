namespace Aktywni.Infrastructure.Commands.Event
{
    public class SearchEventsInDisciplineAndDistance
    {
        public string Name { get; set; }
        public int DisciplineId { get; set; }
        public double Distance { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}