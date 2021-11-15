using Axon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Axon.Presistence.Configurations
{
    class GitHubRepositoryConfigurations : IEntityTypeConfiguration<GitHubRepository>
    {
        public void Configure(EntityTypeBuilder<GitHubRepository> builder)
        {
            builder.ToTable("GitHubRepository");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.CloneUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.DefaultBranch)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Description)
                .HasMaxLength(500);

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(e => e.GitUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.Homepage)
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(e => e.HtmlUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.Language)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.DefaultBranch)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(false);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.NodeId)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.SshUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.Url)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(d => d.GitUser)
                .WithMany(p => p.GitHubRepositories)
                .HasForeignKey(d => d.GitUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GitHubRepository_GitHubProfile");
        }
    }
}
