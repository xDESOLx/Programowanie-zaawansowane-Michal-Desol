using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise2.Models.Kitchen;

public class Knife
{
    [Display(Name = "#")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Długość ostrza jest wymagana")]
    [Range(0.1, int.MaxValue, ErrorMessage = "Nieprawidłowa długość ostrza")]
    [Column(TypeName = "decimal(6, 2)")]
    [Display(Name = "Długość ostrza")]
    [DisplayFormat(DataFormatString = "{0}cm")]
    public decimal BladeLength { get; set; }
    [Required(ErrorMessage = "Waga jest wymagana")]
    [Range(0.1, int.MaxValue, ErrorMessage = "Nieprawidłowa waga")]
    [Column(TypeName = "decimal(6, 2)")]
    [Display(Name = "Waga")]
    [DisplayFormat(DataFormatString = "{0}g")]
    public decimal Weight { get; set; }
    [Required(ErrorMessage = "Typ ostrza jest wymagany")]
    [Display(Name = "Typ ostrza")]
    public string BladeType { get; set; }
    [Required(ErrorMessage = "Materiał rękojeści jest wymagany")]
    [Display(Name = "Materiał rękojeści")]
    public string HandleSubstance { get; set; }
    [Required(ErrorMessage = "Stopień ostrości jest wymagany")]
    [Range(0.1, int.MaxValue, ErrorMessage = "Nieprawidłowy stopień ostrości")]
    [Column(TypeName = "decimal(6, 2)")]
    [Display(Name = "Stopień ostrości")]
    [DisplayFormat(DataFormatString = "{0} stopni HRC")]
    public decimal HRC { get; set; }
}
