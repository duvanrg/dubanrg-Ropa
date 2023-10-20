using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            builder.ToTable("Departamento");
            builder.HasKey(e => e.Id);
            builder.Property (e => e.Id);

            builder.Property (p => p.Nombre)
            .IsRequired()
            .HasMaxLength(40);

            builder.HasOne (p => p.Pais)
            .WithMany (p => p.Departamentos)
            .HasForeignKey(p => p.IdPais);
        }
    }
}