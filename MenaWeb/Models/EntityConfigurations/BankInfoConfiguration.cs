using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class BankInfoConfiguration : IEntityTypeConfiguration<Entities.BankInfo>
    {
        private string nameDatebase;

        public BankInfoConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.BankInfo> builder)
        {
            builder.HasKey(e => e.IdBank);

            builder.ToTable("bank_info", nameDatebase);

            builder.Property(e => e.IdBank)
                .HasColumnName("id_bank")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.IdApartment)
                .HasColumnName("id_apartment")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.Account)
                .HasColumnName("account")
                .HasMaxLength(255);

            builder.Property(e => e.AccountHolder)
                .HasColumnName("account_holder")
                .HasMaxLength(255);

            builder.Property(e => e.Bank)
                .HasColumnName("bank")
                .HasMaxLength(1000);

            builder.Property(e => e.Sum)
                .HasColumnName("sum")
                .HasColumnType("decimal(19,2)");

            builder.Property(e => e.SumString)
                .HasColumnName("sum_string")
                .HasMaxLength(500);

            builder.Property(e => e.Deleted)
                .HasColumnName("was_deleted")
                .HasColumnType("int(1)")
                .HasDefaultValue(0);

            builder.HasOne(r => r.Apartment)
                .WithMany(r => r.BankInfos)
                .HasForeignKey(r => r.IdApartment)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(r => !r.Deleted);
        }
    }
}
