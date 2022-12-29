﻿// <auto-generated />
using System;
using CodingPlatform.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CodingPlatform.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220908124052_RefactorCurrentChallengeAmend")]
    partial class RefactorCurrentChallengeAmend
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CodingPlatform.Domain.Entities.Challenge", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Challenges");
                });

            modelBuilder.Entity("CodingPlatform.Domain.Entities.Submission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("ChallengeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateSubmitted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<byte>("TipsNumber")
                        .HasColumnType("smallint");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeId");

                    b.HasIndex("UserId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("CodingPlatform.Domain.Entities.Tip", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("ChallengeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<byte>("Order")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeId");

                    b.ToTable("Tips");
                });

            modelBuilder.Entity("CodingPlatform.Domain.Entities.Tournament", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("AdminId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ChallengeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MaxParticipants")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("ChallengeId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("CodingPlatform.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CodingPlatform.Domain.Entities.UserTournamentParticipations", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("TournamentId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTournamentParticipations");
                });

            modelBuilder.Entity("CodingPlatform.Domain.Entities.Submission", b =>
                {
                    b.HasOne("CodingPlatform.Domain.Entities.Challenge", "Challenge")
                        .WithMany()
                        .HasForeignKey("ChallengeId");

                    b.HasOne("CodingPlatform.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Challenge");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CodingPlatform.Domain.Entities.Tip", b =>
                {
                    b.HasOne("CodingPlatform.Domain.Entities.Challenge", null)
                        .WithMany("Tips")
                        .HasForeignKey("ChallengeId");
                });

            modelBuilder.Entity("CodingPlatform.Domain.Entities.Tournament", b =>
                {
                    b.HasOne("CodingPlatform.Domain.Entities.User", "Admin")
                        .WithMany("TournamentsAdmin")
                        .HasForeignKey("AdminId");

                    b.HasOne("CodingPlatform.Domain.Entities.Challenge", "Challenge")
                        .WithMany()
                        .HasForeignKey("ChallengeId");

                    b.Navigation("Admin");

                    b.Navigation("Challenge");
                });

            modelBuilder.Entity("CodingPlatform.Domain.Entities.UserTournamentParticipations", b =>
                {
                    b.HasOne("CodingPlatform.Domain.Entities.Tournament", "Tournament")
                        .WithMany("UserTournamentParticipations")
                        .HasForeignKey("TournamentId");

                    b.HasOne("CodingPlatform.Domain.Entities.User", "User")
                        .WithMany("UserTournamentParticipations")
                        .HasForeignKey("UserId");

                    b.Navigation("Tournament");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CodingPlatform.Domain.Entities.Challenge", b =>
                {
                    b.Navigation("Tips");
                });

            modelBuilder.Entity("CodingPlatform.Domain.Entities.Tournament", b =>
                {
                    b.Navigation("UserTournamentParticipations");
                });

            modelBuilder.Entity("CodingPlatform.Domain.Entities.User", b =>
                {
                    b.Navigation("TournamentsAdmin");

                    b.Navigation("UserTournamentParticipations");
                });
#pragma warning restore 612, 618
        }
    }
}
