using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyProjectTravel.Models.DTO
{
    public class StationDTO
    {
        public string? city { get; set; }

        public string? street { get; set; }

        public string? pseudonym { get; set; }
    }
}
