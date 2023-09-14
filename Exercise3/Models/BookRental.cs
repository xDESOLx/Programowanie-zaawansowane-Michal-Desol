using Exercise3.Data.Converters;
using System.ComponentModel;

namespace Exercise3.Models;

public class BookRental
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public int BookId { get; set; }
    public DateOnly RentalDate { get; set; }
}
