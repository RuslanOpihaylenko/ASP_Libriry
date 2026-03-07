using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => new { u.Id });

            builder
               .HasOne(u => u.CityEntity)
               .WithMany(c => c.UserEntities)
               .HasForeignKey(u => u.CityEntityId);

            builder
                .HasMany(u => u.RefreshTokens)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            builder
               .HasMany(u => u.PasswordResetTokens)
               .WithOne(p => p.User)
               .HasForeignKey(p => p.UserId);

            builder
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("SYSDATETIME()");
        }
    }
}
