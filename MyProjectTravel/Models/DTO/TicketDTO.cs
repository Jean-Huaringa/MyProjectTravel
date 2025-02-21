using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyProjectTravel.Models.DTO
{
    public class TicketDTO
    {
        public int idUsr { get; set; }

        public int idWrk { get; set; }

        public int idItn { get; set; }

        public int idBus { get; set; }

        public int row { get; set; }

        public int column { get; set; }

        public string paymentMethod { get; set; } = null!;

        public decimal amount { get; set; }

        public string? userName { get; set; }

        public string? lastname { get; set; }

        public string? age { get; set; }

        public string? typeDocument { get; set; }

        public string? numDocument { get; set; }

        public DateTime creationDate { get; set; }
    }
}
