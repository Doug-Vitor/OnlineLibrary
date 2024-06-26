﻿using Microsoft.AspNetCore.Identity;
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

        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        [DisplayName("Data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [DisplayName("Sua biografia")]
        [StringLength(400, ErrorMessage = "Campo {0} não pode ultrapassar {1} caracteres.")]
        public string ShortBiography { get; set; }

        public string ImagePath { get; set; }
        public virtual List<Book> PublishedBooks { get; set; }

        public Author()
        {
        }

        public Author(string fullName, DateTime birthdate, string shortBiography)
        {
            FullName = fullName;
            Birthdate = birthdate;
            ShortBiography = shortBiography;
        }

        public Author(IdentityUser identityUser, string fullName, string shortBiography)
            : base(identityUser)
        {
            IdentityUser = identityUser;
            FullName = fullName;
            ShortBiography = shortBiography;
        }

        public void ChangeToAuthor(ApplicationUser user)
        {
            Id = user.Id;
            IdentityUser = user.IdentityUser;
            ImagePath = "~/Images/ProfilePhotos/Default.png";
            ShoppingCart = user.ShoppingCart;
            Purchases = user.Purchases;
        }
    }
}
