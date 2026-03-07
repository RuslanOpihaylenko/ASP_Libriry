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
    public class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetTokenEntity>
    {
        public void Configure(EntityTypeBuilder<PasswordResetTokenEntity> builder)
        {
            builder.HasKey(p => new { p.Id });

            builder
               .HasOne(p => p.User)
               .WithMany(u => u.PasswordResetTokens)
               .HasForeignKey(p => p.UserId);

            builder
                .Property(p => p.Expires)
                .HasDefaultValueSql("SYSDATETIME()");
        }
    }
}
