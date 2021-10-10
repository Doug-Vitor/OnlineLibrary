using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace OnlineLibrary.Models
{
    public class Book : Entity
    {
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Campo {0} deve conter entre {2} e {1} caracteres.")]
        [DisplayName("Título")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        [DisplayName("Departamento/Gênero")]
        public Genre Genre { get; set; }

        [StringLength(400, ErrorMessage = "Campo {0} não pode ultrapassar {1} caracteres.")]
        [DisplayName("Resumo")]
        public string Summary { get; set; }

        [Required]
        [Range(20, 1000, ErrorMessage = "{0} deve estar entre R${1} e R${2}")]
        [DisplayName("Preço")]
        public double Price { get; set; }
        public string ImagePath { get; set; }

        public int AuthorId { get; set; }

        [DisplayName("Autor")]
        public virtual Author Author { get; set; }

        public Book()
        {
        }

        public Book(string title, Genre genre, string summary, double price, Author author)
        {
            Title = title;
            Genre = genre;
            Summary = summary;
            Price = price;
            Author = author;
        }

        public string ReturnPrice() => Price.ToString("C2", CultureInfo.CurrentCulture);
    }
}