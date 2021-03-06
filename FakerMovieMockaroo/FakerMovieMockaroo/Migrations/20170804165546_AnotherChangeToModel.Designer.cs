﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FakerMovieMockaroo.Models;

namespace FakerMovieMockaroo.Migrations
{
    [DbContext(typeof(FakerMovieMockarooContext))]
    [Migration("20170804165546_AnotherChangeToModel")]
    partial class AnotherChangeToModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FakerMovieMockaroo.Models.Movie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CurrencyName");

                    b.Property<string>("Genre");

                    b.Property<double>("Price");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("Movie");
                });
        }
    }
}
