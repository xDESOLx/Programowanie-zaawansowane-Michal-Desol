using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Exercise3.Data.Converters;
//https://github.com/dotnet/efcore/issues/24507
public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() : base(
        d => d.ToDateTime(TimeOnly.MinValue),
        d => DateOnly.FromDateTime(d))
    { }
}
