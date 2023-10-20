using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class PrendaConfiguration : IEntityTypeConfiguration<Prenda>
    {
        public void Configure(EntityTypeBuilder<Prenda> builder)
        {
            builder.ToTable("Prenda");
            builder.HasKey(e => e.Id);
            builder.Property (e => e.Id);
            
            builder.HasIndex(p => p.IdPrenda)
            .IsUnique()
            .HasFilter(null);

            builder.Property (p => p.Nombre)
            .IsRequired()
            .HasMaxLength(50);

            builder. Property (p => p.ValorUnitCop)
            .HasColumnType("double");

            builder. Property (p => p.ValorUnitUsd)
            .HasColumnType("double");

            builder.HasOne (p => p.Estado)
            .WithMany (p => p.Prendas)
            .HasForeignKey(p => p.IdEstado);

            builder.HasOne (p => p.TipoProteccion)
            .WithMany (p => p.Prendas)
            .HasForeignKey(p => p.IdTipoProteccion);

            builder.HasOne (p => p.Genero)
            .WithMany (p => p.Prendas)
            .HasForeignKey(p => p.IdGenero);
        }
    }
}