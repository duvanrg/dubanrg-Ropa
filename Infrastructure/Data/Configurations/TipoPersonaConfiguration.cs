using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class TipoPersonaConfiguration : IEntityTypeConfiguration<TipoPersona>
    {
        public void Configure(EntityTypeBuilder<TipoPersona> builder)
        {
            builder.ToTable("TipoPersona");
            builder.HasKey(e => e.Id);
            builder.Property (e => e.Id);
            
            builder.Property (p => p.Nombre)
            .IsRequired()
            .HasMaxLength(40);
        }
    }
}