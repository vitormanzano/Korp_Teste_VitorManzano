using Faturamento.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faturamento.Service.Configurations;

public class NotaFiscalConfiguration : IEntityTypeConfiguration<NotaFiscal>
{
    public void Configure(EntityTypeBuilder<NotaFiscal> builder)
    {
        builder.ToTable("NotasFiscais");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasMany(n => n.Itens)
            .WithOne()
            .HasForeignKey(i => i.NotaFiscalId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
