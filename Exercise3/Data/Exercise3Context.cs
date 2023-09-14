using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Exercise3.Models;
using Exercise3.Data.Converters;

namespace Exercise3.Data
{
    public class Exercise3Context : DbContext
    {
        public Exercise3Context (DbContextOptions<Exercise3Context> options)
            : base(options)
        {
        }

        public DbSet<Exercise3.Models.Person>? Person { get; set; }

        public DbSet<Exercise3.Models.Book>? Book { get; set; }

        public DbSet<Exercise3.Models.Bike>? Bike { get; set; }

        public DbSet<Exercise3.Models.BookRental>? BookRental { get; set; }

        public DbSet<Exercise3.Models.BikeRental>? BikeRental { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookRental>()
                .HasOne<Person>()
                .WithMany()
                .HasForeignKey(br => br.PersonId);

            modelBuilder.Entity<BookRental>()
                .HasOne<Book>()
                .WithMany()
                .HasForeignKey(br => br.BookId);

            modelBuilder.Entity<BikeRental>()
                .HasOne<Person>()
                .WithMany()
                .HasForeignKey(br => br.PersonId);

            modelBuilder.Entity<BikeRental>()
                .HasOne<Bike>()
                .WithMany()
                .HasForeignKey(br => br.BikeId);
        }
    }
}
