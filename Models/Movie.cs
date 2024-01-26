using System.ComponentModel.DataAnnotations;

namespace Moment_2.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Titeln får inte vara tom")]
        public string FilmTitle { get; set; }

        [Required(ErrorMessage = "Regissören får inte vara tom")]
        public string Director { get; set; }

        // Ny egenskap för genre (dropdownlista)
        [Required(ErrorMessage = "Välj en genre")]
        public string Genre { get; set; }

        // Ny egenskap för betyg (radioknappar)
        [Required(ErrorMessage = "Välj ett betyg")]
        public string Rating { get; set; }

        // Ny egenskap för visningsformat (kryssrutor)
        public bool IsDigital { get; set; }
        public bool IsDVD { get; set; }
        public bool IsBluRay { get; set; }
    }
}
