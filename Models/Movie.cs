using System.ComponentModel.DataAnnotations;

namespace Moment_2.Models
{
    public class Movie
    {
        //Validering för fälten
        [Required(ErrorMessage = "Titeln får inte vara tom")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Regissören får inte vara tom")]
        public string Director { get; set; }
    }
}
