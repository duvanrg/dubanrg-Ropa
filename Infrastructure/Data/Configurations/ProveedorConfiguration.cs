using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
            builder.ToTable("Proveedor");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.HasIndex(p => p.NitProveedor)
            .IsUnique()
            .HasFilter(null);

            builder.Property(p => p.NitProveedor)
            .IsRequired()
            .HasMaxLength(20);

            builder.Property(p => p.Nombre)
            .IsRequired()
            .HasMaxLength(40);

            builder.HasOne (p => p.TipoPersona)
            .WithMany (p => p.Proveedores)
            .HasForeignKey(p => p.IdtipoPersona);

            builder.HasOne (p => p.Municipio)
            .WithMany (p => p.Proveedores)
            .HasForeignKey(p => p.Municipio);
        }
    }
}