using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResidentialExpenseControl.Domain.Entities;

namespace ResidentialExpenseControl.Infrastructure.Data.Configurations
{
    // Configuração do EF Core para Transaction
    // Define relacionamentos e constraints do banco
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Value)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(t => t.Type)
                .IsRequired();

            builder.Property(t => t.CreatedAt)
                .IsRequired();

            // Relacionamento com Person
            // Uma pessoa pode ter várias transações
            builder.HasOne(t => t.Person)
                .WithMany()
                .HasForeignKey(t => t.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com Category
            // Uma categoria pode ter várias transações
            builder.HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}