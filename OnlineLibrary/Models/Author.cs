using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models
{
    public class Author : ApplicationUser
    {
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Campo {0} deve conter entre {2} e {1} caracteres.")]
        [DisplayName("Nome completo")] 
        public string FullName { get; set; }

        [Required]
        [DisplayName("Data de nascimento")]
        public DateTime Birthdate { get; set; }

        [StringLength(200, ErrorMessage = "Campo {0} não pode ultrapassar {1} caracteres.")]
        public string ShortBiography { get; set; }

        public virtual List<Book> PublishedBooks { get; set; }

        public Author()
        {
        }

        public Author(IdentityUser identityUser, string fullName, string shortBiography)
            : base(identityUser)
        {
            IdentityUser = identityUser;
            FullName = fullName;
            ShortBiography = shortBiography;
        }
    }
}
