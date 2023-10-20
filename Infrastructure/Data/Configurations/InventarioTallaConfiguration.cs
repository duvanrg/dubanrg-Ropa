using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class InventarioTallaConfiguration : IEntityTypeConfiguration<InventarioTalla>
    {
        public void Configure(EntityTypeBuilder<InventarioTalla> builder)
        {
            builder.ToTable("InventarioTalla");
            builder.HasKey(e => e.IdInv);
            builder.HasKey(e => e.IdInv);
            builder.Property (e => e.IdTalla);
            builder.Property (e => e.IdTalla);

            builder.HasOne (p => p.Inventario)
            .WithMany (p => p.InventariosTallas)
            .HasForeignKey(p => p.IdInv);

            builder.HasOne (p => p.Talla)
            .WithMany (p => p.InventariosTallas)
            .HasForeignKey(p => p.IdTalla);
        }
    }
}