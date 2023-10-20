using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.HasIndex(p => p.IdCliente)
            .IsUnique()
            .HasFilter(null);

            builder.Property (p => p.Nombre)
            .IsRequired()
            .HasMaxLength(40);

            builder.HasOne (p => p.TipoPersona)
            .WithMany (p => p.Clientes)
            .HasForeignKey(p => p.IdTipoPersona);

            builder.HasOne (p => p.Municipio)
            .WithMany (p => p.Clientes)
            .HasForeignKey(p => p.IdMunicipio);
        }
    }
}