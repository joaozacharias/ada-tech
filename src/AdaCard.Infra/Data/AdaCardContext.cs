using AdaCard.Core.Entities;
using AdaCard.Infra.Data.Mappers;

using Microsoft.EntityFrameworkCore;

namespace AdaCard.Infra.Data;

public class AdaCardContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Card> Cards { get; set; }

    public AdaCardContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);

        modelBuilder.UserMap();
        modelBuilder.CardMap();

        base.OnModelCreating(modelBuilder);
    }
}
