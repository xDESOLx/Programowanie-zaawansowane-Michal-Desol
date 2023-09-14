using System;
using System.Collections.Generic;
using Exercise6;
using Exercise6.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise6.Data;

public partial class Exercise6Context : DbContext
{
    public Exercise6Context()
    {
    }

    public Exercise6Context(DbContextOptions<Exercise6Context> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<StudentAssignment> StudentAssignments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasMany(b => b.Courses)
            .WithMany(b => b.Students)
            .UsingEntity<StudentAssignment>(e =>
            {
                e.HasIndex(b => new { b.StudentId, b.CourseId });
            });

        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}
