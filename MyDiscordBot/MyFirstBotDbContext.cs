using Microsoft.EntityFrameworkCore;
using MyDiscordBot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDiscordBot
{
    public class MyFirstBotDbContext : DbContext
    {
        public MyFirstBotDbContext(DbContextOptions<MyFirstBotDbContext> options) : base(options)
        {
        }

        public DbSet<WordwolfTheme> WordwolfThemes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordwolfTheme>()
                .ToTable("WORDWOLF_THEMES")
                .HasData(
                    new WordwolfTheme { Id = 1, A = "りんご", B = "みかん" },
                    new WordwolfTheme { Id = 2, A = "たぬき", B = "きつね" }
                );
        }
    }
}
