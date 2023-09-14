using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise3.Models
{
    public class Bike
    {
        public int Id { get; set; }
        public BikeType Type { get; set; }
        [Column(TypeName = "decimal(4, 2)")]
        [Range(0, 99.99)]
        public decimal FrameSize { get; set; }
        [Column(TypeName = "decimal(4, 2)")]
        [Range(0, 99.99)]
        public decimal WheelSize { get; set; }
        [Required]
        public string Color { get; set; }
    }

    public enum BikeType
    {
        CityBike,
        TrekkingBike,
        MountainBike,
        RoadBike,
        GravelBike,
        CrossBike
    }
}
