using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class InventarioConfiguration : IEntityTypeConfiguration<Inventario>
    {
        public void Configure(EntityTypeBuilder<Inventario> builder)
        {
            builder.ToTable("Inventario");
            builder.HasKey(e => e.Id);
            builder.Property (e => e.Id);

            builder.HasIndex(p => p.CodInv)
            .IsUnique()
            .HasFilter(null);

            builder. Property (p => p.ValorVtaCop)
            .HasColumnType("double");
            
            builder. Property (p => p.ValorVtaUsd)
            .HasColumnType("double");

            builder.HasOne (p => p.Prenda)
            .WithMany (p => p.Inventarios)
            .HasForeignKey(p => p.IdPrenda);
        }
    }
}