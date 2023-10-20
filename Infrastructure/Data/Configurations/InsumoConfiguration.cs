using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class InsumoConfiguration : IEntityTypeConfiguration<Insumo>
    {
        public void Configure(EntityTypeBuilder<Insumo> builder)
        {
            builder.ToTable("Insumo");
            builder.HasKey(e => e.Id);
            builder.Property (e => e.Id);

            builder.HasIndex(p => p.Nombre)
            .IsUnique()
            .HasFilter(null);

            builder.Property (p => p.Nombre)
            .IsRequired()
            .HasMaxLength(100);

            builder. Property (p => p.ValorUnit)
            .HasColumnType("double");
        }
    }
}