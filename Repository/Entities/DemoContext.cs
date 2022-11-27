using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Repository.Entities
{
    public class DemoContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        protected readonly IConfiguration Configuration;

        public DemoContext()
        {

        }

        public DemoContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            if (Configuration.GetConnectionString("ApiDatabase") != null)
            {
                options.UseSqlServer(Configuration.GetConnectionString("ApiDatabase"));
            }
            else
            {
                options.UseSqlServer("Data Source=localhost; Initial Catalog=demo-app; User Id=sa; Password=C24b301992!;trustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Description = "User",
                    Id = Guid.Parse("071bc35b-6ec8-4cb0-aa1a-2f9c979fdd72")
                },
                new Role
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Description = "Ad,om",
                    Id = Guid.Parse("dfa5a606-01f7-45bf-aea5-8b4d12f80b58")
                }
            );

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = Guid.NewGuid(),
                    Name="Apple Inc",
                    Exchange="Nasdaq",
                    Ticker ="AAPL",
                    Isin = "US0378331005",
                    Website = "https://www.apple.com/"
                });

         var hasher = new PasswordHasher<User>();

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Email = "contact@cristianarsene.dev",
                    NormalizedEmail = "CONTACT@CRISTIANARSENE.DEV",
                    UserName = "crarsene",
                    NormalizedUserName = "CRARSENE",
                    FirstName = "Cristian",
                    LastName = "Arsene",
                    Active = true,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = hasher.HashPassword(null, "Parola12345!"),
                    Id = Guid.Parse("b69809b9-3af8-463d-938c-aae2b8805939")
                },
                new User
                {
                    Email = "user@gmail.com",
                    NormalizedEmail = "user@GMAIL.COM",
                    UserName = "user1",
                    NormalizedUserName = "USER1",
                    FirstName = "User",
                    LastName = "Test",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = hasher.HashPassword(null, "Parola12345!"),
                    Id = Guid.Parse("b97e9fea-26ff-4214-8741-192428677bff")
                }
            );

            modelBuilder.Entity<UserRole>().HasData(
                    new UserRole
                    {
                        RoleId = Guid.Parse("dfa5a606-01f7-45bf-aea5-8b4d12f80b58"),
                        UserId = Guid.Parse("b69809b9-3af8-463d-938c-aae2b8805939")
                    },
                    new UserRole
                    {
                        RoleId = Guid.Parse("071bc35b-6ec8-4cb0-aa1a-2f9c979fdd72"),
                        UserId = Guid.Parse("b97e9fea-26ff-4214-8741-192428677bff")
                    }
                );

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("Id");

                entity.Property(e => e.Active)
                .HasColumnName("Active");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedAt");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("Email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .HasColumnName("FirstName");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasColumnName("LastName");

                // Each User can have many entries in the UserRole join table
                entity.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                // Each Role can have many entries in the UserRole join table
                entity.ToTable("Role");
                entity.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRoles");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.RoleId).HasColumnName("RoleId");
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<Company>(entity =>
            {
                // Each Role can have many entries in the UserRole join table
                entity.ToTable("Company");
                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.Exchange).HasColumnName("Exchange");
                entity.Property(e => e.Ticker).HasColumnName("Ticker");
                entity.Property(e => e.Website).HasColumnName("Website");
                entity.Property(e => e.Isin).HasColumnName("Isin");
                entity.Property(e => e.Id).HasColumnName("Id");
            });
        }


    }
}
