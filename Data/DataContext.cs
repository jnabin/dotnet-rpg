using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Migrations;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill{Id = 1, Damage = 20, Name = "Skill 1"},
                new Skill{Id = 2, Damage = 60, Name = "Skill 2"},
                new Skill{Id = 3, Damage = 30, Name = "Skill 3"}
            );

            modelBuilder.Entity<User>()
                .Property(user => user.Role).HasDefaultValue("Player");

            Utility.CreatePasswordHash("1234", out byte[] passwordSalt, out byte[] passwordHash);
            modelBuilder.Entity<User>().HasData(
                new User{Id = 12, UserName = "AdminUser", PasswordHash = passwordHash, PasswordSalt = passwordSalt, Role = "Admin"}
            );
        }

        public DbSet<Character> Characters => Set<Character>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Weapon> Weapons => Set<Weapon>();
        public DbSet<Skill> Skills => Set<Skill>();
    }
}