﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notifications.DataAccess;

namespace Notifications.DataAccess.Migrations
{
    [DbContext(typeof(NotificationsDbContext))]
    partial class NotificationsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Notifications.DataAccess.Entities.NotificationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("EventType")
                        .HasMaxLength(250);

                    b.Property<string>("Message")
                        .HasMaxLength(1000);

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Notification","core");

                    b.HasData(
                        new { Id = new Guid("5f46e0cd-c3ef-4d81-8646-0b612ee93f99"), DateAdded = new DateTime(2020, 1, 17, 22, 53, 55, 287, DateTimeKind.Local), EventType = "AppointmentCancelled", Message = "1 Hi Firstname, your appointment with OrganisationName at AppointmentDateTime has been cancelled for the following reason: Reason.", UserId = new Guid("ae60bbba-ac45-41e8-bb93-2e3db395b114") },
                        new { Id = new Guid("561670a1-76db-4e8e-accf-5a031437f253"), DateAdded = new DateTime(2020, 1, 18, 22, 53, 55, 290, DateTimeKind.Local), EventType = "AppointmentCancelled", Message = "2 Hi Firstname, your appointment with OrganisationName at AppointmentDateTime has been cancelled for the following reason: Reason.", UserId = new Guid("ae60bbba-ac45-41e8-bb93-2e3db395b114") },
                        new { Id = new Guid("3dd4ed21-ce69-457b-885a-bc4fdbe407d6"), DateAdded = new DateTime(2020, 1, 19, 22, 53, 55, 290, DateTimeKind.Local), EventType = "AppointmentCancelled", Message = "3 Hi Firstname, your appointment with OrganisationName at AppointmentDateTime has been cancelled for the following reason: Reason.", UserId = new Guid("ae60bbba-ac45-41e8-bb93-2e3db395b114") }
                    );
                });

            modelBuilder.Entity("Notifications.DataAccess.Entities.TemplateEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body")
                        .HasMaxLength(1000);

                    b.Property<string>("EventType")
                        .HasMaxLength(250);

                    b.Property<string>("Title")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("Template","core");

                    b.HasData(
                        new { Id = new Guid("e0c77fe8-53e3-4473-8dcb-896e340d06a1"), Body = "Hi {Firstname}, your appointment with {OrganisationName} at {AppointmentDateTime} has been cancelled for the following reason: {Reason}.", EventType = "AppointmentCancelled", Title = "Appointment Cancelled" }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}
