using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class AdditionalConfiguration : IEntityTypeConfiguration<Entities.Additional>
    {
        private string nameDatebase;

        public AdditionalConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.Additional> builder)
        {
            builder.HasKey(e => e.IdAddit);

            builder.ToTable("additional", nameDatebase);

            builder.Property(e => e.IdAddit)
                .HasColumnName("id_addit")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.IdContract)
                .HasColumnName("id_contract")
                .HasColumnType("int(11)");

            builder.Property(e => e.IdApartment1)
                .HasColumnName("id_apart1")
                .HasColumnType("int(11)");

            builder.Property(e => e.IdApartment2)
                .HasColumnName("id_apart")
                .HasColumnType("int(11)");

            builder.Property(e => e.IdPredost)
                .HasColumnName("id_predost")
                .HasColumnType("tinyint(3)");

            builder.Property(e => e.Osnovanie)
                .HasColumnName("id_osnovanie")
                .HasMaxLength(255);

            builder.Property(e => e.DateDogPor)
                .HasColumnName("dateDogPor")
                .HasColumnType("date");

            builder.Property(e => e.DogPor)
                .HasColumnName("dogPor")
                .HasMaxLength(255);

            builder.Property(e => e.IdCopy)
                .HasColumnName("id_copy")
                .HasColumnType("int(11)");

            builder.Property(e => e.Ogranichenie)
                .HasColumnName("ogranichenie")
                .HasMaxLength(255);

            builder.Property(e => e.InfoUvedom)
                .HasColumnName("dateUvedom")
                .HasMaxLength(255);

            builder.Property(e => e.Primechanie)
               .HasColumnName("primechanie")
               .HasMaxLength(2000);

            builder.Property(e => e.PhoneCivil)
               .HasColumnName("phoneCivil")
               .HasMaxLength(500);

            builder.Property(e => e.InfoSnyatUchet)
               .HasColumnName("InfoSnyatUchet")
               .HasMaxLength(500);

            builder.Property(e => e.DateEvualation)
                .HasColumnName("dateEvualation")
                .HasColumnType("date");

            builder.Property(e => e.InfoEvualation)
               .HasColumnName("infoEvualation")
               .HasMaxLength(255);

            builder.Property(e => e.DateEvualDone)
               .HasColumnName("dateEvualDone")
                .HasColumnType("date");

            builder.Property(e => e.DateZaselenie)
               .HasColumnName("dateZaselenie")
                .HasColumnType("date");

            builder.HasOne(r => r.Contract)
                .WithMany(r => r.Additionals)
                .HasForeignKey(r => r.IdContract)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Predost)
                .WithMany(r => r.Additionals)
                .HasForeignKey(r => r.IdPredost)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.CopyKgc)
                .WithMany(r => r.Additionals)
                .HasForeignKey(r => r.IdCopy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
