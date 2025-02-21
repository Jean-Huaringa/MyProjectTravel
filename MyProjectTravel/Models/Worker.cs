using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProjectTravel.Models
{
    public class Worker
    {
        public int idWrk { get; set; }

        public decimal salary { get; set; }

        public string? role { get; set; }

        public DateTime? registrationDate { get; set; }

        public bool availability { get; set; }

        public bool state { get; set; }

        public User? user { get; set; }

        public ICollection<Itinerary> itineraries { get; set; } = new List<Itinerary>();
    }
}
