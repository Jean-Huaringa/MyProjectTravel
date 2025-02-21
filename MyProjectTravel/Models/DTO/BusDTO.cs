using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyProjectTravel.Models.DTO
{
    public class BusDTO
    {
        public string? placa { get; set; }

        public string? model { get; set; }

        public int numColumns { get; set; }

        public int numRows { get; set; }
    }
}
