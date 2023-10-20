using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class OrdenConfiguration : IEntityTypeConfiguration<Orden>
    {
        public void Configure(EntityTypeBuilder<Orden> builder)
        {
            builder.ToTable("Orden");
            builder.HasKey(e => e.Id);
            builder.Property (e => e.Id);

            builder. Property (p => p.Fecha)
            .HasColumnType("datatime");

            builder.HasOne (p => p.Empleado)
            .WithMany (p => p.Ordenes)
            .HasForeignKey(p => p.IdEmpleado);

            builder.HasOne (p => p.Cliente)
            .WithMany (p => p.ordenes)
            .HasForeignKey(p => p.IdCliente);

            builder.HasOne (p => p.Estado)
            .WithMany (p => p.Ordenes)
            .HasForeignKey(p => p.IdEstado);
        }
    }
}