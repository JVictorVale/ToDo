using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Domain.Models;

namespace ToDo.Infra.Data.Mappings;

public class AssignmentListMap : IEntityTypeConfiguration<AssignmentList>
{
    public void Configure(EntityTypeBuilder<AssignmentList> builder)
    {
        builder.Property(c => c.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(c => c.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        
        builder.Property(c => c.CreateAt)
            .HasColumnName("create_at")
            .ValueGeneratedOnAdd();

        builder.Property(c => c.UpdateAt)
            .HasColumnName("update_at")
            .ValueGeneratedOnAddOrUpdate();
        
        builder
            .HasMany(c => c.Assignments)
            .WithOne(c => c.AssignmentList)
            .OnDelete(DeleteBehavior.Cascade);
    }
}