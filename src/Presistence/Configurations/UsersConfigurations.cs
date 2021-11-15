using Axon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Axon.Presistence.Configurations
{
    class UsersConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Password).IsRequired();

            builder.Property(e => e.RefreshToken)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);
        }
    }
}
