using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Exercise2.Models.Library;
using Exercise2.Models.Garden;
using Exercise2.Models.Refrigerator;
using Exercise2.Models.Kitchen;

namespace Exercise2.Data
{
    public class Exercise2Context : DbContext
    {
        public Exercise2Context (DbContextOptions<Exercise2Context> options)
            : base(options)
        {
        }

        public DbSet<Exercise2.Models.Library.Book>? Books { get; set; }

        public DbSet<Exercise2.Models.Library.Author>? Authors { get; set; }

        public DbSet<Exercise2.Models.Library.Genre>? Genres { get; set; }

        public DbSet<Exercise2.Models.Garden.GardenFurniture>? GardenFurniture { get; set; }

        public DbSet<Exercise2.Models.Refrigerator.Vegetable>? Vegetables { get; set; }

        public DbSet<Exercise2.Models.Garden.Tool>? Tools { get; set; }

        public DbSet<Exercise2.Models.Garden.Tree>? Trees { get; set; }

        public DbSet<Exercise2.Models.Kitchen.Knife>? Knives { get; set; }

        public DbSet<Exercise2.Models.Kitchen.Glass>? Glasses { get; set; }

        public DbSet<Exercise2.Models.Kitchen.Plate>? Plates { get; set; }

        public DbSet<Exercise2.Models.Refrigerator.Meat>? Meat { get; set; }

        public DbSet<Exercise2.Models.Refrigerator.Fruit>? Fruits { get; set; }
    }
}
