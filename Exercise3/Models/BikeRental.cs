using Exercise3.Data.Converters;
using System.ComponentModel;

namespace Exercise3.Models;

public class BikeRental
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public int BikeId { get; set; }
    public DateOnly RentalDate { get; set; }
}
