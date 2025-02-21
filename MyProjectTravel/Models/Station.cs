using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProjectTravel.Models
{
    public class Station
    {
        public int idStn { get; set; }

        public string? city { get; set; }

        public string? street { get; set; }

        public string? pseudonym { get; set; }

        public bool state { get; set; }

        public ICollection<Itinerary> origins { get; set; } = new List<Itinerary>();

        public ICollection<Itinerary> destinations { get; set; } = new List<Itinerary>();
    }
}
