using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class EvaluatorConfiguration : IEntityTypeConfiguration<Entities.Evaluator>
    {
        private string nameDatebase;

        public EvaluatorConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.Evaluator> builder)
        {
            builder.HasKey(e => e.IdEvaluator);

            builder.ToTable("sp_evaluator", nameDatebase);

            builder.Property(e => e.IdEvaluator)
                .HasColumnName("id_evaluator")
                .HasColumnType("int(11)")
                .IsRequired();

            builder.Property(e => e.EvaluatorName)
                .HasColumnName("evaluator_name")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.EvaluatorBoss)
                .HasColumnName("evaluator_boss")
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
