using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenPassword.Domain.Entities;

namespace OpenPassword.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        
        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.Property(u => u.Username)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(u => u.Email)
            .HasMaxLength(100)
            .IsRequired();
    }
}
