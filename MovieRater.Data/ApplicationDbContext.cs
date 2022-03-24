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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {
        }

        public DbSet<ShowEntity> Users { get; set; }
        public DbSet<ShowEntity> Shows { get; set; }
        public DbSet<ShowEntity> Movies { get; set; }
        public DbSet<ShowEntity> Ratings { get; set; }
    }
}