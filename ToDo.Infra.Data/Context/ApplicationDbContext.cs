using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Contracts.Interfaces;
using ToDo.Domain.Models;
using ToDo.Infra.Data.Mappings;

namespace ToDo.Infra.Data.Context;

public class ApplicationDbContext : DbContext, IUnityOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Assignment> Assignments { get; set; } = null!;
    public DbSet<AssignmentList> AssignmentLists { get; set; } = null!;

    public async Task<bool> Commit() => await SaveChangesAsync() > 0;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new AssignmentMap());
        modelBuilder.ApplyConfiguration(new AssignmentListMap());
        base.OnModelCreating(modelBuilder);
    }
}