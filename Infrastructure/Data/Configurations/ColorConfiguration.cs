using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.ToTable("Color");
            builder.HasKey(e => e.Id);
            builder.Property (e => e.Id);

            builder.Property (p => p.Descripcion)
            .IsRequired()
            .HasMaxLength(50);
        }
    }
}