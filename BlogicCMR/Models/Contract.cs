using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation; // <- přidáno
using System.Collections.Generic;
using System;

namespace BlogicCMR.Models
{
    public class Contract : IValidatableObject
    {
        [Key]
        [Display(Name = "ID smlouvy")]
        public int ContractId { get; set; }

        [Required(ErrorMessage = "Instituce je povinná.")]
        [MaxLength(20, ErrorMessage = "Instituce může mít maximálně 20 znaků.")]
        [Display(Name = "Instituce (ČSOB, AEGON, Axa, atd.)")]
        public string Institution { get; set; }

        // —–––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––
        // Jediný klient
        [Required(ErrorMessage = "Klient je povinný.")]
        [Display(Name = "Klient")]
        public int ClientId { get; set; }

        [ValidateNever]                             // ← toto zabrání MVC validovat celé Client
        [Display(Name = "Klient")]
        public Client Client { get; set; }

        // —–––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––
        // Správce smlouvy (jeden Advisor)
        [Required(ErrorMessage = "Správce smlouvy je povinný.")]
        [ForeignKey(nameof(Manager))]
        [Display(Name = "Správce smlouvy")]
        public int? ManagerId { get; set; }

        [ValidateNever]                             // ← i tady
        [Display(Name = "Správce smlouvy")]
        public Advisor? Manager { get; set; }

        // —–––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––
        // Více účastníků (many-to-many)
        [Display(Name = "Účastníci smlouvy")]
        public ICollection<Advisor> Participants { get; set; } = new List<Advisor>();

        // Pomocné pole pro binding a validaci z Create/Edit formuláře
        [NotMapped]
        [Required(ErrorMessage = "Vyberte alespoň jednoho účastníka.")]
        [MinLength(1, ErrorMessage = "Musí být vybrán minimálně jeden účastník smlouvy.")]
        [Display(Name = "Účastníci smlouvy")]
        public List<int> ParticipantIds { get; set; } = new List<int>();

        // —–––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––
        [Required(ErrorMessage = "Datum uzavření je povinné.")]
        [DataType(DataType.Date)]
        [Display(Name = "Datum uzavření")]
        public DateTime DateSigned { get; set; }

        [Required(ErrorMessage = "Datum začátku platnosti je povinné.")]
        [DataType(DataType.Date)]
        [Display(Name = "Datum začátku platnosti")]
        public DateTime DateValidFrom { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Datum ukončení")]
        public DateTime? DateEnd { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ParticipantIds == null || ParticipantIds.Count < 1)
            {
                yield return new ValidationResult(
                    "Musí být vybrán alespoň jeden účastník smlouvy.",
                    new[] { nameof(ParticipantIds) }
                );
            }
        }
    }
}
