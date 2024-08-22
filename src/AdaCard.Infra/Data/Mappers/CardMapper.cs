using AdaCard.Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace AdaCard.Infra.Data.Mappers;

internal static class CardMapper
{
    public static ModelBuilder CardMap(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id);
            b.Property(x => x.Lista).HasMaxLength(512);
            b.Property(x => x.Conteudo).HasMaxLength(1024);
            b.Property(x => x.Titulo).HasMaxLength(128);
        });

        return modelBuilder;
    }
}
