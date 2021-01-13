using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class ApartmentRedemptionConfiguration : IEntityTypeConfiguration<Entities.ApartmentRedemption>
    {
        private string nameDatebase;

        public ApartmentRedemptionConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.ApartmentRedemption> builder)
        {
            builder.HasKey(e => e.IdApartmentRedemption);

            builder.ToTable("apartment_redemption", nameDatebase);

            builder.Property(e => e.IdApartmentRedemption)
                .HasColumnName("id_apartment_redemption")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.IdApartment)
                .HasColumnName("id_apartment")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.DateRedemption)
                .HasColumnName("date_redemption")
                .HasColumnType("datetime");
            
            builder.Property(e => e.Deleted)
                .HasColumnName("was_deleted")
                .HasColumnType("int(1)")
                .HasDefaultValue(0);

            builder.HasOne(r => r.Apartment)
                .WithMany(r => r.ApartmentRedemptions)
                .HasForeignKey(r => r.IdApartment)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(r => !r.Deleted);
        }
    }
}
