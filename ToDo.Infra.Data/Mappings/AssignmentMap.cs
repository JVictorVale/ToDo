using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Domain.Models;

namespace ToDo.Infra.Data.Mappings;

public class AssignmentMap : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.ToTable("assignments");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Description)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(a => a.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(a => a.AssignmentListId)
            .HasColumnName("assignment_list_id")
            .IsRequired(false);

        builder.Property(a => a.Concluded)
            .HasColumnName("concluded")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(a => a.ConcludedAt)
            .HasColumnName("concluded_at")
            .IsRequired(false);

        builder.Property(a => a.Deadline)
            .HasColumnName("deadline")
            .IsRequired(false);
        
        builder.Property(a => a.CreateAt)
            .HasColumnName("create_at")
            .ValueGeneratedOnAdd();

        builder.Property(a => a.UpdateAt)
            .HasColumnName("update_at")
            .ValueGeneratedOnAddOrUpdate();

    }
}