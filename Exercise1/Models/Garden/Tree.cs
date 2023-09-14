using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise1.Models.Garden;

public class Tree
{
    [Display(Name = "#")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Wysokość jest wymagana")]
    [Range(0.1, int.MaxValue, ErrorMessage = "Nieprawidłowa wysokość")]
    [Column(TypeName = "decimal(6, 2)")]
    [Display(Name = "Wysokość")]
    [DisplayFormat(DataFormatString = "{0}m")]
    public decimal Height { get; set; }
    [Required(ErrorMessage = "Rodzaj drzewa jest wymagany")]
    [Display(Name = "Rodzaj drzewa")]
    public string Type { get; set; }
    [Required(ErrorMessage = "Data zasadzenia jest wymagana")]
    [Display(Name = "Data zasadzenia")]
    [DataType(DataType.Date)]
    public DateTime PlantingDate { get; set; }
    [Required(ErrorMessage = "Średnica pnia jest wymagana")]
    [Range(0.1, int.MaxValue, ErrorMessage = "Nieprawidłowa średnica pnia")]
    [Column(TypeName = "decimal(6, 2)")]
    [Display(Name = "Średnica pnia")]
    [DisplayFormat(DataFormatString = "{0}cm")]
    public decimal TrunkDiameter { get; set; }
    [Required(ErrorMessage = "Kolor liści jest wymagany")]
    [Display(Name = "Kolor liści")]
    public string LeafColor { get; set; }
}
