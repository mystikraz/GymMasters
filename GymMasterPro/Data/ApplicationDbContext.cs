using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymMasterPro.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Member> Members { get; set; } = default!;
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Checkin> Checkins { get; set; }
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

            builder.Entity<Membership>(t =>
            {
                t.HasOne(m => m.Member)
                .WithMany(m => m.Memberships)
                .HasForeignKey(m => m.MemberId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

                t.HasOne(m=>m.Plan)
                .WithMany(m=>m.Memberships)
                .HasForeignKey(m=>m.PlanId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Checkin>(t =>
            {
                t.HasOne(m => m.Member)
                .WithMany(m => m.Checkins)
                .HasForeignKey(m => m.MemberId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

                t.HasOne(m => m.Plan)
                .WithMany(m => m.Checkins)
                .HasForeignKey(m => m.PlanId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}