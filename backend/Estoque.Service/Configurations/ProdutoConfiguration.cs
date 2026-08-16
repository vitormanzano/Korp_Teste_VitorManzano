using Estoque.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estoque.Service.Configurations;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Codigo)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Descricao)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Saldo)
            .IsRequired()
            .HasDefaultValue(0);

        builder.HasIndex(p => p.Codigo)
            .IsUnique();

        builder.ToTable(t => t.HasCheckConstraint("CK_Produto_Saldo_NaoNegativo", "\"Saldo\" >= 0"));

        builder.Property<uint>("Version")
            .IsRowVersion()
            .HasColumnName("xmin")
            .HasColumnType("xid");
    }
}
