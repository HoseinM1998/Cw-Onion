using Hw14.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw14.Configuration
{
    public class CardConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CardNumber)
                .IsRequired()
                .HasMaxLength(16);

            builder.Property(c => c.HolderName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Balance)
                .IsRequired();

            builder.Property(c => c.Password)
                .IsRequired()
                .HasMaxLength(4);

        }
    }
}
