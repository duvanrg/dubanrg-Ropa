using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class DetalleVentaConfiguration : IEntityTypeConfiguration<DetalleVenta>
    {
        public void Configure(EntityTypeBuilder<DetalleVenta> builder)
        {
            builder.ToTable("DetalleVenta");
            builder.HasKey(e => e.Id);
            builder.Property (e => e.Id);

            builder. Property (p => p.ValorUnit)
            .HasColumnType("double");

            builder.HasOne (p => p.Venta)
            .WithMany (p => p.DetallesVentas)
            .HasForeignKey(p => p.IdVenta);

            builder.HasOne (p => p.Producto)
            .WithMany (p => p.DetalleVentas)
            .HasForeignKey(p => p.IdProducto);

            builder.HasOne (p => p.Talla)
            .WithMany (p => p.DetallesVentas)
            .HasForeignKey(p => p.IdTalla);
        }
    }
}