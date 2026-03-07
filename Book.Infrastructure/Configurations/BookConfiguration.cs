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
    public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.HasKey(b => new { b.Id });

            builder
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId);
            builder
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books);
        }
    }
}
