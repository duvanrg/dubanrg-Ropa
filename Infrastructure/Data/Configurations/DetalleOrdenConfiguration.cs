using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class DetalleOrdenConfiguration : IEntityTypeConfiguration<DetalleOrden>
    {
        public void Configure(EntityTypeBuilder<DetalleOrden> builder)
        {
            builder.ToTable("DetalleOrden");
            builder.HasKey(e => e.Id);
            builder.Property (e => e.Id);

            builder.HasOne (p => p.Orden)
            .WithMany (p => p.DetallesOrdenes)
            .HasForeignKey(p => p.IdOrden);

            builder.HasOne (p => p.Prenda)
            .WithMany (p => p.DetallesOrdenes)
            .HasForeignKey(p => p.IdPrenda);

            builder.HasOne (p => p.Color)
            .WithMany (p => p.DetallesOrdenes)
            .HasForeignKey(p => p.IdColor);

            builder.HasOne (p => p.Estado)
            .WithMany (p => p.detallesOrdenes)
            .HasForeignKey(p => p.IdEstado);
        }
    }
}   