using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MenaWeb.Models.EntityConfigurations
{
    public class ContractConfiguration : IEntityTypeConfiguration<Entities.Contract>
    {
        private string nameDatebase;

        public ContractConfiguration(string nameDatebase)
        {
            this.nameDatebase = nameDatebase;
        }

        public void Configure(EntityTypeBuilder<Entities.Contract> builder)
        {
            builder.HasKey(e => e.IdContract);

            builder.ToTable("contracts", nameDatebase);

            builder.Property(e => e.IdContract)
                .HasColumnName("id_contract")
                .HasColumnType("int(11)");

            builder.Property(e => e.IdDelegate)
                .HasColumnName("id_delegate")
                .HasColumnType("tinyint(4)")
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(e => e.IdExecutor)
                .HasColumnName("id_executor")
                .HasColumnType("tinyint(3)")
                .IsRequired()
                .HasDefaultValue(3);

            builder.Property(e => e.IdApartmentSide1)
                .HasColumnName("id_apartment_side1")
                .HasColumnType("int(11)");

            builder.Property(e => e.IdApartmentSide2)
                .HasColumnName("id_apartment_side2")
                .HasColumnType("int(11)");

            builder.Property(e => e.IdApartmentSide12)
                .HasColumnName("id_apartment_side12")
                .HasColumnType("int(11)");

            builder.Property(e => e.PreContractDate)
                .HasColumnName("pre_contract_date")
                .HasColumnType("datetime");

            builder.Property(e => e.IdPreContractIssued)
                .HasColumnName("id_pre_contract_issued")
                .HasColumnType("tinyint(4)")
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(e => e.IdContractReason)
                .HasColumnName("id_contract_reason")
                .HasColumnType("int(11)")
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(e => e.ContractRegistrationDate)
                .HasColumnName("contract_Registration_date")
                .HasColumnType("datetime");

            builder.Property(e => e.AgreementRegistrationDate)
                .HasColumnName("agreement_registration_date")
                .HasColumnType("datetime");

            builder.Property(e => e.IdAgreementRepresent)
                .HasColumnName("id_agreement_represent")
                .HasColumnType("int(11)");

            builder.Property(e => e.PreContractNumber)
                .HasColumnName("pre_contract_number")
                .HasMaxLength(20);

            builder.Property(e => e.OrderNumber)
                .HasColumnName("order_number")
                .HasMaxLength(20);

            builder.Property(e => e.OrderDate)
                .HasColumnName("order_date")
                .HasColumnType("datetime");

            builder.Property(e => e.LastChangeDate)
                .HasColumnName("last_change_date")
                .HasColumnType("datetime")
                .IsRequired()
                .HasDefaultValue(new DateTime(1986, 11, 15));

            builder.Property(e => e.LastChangeUser)
                .HasColumnName("last_change_user")
                .HasMaxLength(50);

            builder.Property(e => e.FillingDate)
               .HasColumnName("filing_date")
               .HasColumnType("datetime");

            builder.Property(e => e.EvictionRequired)
                .HasColumnName("eviction_required")
                .HasColumnType("tinyint(1)")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(e => e.Deleted)
                .HasColumnName("was_deleted")
                .HasColumnType("tinyint(1)")
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasOne(r => r.Delegate)
                .WithMany(r => r.Contracts)
                .HasForeignKey(r => r.IdDelegate)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Signer)
                .WithMany(r => r.Contracts)
                .HasForeignKey(r => r.IdExecutor)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.ApartmentSide1)
                .WithMany(r => r.Side1Contracts)
                .HasForeignKey(r => r.IdApartmentSide1)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.ApartmentSide2)
                .WithMany(r => r.Side2Contracts)
                .HasForeignKey(r => r.IdApartmentSide2)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.ApartmentSide12)
                .WithMany(r => r.Side12Contracts)
                .HasForeignKey(r => r.IdApartmentSide12)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.PreContractIssued)
                .WithMany(r => r.Contracts)
                .HasForeignKey(r => r.IdPreContractIssued)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.ContractReason)
                .WithMany(r => r.Contracts)
                .HasForeignKey(r => r.IdContractReason)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(r => !r.Deleted);
        }
    }
}
