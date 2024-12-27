using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Configuration.Config
{
    public class TransactionConfig : IEntityTypeConfiguration<Transactiion>
    {
        public void Configure(EntityTypeBuilder<Transactiion> builder)
        {
            builder.HasKey(t => t.TransactionId);

            builder.Property(t => t.Amount)
                .IsRequired();

            builder.Property(t => t.TransactionDate)
                .IsRequired();

            builder.Property(t => t.IsSuccessful)
                .IsRequired();

            builder.HasOne(x => x.SourceCard)
                .WithMany(x => x.TransactionsAsSource)
                .HasForeignKey(x => x.SourceCardNumber)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.DestinationCard)
                .WithMany(x => x.TransactionsAsDestination)
                .HasForeignKey(x => x.DestinationCardNumber)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
