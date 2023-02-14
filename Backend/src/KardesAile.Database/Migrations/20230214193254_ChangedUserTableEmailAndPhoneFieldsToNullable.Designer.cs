﻿// <auto-generated />
using System;
using KardesAile.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KardesAile.Database.Migrations
{
    [DbContext(typeof(KardesAileDbContext))]
    [Migration("20230214193254_ChangedUserTableEmailAndPhoneFieldsToNullable")]
    partial class ChangedUserTableEmailAndPhoneFieldsToNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("kardesaile")
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("KardesAile.Database.Entities.Child", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("created_by");

                    b.Property<int>("Gender")
                        .HasColumnType("integer")
                        .HasColumnName("gender");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_at");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("modified_by");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_child");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_child_user_id");

                    b.ToTable("child", "kardesaile");
                });

            modelBuilder.Entity("KardesAile.Database.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uuid")
                        .HasColumnName("country_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("created_by");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("StateCode")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("state_code");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_city");

                    b.HasIndex("CountryId")
                        .HasDatabaseName("ix_city_country_id");

                    b.HasIndex("Name")
                        .HasDatabaseName("ix_city_name");

                    b.ToTable("city", "kardesaile");
                });

            modelBuilder.Entity("KardesAile.Database.Entities.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)")
                        .HasColumnName("country_code");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("created_by");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_country");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_country_name");

                    b.ToTable("country", "kardesaile");
                });

            modelBuilder.Entity("KardesAile.Database.Entities.DisasterVictim", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("address");

                    b.Property<bool>("AddressValidated")
                        .HasColumnType("boolean")
                        .HasColumnName("address_validated");

                    b.Property<Guid?>("CityId")
                        .HasColumnType("uuid")
                        .HasColumnName("city_id");

                    b.Property<Guid?>("CountryId")
                        .HasColumnType("uuid")
                        .HasColumnName("country_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("created_by");

                    b.Property<string>("IdentityNumber")
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)")
                        .HasColumnName("identity_number");

                    b.Property<bool>("IdentityNumberValidated")
                        .HasColumnType("boolean")
                        .HasColumnName("identity_number_validated");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_at");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("modified_by");

                    b.Property<string>("TemporaryAddress")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("temporary_address");

                    b.Property<Guid?>("TemporaryCityId")
                        .HasColumnType("uuid")
                        .HasColumnName("temporary_city_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_disaster_victim");

                    b.HasIndex("CityId")
                        .HasDatabaseName("ix_disaster_victim_city_id");

                    b.HasIndex("CountryId")
                        .HasDatabaseName("ix_disaster_victim_country_id");

                    b.HasIndex("TemporaryCityId")
                        .HasDatabaseName("ix_disaster_victim_temporary_city_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_disaster_victim_user_id");

                    b.ToTable("disaster_victim", "kardesaile");
                });

            modelBuilder.Entity("KardesAile.Database.Entities.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean")
                        .HasColumnName("active");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_at");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("modified_by");

                    b.Property<Guid?>("SupporterChildId")
                        .HasColumnType("uuid")
                        .HasColumnName("supporter_child_id");

                    b.Property<Guid>("SupporterId")
                        .HasColumnType("uuid")
                        .HasColumnName("supporter_id");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<Guid?>("VictimChildId")
                        .HasColumnType("uuid")
                        .HasColumnName("victim_child_id");

                    b.Property<Guid>("VictimId")
                        .HasColumnType("uuid")
                        .HasColumnName("victim_id");

                    b.HasKey("Id")
                        .HasName("pk_match");

                    b.HasIndex("SupporterChildId")
                        .IsUnique()
                        .HasDatabaseName("ix_match_supporter_child_id");

                    b.HasIndex("SupporterId")
                        .HasDatabaseName("ix_match_supporter_id");

                    b.HasIndex("VictimChildId")
                        .IsUnique()
                        .HasDatabaseName("ix_match_victim_child_id");

                    b.HasIndex("VictimId")
                        .HasDatabaseName("ix_match_victim_id");

                    b.ToTable("match", "kardesaile");
                });

            modelBuilder.Entity("KardesAile.Database.Entities.Supporter", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("address");

                    b.Property<Guid?>("CityId")
                        .HasColumnType("uuid")
                        .HasColumnName("city_id");

                    b.Property<Guid?>("CountryId")
                        .HasColumnType("uuid")
                        .HasColumnName("country_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_at");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("modified_by");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_supporter");

                    b.HasIndex("CityId")
                        .HasDatabaseName("ix_supporter_city_id");

                    b.HasIndex("CountryId")
                        .HasDatabaseName("ix_supporter_country_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_supporter_user_id");

                    b.ToTable("supporter", "kardesaile");
                });

            modelBuilder.Entity("KardesAile.Database.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("created_by");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailValidated")
                        .HasColumnType("boolean")
                        .HasColumnName("email_validated");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("first_name");

                    b.Property<string>("Hash")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("hash");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("last_name");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_at");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("modified_by");

                    b.Property<string>("Phone")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("phone");

                    b.Property<bool>("PhoneValidated")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_validated");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.Property<string>("Salt")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("salt");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_user_email");

                    b.ToTable("user", "kardesaile");
                });

            modelBuilder.Entity("KardesAile.Database.Entities.Child", b =>
                {
                    b.HasOne("KardesAile.Database.Entities.User", "User")
                        .WithMany("Children")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_child_user_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KardesAile.Database.Entities.City", b =>
                {
                    b.HasOne("KardesAile.Database.Entities.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_city_country_country_id");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("KardesAile.Database.Entities.DisasterVictim", b =>
                {
                    b.HasOne("KardesAile.Database.Entities.City", "City")
                        .WithMany("DisasterVictimsCities")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("fk_disaster_victim_city_city_id");

                    b.HasOne("KardesAile.Database.Entities.Country", "Country")
                        .WithMany("DisasterVictims")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("fk_disaster_victim_country_country_id");

                    b.HasOne("KardesAile.Database.Entities.City", "TemporaryCity")
                        .WithMany("DisasterVictimsTemporaryCities")
                        .HasForeignKey("TemporaryCityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("fk_disaster_victim_city_temporary_city_id");

                    b.HasOne("KardesAile.Database.Entities.User", "User")
                        .WithMany("DisasterVictims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_disaster_victim_user_user_id");

                    b.Navigation("City");

                    b.Navigation("Country");

                    b.Navigation("TemporaryCity");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KardesAile.Database.Entities.Match", b =>
                {
                    b.HasOne("KardesAile.Database.Entities.Child", "SupporterChild")
                        .WithOne("SupporterMatch")
                        .HasForeignKey("KardesAile.Database.Entities.Match", "SupporterChildId")
                        .HasConstraintName("fk_match_child_supporter_child_id");

                    b.HasOne("KardesAile.Database.Entities.Supporter", "Supporter")
                        .WithMany("Matches")
                        .HasForeignKey("SupporterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_match_supporter_supporter_id");

                    b.HasOne("KardesAile.Database.Entities.Child", "VictimChild")
                        .WithOne("VictimMatch")
                        .HasForeignKey("KardesAile.Database.Entities.Match", "VictimChildId")
                        .HasConstraintName("fk_match_child_victim_child_id");

                    b.HasOne("KardesAile.Database.Entities.DisasterVictim", "Victim")
                        .WithMany("Matches")
                        .HasForeignKey("VictimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_match_disaster_victim_victim_id");

                    b.Navigation("Supporter");

                    b.Navigation("SupporterChild");

                    b.Navigation("Victim");

                    b.Navigation("VictimChild");
                });

            modelBuilder.Entity("KardesAile.Database.Entities.Supporter", b =>
                {
                    b.HasOne("KardesAile.Database.Entities.City", "City")
                        .WithMany("Supporters")
                        .HasForeignKey("CityId")
                        .HasConstraintName("fk_supporter_city_city_id");

                    b.HasOne("KardesAile.Database.Entities.Country", "Country")
                        .WithMany("Supporters")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("fk_supporter_country_country_id");

                    b.HasOne("KardesAile.Database.Entities.User", "User")
                        .WithMany("Supporters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_supporter_user_user_id");

                    b.Navigation("City");

                    b.Navigation("Country");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KardesAile.Database.Entities.Child", b =>
                {
                    b.Navigation("SupporterMatch");

                    b.Navigation("VictimMatch");
                });

            modelBuilder.Entity("KardesAile.Database.Entities.City", b =>
                {
                    b.Navigation("DisasterVictimsCities");

                    b.Navigation("DisasterVictimsTemporaryCities");

                    b.Navigation("Supporters");
                });

            modelBuilder.Entity("KardesAile.Database.Entities.Country", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("DisasterVictims");

                    b.Navigation("Supporters");
                });

            modelBuilder.Entity("KardesAile.Database.Entities.DisasterVictim", b =>
                {
                    b.Navigation("Matches");
                });

            modelBuilder.Entity("KardesAile.Database.Entities.Supporter", b =>
                {
                    b.Navigation("Matches");
                });

            modelBuilder.Entity("KardesAile.Database.Entities.User", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("DisasterVictims");

                    b.Navigation("Supporters");
                });
#pragma warning restore 612, 618
        }
    }
}
