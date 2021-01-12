using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Entities.Person>
    {
        private string nameDatebase;

        public PersonConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.Person> builder)
        {
            builder.HasKey(e => e.IdPerson);

            builder.ToTable("persons", nameDatebase);

            builder.Property(e => e.IdPerson)
                .HasColumnName("id_person")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.IdApartment)
                .HasColumnName("id_apartment")
                .HasColumnType("int(11)");

            builder.Property(e => e.IdPersonStatus)
                .HasColumnName("id_person_status")
                .HasColumnType("smallint(6)")
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(e => e.IdContractor)
                .HasColumnName("id_contractor")
                .HasColumnType("tinyint(4)")
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(e => e.Family)
                .HasColumnName("family")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Father)
                .HasColumnName("father")
                .HasMaxLength(50);

            builder.Property(e => e.Birth)
                .HasColumnName("birth")
                .HasColumnType("datetime");

            builder.Property(e => e.IdDocument)
                .HasColumnName("id_document")
                .HasColumnType("smallint(6)")
                .IsRequired()
                .HasDefaultValue(255);

            builder.Property(e => e.DocumentSeria)
                .HasColumnName("document_seria")
                .HasMaxLength(8);

            builder.Property(e => e.DocumentNumber)
                .HasColumnName("document_number")
                .HasMaxLength(8);

            builder.Property(e => e.IdDocumentIssued)
                .HasColumnName("id_document_issued")
                .HasColumnType("int(10)");

            builder.Property(e => e.DocumentDate)
                .HasColumnName("document_date")
                .HasColumnType("date");

            builder.Property(e => e.BornPlace)
                .HasColumnName("born_place")
                .HasMaxLength(255);

            builder.Property(e => e.IdTemplate)
                .HasColumnName("id_template")
                .HasColumnType("int(11)");

            builder.Property(e => e.Portion)
                .HasColumnName("portion")
                .IsRequired()
                .HasMaxLength(5)
                .HasDefaultValue("1");

            builder.Property(e => e.Phone)
                .HasColumnName("phone")
                .HasMaxLength(255);

            builder.Property(e => e.Sex)
                .HasColumnName("sex")
                .HasColumnType("tinyint(1)");

            builder.Property(e => e.LastChangeUser)
                .HasColumnName("last_change_user")
                .IsRequired()
                .HasMaxLength(250)
                .HasDefaultValue("bad user");

            builder.Property(e => e.LastChangeDate)
                .HasColumnName("last_change_date")
                .HasColumnType("date")
                .IsRequired()
                .HasDefaultValue(new DateTime(1986, 11, 15));

            builder.Property(e => e.Deleted)
                .HasColumnName("was_deleted")
                .HasColumnType("tinyint(1)")
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasOne(r => r.Apartment)
                .WithMany(r => r.People)
                .HasForeignKey(r => r.IdApartment)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Contractor)
                .WithMany(r => r.People)
                .HasForeignKey(r => r.IdContractor)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.PersonStatus)
                .WithMany(r => r.People)
                .HasForeignKey(r => r.IdPersonStatus)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.DocumentIssued)
                .WithMany(r => r.People)
                .HasForeignKey(r => r.IdDocumentIssued)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(r => !r.Deleted);
        }
    }
}
