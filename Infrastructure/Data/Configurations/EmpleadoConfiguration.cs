using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.ToTable("Empleado");
            builder.HasKey(e => e.Id);
            builder.Property (e => e.Id);

            builder.HasIndex(p => p.IdEmpleado)
            .IsUnique()
            .HasFilter(null);

            builder.Property (p => p.Nombre)
            .IsRequired()
            .HasMaxLength(40);

            builder. Property (p => p.FechaIngreso)
            .HasColumnType("datetime");

            builder.HasOne (p => p.Cargo)
            .WithMany (p => p.Empleados)
            .HasForeignKey(p => p.IdCargo);

            builder.HasOne (p => p.Municipio)
            .WithMany (p => p.Empleados)
            .HasForeignKey(p => p.IdMunicipio);
        }
    }
}