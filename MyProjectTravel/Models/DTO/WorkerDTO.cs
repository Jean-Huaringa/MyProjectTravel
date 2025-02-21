using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyProjectTravel.Models.DTO
{
    public class WorkerDTO
    {
        public int idWrk { get; set; }

        public decimal salary { get; set; }

        public string? role { get; set; }
    }
}
