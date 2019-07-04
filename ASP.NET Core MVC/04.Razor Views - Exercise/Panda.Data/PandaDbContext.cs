using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Panda.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Data
{
    public class PandaDbContext : IdentityDbContext<PandaUser>
    {
        // public DbSet<PandaUser> Users { get; set; }

        public PandaDbContext(DbContextOptions<PandaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PandaUser>().ToTable("User");
        }
    }
}
