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
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
        {
            builder.HasKey(r => new { r.Id });

            builder
               .HasOne(r => r.User)
               .WithMany(u => u.RefreshTokens)
               .HasForeignKey(r => r.UserId);

            builder
                .Property(r => r.Expires)
                .HasDefaultValueSql("SYSDATETIME()");

            builder
                .Property(r => r.Created)
                .HasDefaultValueSql("SYSDATETIME()");
        }
    }
}
