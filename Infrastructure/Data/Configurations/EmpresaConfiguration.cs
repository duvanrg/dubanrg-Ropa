using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("Empresa");
            builder.HasKey(e => e.Id);
            builder.Property (e => e.Id);

            builder.HasIndex(p => p.Nit)
            .IsUnique()
            .HasFilter(null);

            builder.Property (p => p.RazonSocial)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property (p => p.RepresentanteLegal)
            .IsRequired()
            .HasMaxLength(50);

            builder. Property (p => p.FechaCreacion)
            .HasColumnType("datetime");

            builder.HasOne (p => p.Municipio)
            .WithMany (p => p.Empresas)
            .HasForeignKey(p => p.IdMunicipio);
        }
    }
}