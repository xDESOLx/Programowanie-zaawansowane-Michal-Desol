using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise2.Models.Library;

public class Genre
{
    [Display(Name = "#")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Nazwa gatunku jest wymagana")]
    [Display(Name = "Nazwa gatunku")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Data utworzenia jest wymagana")]
    [Display(Name = "Data utworzenia")]
    [DataType(DataType.Date)]
    public DateTime DateOfCreation { get; set; }
    [Required(ErrorMessage = "Motywy są wymagane")]
    [Display(Name = "Motywy")]
    public string Themes { get; set; }
    [Required(ErrorMessage = "Popularność jest wymagana")]
    [Range(1, 5, ErrorMessage = "Nieprawidłowa popularność")]
    [Column(TypeName = "decimal(6, 2)")]
    [Display(Name = "Popularność")]
    public decimal Popularity { get; set; }
    [Required(ErrorMessage = "Ilość książek jest wymagana")]
    [Range(1, int.MaxValue, ErrorMessage = "Nieprawidłowa ilość książek")]
    [Display(Name = "Ilość książek")]
    public int AmountOfBooksWorldwide { get; set; }
}
