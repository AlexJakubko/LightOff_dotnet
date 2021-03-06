using System;
using System.Collections.Generic;
using LightsOff.LightsOffCore.Entity;
using LightsOffCore.Entity;
using Microsoft.EntityFrameworkCore;

namespace LightsOffCore.Service
{
    public class LightsOffDbContext : DbContext
    {
        public DbSet<Score> Scores { get; set; }    

        public DbSet<Comment> Comments { get; set;}

        public DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=lightsoff;Trusted_Connection=True;");
        }

    }
}