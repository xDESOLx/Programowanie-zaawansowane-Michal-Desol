using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise1.Models.Kitchen;

public class Glass
{
    [Display(Name = "#")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Pojemność jest wymagana")]
    [Range(0.1, int.MaxValue, ErrorMessage = "Nieprawidłowa pojemność")]
    [Column(TypeName = "decimal(6, 2)")]
    [Display(Name = "Pojemność")]
    [DisplayFormat(DataFormatString = "{0}ml")]
    public decimal Diameter { get; set; }
    [Required(ErrorMessage = "Waga jest wymagana")]
    [Range(0.1, int.MaxValue, ErrorMessage = "Nieprawidłowa waga")]
    [Column(TypeName = "decimal(6, 2)")]
    [Display(Name = "Waga")]
    [DisplayFormat(DataFormatString = "{0}g")]
    public decimal Weight { get; set; }
    [Required(ErrorMessage = "Typ jest wymagany")]
    [Display(Name = "Typ")]
    public string Type { get; set; }
    [Required(ErrorMessage = "Kolor jest wymagany")]
    [Display(Name = "Kolor")]
    public string Color { get; set; }
    [Required(ErrorMessage = "Materiał wykonania jest wymagany")]
    [Display(Name = "Materiał wykonania")]
    public string Substance { get; set; }
}
