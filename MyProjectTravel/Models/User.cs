using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProjectTravel.Models
{
    public class User
    {
        public int idUsr { get; set; }

        public string? userName { get; set; }

        public string? lastname { get; set; }

        public string? phone { get; set; }

        public DateOnly birthdate { get; set; }

        public string? typeDocument { get; set; }

        public string? numDocument { get; set; }

        public string? mail { get; set; }

        public string? password { get; set; }

        public bool? state { get; set; }

        public bool? ban { get; set; }

        public DateTime registrationDate { get; set; }

        public ICollection<Ticket> tickets { get; set; } = new List<Ticket>();

        public Worker? worker { get; set; }
    }
}
