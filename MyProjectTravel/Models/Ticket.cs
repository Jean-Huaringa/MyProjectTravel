using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyProjectTravel.Models
{
    public class Ticket
    {
        public int idTck { get; set; }
        
        public int idUsr { get; set; }
        
        public int idWrk { get; set; }
        
        public int idItn { get; set; }
        
        public int idBus { get; set; }
        
        public int row { get; set; }
        
        public int column { get; set; }
        
        public string? paymentMethod { get; set; }
        
        public decimal amount { get; set; }
        
        public string? userName { get; set; }
        
        public string? lastname { get; set; }
        
        public string? age { get; set; }
        
        public string? typeDocument { get; set; }
        
        public string? numDocument { get; set; }
        
        public bool state { get; set; }
        
        public bool? problem { get; set; }
        
        public string? problemDescription { get; set; }
        
        public DateTime creationDate { get; set; }
        
        public User? user { get; set; }
        
        public Worker? worker { get; set; }
        
        public Itinerary? itinerary { get; set; }
        
        public Bus? bus { get; set; }
        
    }
}
