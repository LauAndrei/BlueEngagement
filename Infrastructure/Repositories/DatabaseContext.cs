using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DatabaseContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Quest> Quests { get; set; }
    public DbSet<Proof> Proofs { get; set; }
    public DbSet<TakenQuest> TakenQuests { get; set; }
    public DbSet<Badge> Badges { get; set; }
}