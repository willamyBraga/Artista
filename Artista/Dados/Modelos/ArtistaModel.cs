using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Artista.Data.Model
{
    public class ArtistaModel
    {
        public int ArtistaId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string ArtistaNome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string MusicaArtista { get; set; } 
    }
}
