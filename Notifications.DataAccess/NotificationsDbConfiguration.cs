using Microsoft.EntityFrameworkCore;
using Notifications.DataAccess.Entities;

namespace Notifications.DataAccess
{
    public static class NotificationsDbConfiguration
    {
        const int maxLongTxtLenght = 1000;
        const int maxShortTxtLenght = 250;
        const string dbSchema = "core";


        public static void ApplyAllConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotificationEntity>()
                .ToTable("Notification", "core")
                .HasKey(x => x.Id);
            modelBuilder.Entity<NotificationEntity>(
           entity =>
           {
               entity.Property(x => x.EventType).HasMaxLength(maxShortTxtLenght);
               entity.Property(x => x.Message).HasMaxLength(maxLongTxtLenght);

               entity.ToTable("Notification", dbSchema);
               entity.HasKey(x => x.Id);
           }
           );

        modelBuilder.Entity<TemplateEntity>(
           entity =>
           {
               entity.Property(x => x.Body).HasMaxLength(maxLongTxtLenght);
               entity.Property(x => x.Title).HasMaxLength(maxShortTxtLenght);
               entity.Property(x => x.EventType).HasMaxLength(maxShortTxtLenght);
               entity.ToTable("Template", dbSchema);
               entity.HasKey(x => x.Id);
           }
           );

          


        }

    }
}
