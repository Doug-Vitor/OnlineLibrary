using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models.Enums
{
    public enum Department
    {
        [Display(Name = "Artes")]
        Arts,

        [Display(Name = "Biografias")]
        Biographies,

        [Display(Name = "Negócios")]
        Bussiness,

        [Display(Name = "Computação & Tecnologia")]
        ComputersAndTechnology,

        [Display(Name = "HQ's & Mangás")]
        ComicsAndGraphicNovels,

        [Display(Name = "Artesanato, Casa & Estilo de Vida")]
        CraftsHobbiesAndHome,

        [Display(Name = "Culinária")]
        Culinary,

        [Display(Name = "Drama")]
        Drama,

        [Display(Name = "Engenharia & Transportes")]
        EngineeringAndTransportation,

        [Display(Name = "Educação")]
        Education,

        [Display(Name = "Infantil")]
        ForChildren,

        [Display(Name = "Saúde")]
        Health,

        [Display(Name = "História")]
        History,

        [Display(Name = "Humor & Entretenimento")]
        HumorAndEntertainment,

        [Display(Name = "Medicina")]
        Medical,

        [Display(Name = "Policial, Suspense & Mistério")]
        MysteryThrillerAndSuspense,

        [Display(Name = "Outros")]
        Others,

        [Display(Name = "Relacionamentos")]
        Relationships,

        [Display(Name = "Religião")]
        Religion,

        [Display(Name = "Romance")]
        Romance,

        [Display(Name = "Ficção científica")]
        SciFi,

        [Display(Name = "Autoajuda")]
        SelfHelp,

        [Display(Name = "Ciência")]
        Science,

        [Display(Name = "Esportes")]
        Sports,

        [Display(Name = "Política & Ciências sociais")]
        PoliticsAndSocialScience,

        [Display(Name = "Turismo & Viagem")]
        Travel,
    }
}
