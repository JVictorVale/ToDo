using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Domain.Models;

namespace ToDo.Infra.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Password)
            .HasColumnName("password")
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.CreateAt)
            .HasColumnName("create_at")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UpdateAt)
            .HasColumnName("update_at")
            .ValueGeneratedOnAddOrUpdate();
        
        builder
            .HasMany(x => x.Assignments)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder
            .HasMany(x => x.AssignmentLists)
            .WithOne(x => x.User);
    }
}