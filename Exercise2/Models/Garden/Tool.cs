using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise2.Models.Garden;

public class Tool
{
    [Display(Name = "#")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Typ jest wymagany")]
    [Display(Name = "Typ")]
    public string Type { get; set; }
    [Required(ErrorMessage = "Materiał wykonania jest wymagany")]
    [Display(Name = "Materiał wykonania")]
    public string Substance { get; set; }
    [Required(ErrorMessage = "Waga jest wymagana")]
    [Range(0.1, int.MaxValue, ErrorMessage = "Nieprawidłowa waga")]
    [Column(TypeName = "decimal(6, 2)")]
    [Display(Name = "Waga")]
    [DisplayFormat(DataFormatString = "{0}kg")]
    public decimal Weight { get; set; }
    [Required(ErrorMessage = "Długość jest wymagana")]
    [Range(0.1, int.MaxValue, ErrorMessage = "Nieprawidłowa długość")]
    [Column(TypeName = "decimal(6, 2)")]
    [Display(Name = "Długość")]
    [DisplayFormat(DataFormatString = "{0}cm")]
    public decimal Length { get; set; }
    [Required(ErrorMessage = "Cena jest wymagana")]
    [Range(1, int.MaxValue, ErrorMessage = "Nieprawidłowa cena")]
    [Column(TypeName = "decimal(6, 2)")]
    [Display(Name = "Cena")]
    [DataType(DataType.Currency)]
    public decimal ListPrice { get; set; }
}
