using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise1.Models.Refrigerator;

public class Vegetable
{
    [Display(Name = "#")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Data przydatności do spożycia jest wymagana")]
    [Display(Name = "Data przydatności do spożycia")]
    [DataType(DataType.Date)]
    public DateTime ExpiryDate { get; set; }
    [Required(ErrorMessage = "Rodzaj jest wymagany")]
    [Display(Name = "Rodzaj")]
    public string Type { get; set; }
    [Required(ErrorMessage = "Waga jest wymagana")]
    [Range(0.1, int.MaxValue, ErrorMessage = "Nieprawidłowa waga")]
    [Column(TypeName = "decimal(6, 2)")]
    [Display(Name = "Waga")]
    [DisplayFormat(DataFormatString = "{0}g")]
    public decimal Weight { get; set; }
    [Required(ErrorMessage = "Zawartość cukru jest wymagana")]
    [Range(0.1, int.MaxValue, ErrorMessage = "Nieprawidłowa zawartośc cukru")]
    [Column(TypeName = "decimal(6, 2)")]
    [Display(Name = "Zawartość cukru")]
    [DisplayFormat(DataFormatString = "{0}g/100g")]
    public decimal SugarContent { get; set; }
    [Required(ErrorMessage = "Cena jest wymagana")]
    [Range(1, int.MaxValue, ErrorMessage = "Nieprawidłowa cena")]
    [Column(TypeName = "decimal(6, 2)")]
    [Display(Name = "Cena")]
    [DataType(DataType.Currency)]
    public decimal ListPrice { get; set; }
}
