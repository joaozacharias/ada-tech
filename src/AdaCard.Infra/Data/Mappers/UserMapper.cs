using AdaCard.Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace AdaCard.Infra.Data.Mappers;

internal static class UserMapper
{
    public static ModelBuilder UserMap(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id);
            b.Property(x => x.Nome).HasMaxLength(100);
            b.Property(x => x.Login).HasMaxLength(100);
            b.Property(x => x.Senha).HasMaxLength(512);
        });

        return modelBuilder;
    }
}
