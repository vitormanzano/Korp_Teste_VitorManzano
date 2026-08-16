using Faturamento.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faturamento.Service.Configurations;

public class ItemNotaFiscalConfiguration : IEntityTypeConfiguration<ItemNotaFiscal>
{
    public void Configure(EntityTypeBuilder<ItemNotaFiscal> builder)
    {
        builder.ToTable("ItensNotaFiscal");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.CodigoProduto)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(i => i.DescricaoProduto)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Quantidade)
            .IsRequired();

        builder.ToTable(t => t.HasCheckConstraint("CK_ItemNotaFiscal_Quantidade_Positiva", "\"Quantidade\" > 0"));
    }
}
