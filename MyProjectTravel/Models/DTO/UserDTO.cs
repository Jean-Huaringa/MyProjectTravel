using System.ComponentModel.DataAnnotations;

namespace MyProjectTravel.Models.DTO
{
    public class UserDTO
    {
        public string? userName { get; set; }

        public string? lastname { get; set; }

        public string? phone { get; set; }

        public DateOnly birthdate { get; set; }

        public string? typeDocument { get; set; }

        public string? numDocument { get; set; }

        public string? mail { get; set; }

        public string? password { get; set; }

        public static implicit operator UserDTO(string v)
        {
            throw new NotImplementedException();
        }
    }
}
