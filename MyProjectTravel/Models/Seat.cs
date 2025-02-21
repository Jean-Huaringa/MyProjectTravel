using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyProjectTravel.Models
{
    public class Seat
    {
        public int idBus { get; set; }
        
        public int row { get; set; }
        
        public int column { get; set; }
        
        public string? type { get; set; }
        
        public bool? busy { get; set; }
        
        public bool? state { get; set; }
        
        public Bus? bus { get; set; }
        
    }
}
