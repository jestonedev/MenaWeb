using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class RedEvaluationConfiguration : IEntityTypeConfiguration<Entities.RedEvaluation>
    {
        private string nameDatebase;

        public RedEvaluationConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.RedEvaluation> builder)
        {
            builder.HasKey(e => e.IdEvaluation);

            builder.ToTable("red_evaluation", nameDatebase);

            builder.Property(e => e.IdEvaluation)
                .HasColumnName("id_rEvaluation")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.IdApartment)
                .HasColumnName("id_apartment")
                .HasColumnType("int(11)");

            builder.Property(e => e.IdOrganization)
                .HasColumnName("id_org")
                .HasColumnType("int(11)");

            builder.Property(e => e.EvaluationPrice)
                .HasColumnName("rEvaluation_price")
                .HasMaxLength(50);

            builder.Property(e => e.Deleted)
                .HasColumnName("was_deleted")
                .HasColumnType("tinyint(1)")
                .HasDefaultValue(0);

            builder.HasOne(r => r.Apartment)
                .WithMany(r => r.RedEvaluations)
                .HasForeignKey(r => r.IdApartment)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.RedOrganization)
                .WithMany(r => r.RedEvaluations)
                .HasForeignKey(r => r.IdOrganization)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(r => !r.Deleted);
        }
    }
}
