using System.ComponentModel.DataAnnotations;

namespace Exercise2.Models.Library;

public class Author
{
    [Display(Name = "#")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Imię jest wymagane")]
    [Display(Name = "Imię")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Nazwisko jest wymagane")]
    [Display(Name = "Nazwisko")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "Data urodzenia jest wymagana")]
    [Display(Name = "Data urodzenia")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
    [Required(ErrorMessage = "Kraj pochodzenia jest wymagany")]
    [Display(Name = "Kraj pochodzenia")]
    public string CountryOfOrigin { get; set; }
    [Required(ErrorMessage = "Ilość wydanych książek jest wymagana")]
    [Range(1, int.MaxValue, ErrorMessage = "Nieprawidłowa ilość wydanych książek")]
    [Display(Name = "Ilość wydanych książek")]
    public int PublishedBooksAmount { get; set; }
}
