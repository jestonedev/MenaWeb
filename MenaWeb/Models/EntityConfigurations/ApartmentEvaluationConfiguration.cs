using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class ApartmentEvaluationConfiguration : IEntityTypeConfiguration<Entities.ApartmentEvaluation>
    {
        private string nameDatebase;

        public ApartmentEvaluationConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.ApartmentEvaluation> builder)
        {
            builder.HasKey(e => e.IdApartmentEvaluation);

            builder.ToTable("apartment_evaluations", nameDatebase);

            builder.Property(e => e.IdApartmentEvaluation)
                .HasColumnName("id_apartment_evaluation")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.IdApartment)
                .HasColumnName("id_apartment")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.IdEvaluator)
               .HasColumnName("id_evaluator")
               .HasColumnType("smallint(5)");

            builder.Property(e => e.EvaluationNumber)
                .HasColumnName("evaluation_number")
                .HasMaxLength(255);

            builder.Property(e => e.EvaluationPrice)
                .HasColumnName("evaluation_price")
                .HasColumnType("decimal(19,2)");


            builder.Property(e => e.EvaluationDate)
                .HasColumnName("evaluation_date")
                .HasColumnType("datetime");

            builder.HasOne(r => r.Apartment)
                .WithMany(r => r.ApartmentEvaluations)
                .HasForeignKey(r => r.IdApartment)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Evaluator)
                .WithMany(r => r.ApartmentEvaluations)
                .HasForeignKey(r => r.IdEvaluator)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
