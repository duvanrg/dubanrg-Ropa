using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class InsumoProveedorConfiguration : IEntityTypeConfiguration<InsumoProveedor>
    {
        public void Configure(EntityTypeBuilder<InsumoProveedor> builder)
        {
            builder.ToTable("InsumoProveedor");
            builder.HasKey(e => e.IdInsumo);
            builder.HasKey(e => e.IdInsumo);
            builder.Property (e => e.IdProveedor);
            builder.Property (e => e.IdProveedor);

            builder.HasOne (p => p.Insumo)
            .WithMany (p => p.InsumosProveedores)
            .HasForeignKey(p => p.IdInsumo);

            builder.HasOne (p => p.Proveedor)
            .WithMany (p => p.InsumosProveedores)
            .HasForeignKey(p => p.IdProveedor);
        }
    }
}