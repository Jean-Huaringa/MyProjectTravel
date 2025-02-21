using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyProjectTravel.Models
{
    public class Itinerary
    {
        public int idItn { get; set; }
        
        public int idOrigin { get; set; }
        
        public int idDestination { get; set; }
        
        public int idWrk { get; set; }
        
        public int idBus { get; set; }
        
        public DateTime startDate { get; set; }
        
        public DateTime arrivalDate { get; set; }
        
        public decimal price { get; set; }
        
        public bool availability { get; set; }
        
        public bool state { get; set; }
        
        public Bus bus { get; set; }
        
        public Station destination { get; set; }
        
        public Station origin { get; set; }
        
        public Worker worker { get; set; }
    }
}
