using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class CargoConfiguration : IEntityTypeConfiguration<Cargo>
    {
        public void Configure(EntityTypeBuilder<Cargo> builder)
        {
            builder.ToTable("Cargo");
            builder.HasKey(e => e.Id);
            builder.Property (e => e.Id);

            builder.Property (p => p.Descripcion)
            .IsRequired()
            .HasMaxLength(100);

            builder. Property (p => p.SueldoBase)
            .HasColumnType("double");
        }
    }
}