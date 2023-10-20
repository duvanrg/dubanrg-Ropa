using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class TipoProteccionConfiguration : IEntityTypeConfiguration<TipoProteccion>
    {
        public void Configure(EntityTypeBuilder<TipoProteccion> builder)
        {
            builder.ToTable("TipoProteccion");
            builder.HasKey(e => e.Id);
            builder.Property (e => e.Id);

            builder.Property (p => p.Descripcion)
            .IsRequired()
            .HasMaxLength(100);
        }
    }
}