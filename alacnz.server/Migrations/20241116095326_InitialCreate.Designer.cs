﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using alacnz.server.Data;

#nullable disable

namespace alacnz.server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241116095326_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("alacnz.server.Models.Alert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CasoId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaAlerta")
                        .HasColumnType("datetime2");

                    b.Property<string>("TipoAlerta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CasoId");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("alacnz.server.Models.Beneficiary", b =>
                {
                    b.Property<int>("BeneficiaryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BeneficiaryId"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BeneficiaryId");

                    b.HasIndex("CaseId");

                    b.ToTable("Beneficiaries");
                });

            modelBuilder.Entity("alacnz.server.Models.Case", b =>
                {
                    b.Property<int>("CaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CaseId"));

                    b.Property<string>("ActivationFeasibility")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ClosureDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReferralReason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReferredBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("int");

                    b.Property<string>("Services")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SocialWorkTeamId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CaseId");

                    b.HasIndex("ClientId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("SocialWorkTeamId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("alacnz.server.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientId"));

                    b.Property<string>("BeneficiariesNationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImmigrationStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialSecurityBenefit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("alacnz.server.Models.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("alacnz.server.Models.Session", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SessionId"));

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.Property<string>("Outcome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SessionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SessionId");

                    b.HasIndex("CaseId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("alacnz.server.Models.SocialWorkTeam", b =>
                {
                    b.Property<int>("SocialWorkTeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SocialWorkTeamId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specialty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SocialWorkTeamId");

                    b.ToTable("SocialWorkTeams");
                });

            modelBuilder.Entity("alacnz.server.Models.Alert", b =>
                {
                    b.HasOne("alacnz.server.Models.Case", "Caso")
                        .WithMany()
                        .HasForeignKey("CasoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Caso");
                });

            modelBuilder.Entity("alacnz.server.Models.Beneficiary", b =>
                {
                    b.HasOne("alacnz.server.Models.Case", "Case")
                        .WithMany("Beneficiaries")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Case");
                });

            modelBuilder.Entity("alacnz.server.Models.Case", b =>
                {
                    b.HasOne("alacnz.server.Models.Client", "Client")
                        .WithMany("Cases")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("alacnz.server.Models.Service", "Service")
                        .WithMany("Cases")
                        .HasForeignKey("ServiceId");

                    b.HasOne("alacnz.server.Models.SocialWorkTeam", "SocialWorkTeam")
                        .WithMany("Cases")
                        .HasForeignKey("SocialWorkTeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Client");

                    b.Navigation("Service");

                    b.Navigation("SocialWorkTeam");
                });

            modelBuilder.Entity("alacnz.server.Models.Session", b =>
                {
                    b.HasOne("alacnz.server.Models.Case", "Case")
                        .WithMany("Sessions")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Case");
                });

            modelBuilder.Entity("alacnz.server.Models.Case", b =>
                {
                    b.Navigation("Beneficiaries");

                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("alacnz.server.Models.Client", b =>
                {
                    b.Navigation("Cases");
                });

            modelBuilder.Entity("alacnz.server.Models.Service", b =>
                {
                    b.Navigation("Cases");
                });

            modelBuilder.Entity("alacnz.server.Models.SocialWorkTeam", b =>
                {
                    b.Navigation("Cases");
                });
#pragma warning restore 612, 618
        }
    }
}
