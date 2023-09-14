using System.ComponentModel.DataAnnotations;

namespace Exercise3.Models;

public class Person
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Range(0, int.MaxValue)]
    public int Age { get; set; }
    [Required]
    [Phone]
    public string Phone { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
