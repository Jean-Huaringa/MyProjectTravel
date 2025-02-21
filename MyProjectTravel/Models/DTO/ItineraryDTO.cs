using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProjectTravel.Models.DTO
{
    public class ItineraryDTO
    {
        public int idOrigin { get; set; }

        public int idDestination { get; set; }

        public int idWrk { get; set; }

        public int idBus { get; set; }

        public DateTime startDate { get; set; }

        public DateTime arrivalDate { get; set; }

        public decimal price { get; set; }

        public bool availability { get; set; } 
    }
}
