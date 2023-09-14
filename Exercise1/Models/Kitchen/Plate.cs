using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise1.Models.Kitchen;

public class Plate
{
    [Display(Name = "#")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Średnica jest wymagana")]
    [Range(0.1, int.MaxValue, ErrorMessage = "Nieprawidłowa średnica")]
    [Column(TypeName = "decimal(6, 2)")]
    [Display(Name = "Średnica")]
    [DisplayFormat(DataFormatString = "{0}cm")]
    public decimal Diameter { get; set; }
    [Required(ErrorMessage = "Waga jest wymagana")]
    [Range(0.1, int.MaxValue, ErrorMessage = "Nieprawidłowa waga")]
    [Column(TypeName = "decimal(6, 2)")]
    [Display(Name = "Waga")]
    [DisplayFormat(DataFormatString = "{0}g")]
    public decimal Weight { get; set; }
    [Required(ErrorMessage = "Kolor jest wymagany")]
    [Display(Name = "Kolor")]
    public string Color { get; set; }
    [Required(ErrorMessage = "Materiał wykonania jest wymagany")]
    [Display(Name = "Materiał wykonania")]
    public string Substance { get; set; }
    [Required(ErrorMessage = "Wysokość jest wymagana")]
    [Range(0.1, int.MaxValue, ErrorMessage = "Nieprawidłowa wysokość")]
    [Column(TypeName = "decimal(6, 2)")]
    [Display(Name = "Wysokość")]
    [DisplayFormat(DataFormatString = "{0}cm")]
    public decimal Height { get; set; }
}
