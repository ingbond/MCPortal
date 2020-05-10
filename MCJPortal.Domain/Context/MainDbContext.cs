using MCJPortal.Domain.Models;
using MCJPortal.Domain.Models.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCJPortal.Domain.Context
{
    public class MainDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
            ApplicationUserRole, IdentityUserLogin<string>,
            IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<ListItem> ListItems { get; set; }
        public virtual DbSet<ListType> ListTypes { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectLine> ProjectLines { get; set; }
        public virtual DbSet<UserProject> UserProjects { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Documentation> Documentation { get; set; }

        public override int SaveChanges()
        {
            UpdateDateModified();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateDateModified();
            return await base.SaveChangesAsync();
        }

        private void UpdateDateModified()
        {
            var entitiesUsers = ChangeTracker.Entries().Where(x => x.Entity is ApplicationUser && (x.State == EntityState.Modified));

            foreach (var e in entitiesUsers)
            {
                ((ApplicationUser)e.Entity).DateModified = DateTime.UtcNow;
            }

            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Modified));

            foreach (var e in entities)
            {
                ((BaseEntity)e.Entity).DateModified = DateTime.UtcNow;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customization after calling base.OnModelCreating(builder);

            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationRole>().ToTable("Roles");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRoles");            

            // sequences-- 
            modelBuilder.HasSequence<Int64>("UserNumbers")
                .StartsAt(1000000000)
                .IncrementsBy(1);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.UserNumber)
                .HasDefaultValueSql("NEXT VALUE FOR UserNumbers");

            modelBuilder.Entity<ApplicationUser>()
                .Property(b => b.IsAllProjectsAllowed)
                .HasDefaultValue(true);

            // indexes on the Numbers 
            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(e => e.UserNumber)
                .IsUnique();

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.DateCreated).HasDefaultValueSql("getutcdate()").ValueGeneratedOnAdd();

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(d => d.UserProjects)
                .WithOne(d => d.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }
    }
}
