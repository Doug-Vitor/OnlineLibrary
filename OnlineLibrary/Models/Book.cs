using OnlineLibrary.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models
{
    public class Book : Entity
    {
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Campo {0} deve conter entre {2} e {1} caracteres.")]
        [DisplayName("Título")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        [DisplayName("Departamento/Gênero")]
        public Genre Genre { get; set; }

        [StringLength(200, ErrorMessage = "Campo {0} não pode ultrapassar {1} caracteres.")]
        [DisplayName("Resumo")]
        public string Summary { get; set; }

        [Required]
        [Range(20, 1000, ErrorMessage = "{0} deve estar entre R${1} e R${2}")]
        [DisplayName("Preço")]
        public double Price { get; set; }

        public int AuthorId { get; set; }

        [DisplayName("Autor")]
        public virtual Author Author { get; set; }

        public Book()
        {
        }
    }
}