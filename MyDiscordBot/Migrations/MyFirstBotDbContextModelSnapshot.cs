﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyDiscordBot;

namespace MyDiscordBot.Migrations
{
    [DbContext(typeof(MyFirstBotDbContext))]
    partial class MyFirstBotDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyDiscordBot.Models.WordwolfTheme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("A")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("B")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WORDWOLF_THEMES");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            A = "りんご",
                            B = "みかん"
                        },
                        new
                        {
                            Id = 2,
                            A = "たぬき",
                            B = "きつね"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
