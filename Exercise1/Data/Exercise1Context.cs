using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Exercise1.Models.Library;
using Exercise1.Models.Garden;
using Exercise1.Models.Refrigerator;
using Exercise1.Models.Kitchen;

namespace Exercise1.Data
{
    public class Exercise1Context : DbContext
    {
        public Exercise1Context (DbContextOptions<Exercise1Context> options)
            : base(options)
        {
        }

        public DbSet<Exercise1.Models.Library.Book>? Books { get; set; }

        public DbSet<Exercise1.Models.Library.Author>? Authors { get; set; }

        public DbSet<Exercise1.Models.Library.Genre>? Genres { get; set; }

        public DbSet<Exercise1.Models.Garden.GardenFurniture>? GardenFurniture { get; set; }

        public DbSet<Exercise1.Models.Refrigerator.Vegetable>? Vegetables { get; set; }

        public DbSet<Exercise1.Models.Garden.Tool>? Tools { get; set; }

        public DbSet<Exercise1.Models.Garden.Tree>? Trees { get; set; }

        public DbSet<Exercise1.Models.Kitchen.Knife>? Knives { get; set; }

        public DbSet<Exercise1.Models.Kitchen.Glass>? Glasses { get; set; }

        public DbSet<Exercise1.Models.Kitchen.Plate>? Plates { get; set; }

        public DbSet<Exercise1.Models.Refrigerator.Meat>? Meat { get; set; }

        public DbSet<Exercise1.Models.Refrigerator.Fruit>? Fruits { get; set; }
    }
}
