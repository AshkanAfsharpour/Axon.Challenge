using Axon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Axon.Presistence.Configurations
{
    class GitHubProfileConfigurations : IEntityTypeConfiguration<GitHubProfile>
    {
        public void Configure(EntityTypeBuilder<GitHubProfile> builder)
        {
            builder.ToTable("GitHubProfile");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.AvatarUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.Bio)
                .HasMaxLength(250);

            builder.Property(e => e.Blog)
                .HasMaxLength(250);

            builder.Property(e => e.Company)
                .HasMaxLength(150);

            builder.Property(e => e.Email)
                .HasMaxLength(150);

            builder.Property(e => e.HtmlUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.Location)
                .HasMaxLength(100);

            builder.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.NodeId)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Url)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
