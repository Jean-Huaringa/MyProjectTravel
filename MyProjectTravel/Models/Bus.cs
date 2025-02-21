using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProjectTravel.Models
{
    public class Bus
    {
        public int idBus { get; set; }

        public string? placa { get; set; }

        public string? model { get; set; }

        public int numColumns { get; set; }

        public int numRows { get; set; }

        public bool availability { get; set; }

        public bool state { get; set; }

        public ICollection<Seat> seating { get; set; } = new List<Seat>();

        public ICollection<Itinerary> itineraries { get; set; } = new List<Itinerary>();
    }
}
