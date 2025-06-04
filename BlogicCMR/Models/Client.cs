using System.ComponentModel.DataAnnotations;

namespace BlogicCMR.Models
{
    public class Client
    {
        [Key]
        [Display(Name = "ID klienta")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Jméno je povinné.")]
        [MaxLength(15, ErrorMessage = "Jméno může mít maximálně 15 znaků.")]
        [Display(Name = "Jméno")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Příjmení je povinné.")]
        [MaxLength(15, ErrorMessage = "Příjmení může mít maximálně 15 znaků.")]
        [Display(Name = "Příjmení")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "E-mail je povinný.")]
        [EmailAddress(ErrorMessage = "Neplatný formát e-mailu.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Neplatný formát telefonu.")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Rodné číslo je povinné.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Zadejte přesně 10 číslic bez lomítka.")]
        [Display(Name = "Rodné číslo")]
        public string PersonalIdNumber { get; set; }


        [Range(1, 120, ErrorMessage = "Věk musí být mezi 1 a 120.")]
        [Display(Name = "Věk")]
        public int Age { get; set; }

        [Display(Name = "Smlouvy")]
        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
        
        public string FullName => $"{FirstName} {LastName}";
    }
}