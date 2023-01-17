using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymMasterPro.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Trainer> Trainers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Trainer>(t =>
            {
                t.HasMany(m=>m.Members)
                .WithOne(m=>m.Trainer)
                .HasForeignKey(m=>m.TrainerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            });
        }
        public DbSet<Entities.Member> Member { get; set; } = default!;
    }
}