using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieRater.Data.Entities;

namespace MovieRater.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<MovieEntity> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieEntity>()
        .HasOne(n => n.Owner)
        .WithMany(p => p.Movies)
        .HasForeignKey(n => n.OwnerId);
    }
        
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : base(options)
    {
        
    }
    
    }

}
