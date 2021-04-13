using System.ComponentModel.DataAnnotations;

namespace dotnetcore_api.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        [MaxLength(50,ErrorMessage ="Esse campo deve conter até 50 caracteres")]
        public string Nome { get; set; }
    }

    public class UserProfile
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
