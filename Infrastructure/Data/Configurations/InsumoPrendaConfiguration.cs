using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class InsumoPrendaConfiguration : IEntityTypeConfiguration<InsumoPrenda>
    {
        public void Configure(EntityTypeBuilder<InsumoPrenda> builder)
        {
            builder.ToTable("InsumoPrenda");
            builder.HasKey(e => e.IdInsumo);
            builder.Property (e => e.IdInsumo);
            builder.HasKey(e => e.IdPrenda);
            builder.Property (e => e.IdPrenda);

            builder.HasOne (p => p.Insumo)
            .WithMany (p => p.InsumosPrendas)
            .HasForeignKey(p => p.IdInsumo);

            builder.HasOne (p => p.Prenda)
            .WithMany (p => p.insumosPrendas)
            .HasForeignKey(p => p.IdPrenda);
        }
    }
}