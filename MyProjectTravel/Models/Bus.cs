using System.Text.Json.Serialization;

namespace MyProjectTravel.Models
{
    public class Bus
    {
        [JsonPropertyName("idBus")]
        public int idBus { get; set; }

        [JsonPropertyName("placa")]
        public string? placa { get; set; }

        [JsonPropertyName("model")]
        public string? model { get; set; }

        [JsonPropertyName("numColumns")]
        public int numColumns { get; set; }

        [JsonPropertyName("numRows")]
        public int numRows { get; set; }

        [JsonPropertyName("availability")]
        public bool availability { get; set; }

        [JsonPropertyName("state")]
        public bool state { get; set; }

        public ICollection<Seat> seating { get; set; } = new List<Seat>();

        public ICollection<Itinerary> itineraries { get; set; } = new List<Itinerary>();
    }
}
