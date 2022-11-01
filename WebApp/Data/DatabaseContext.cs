using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wm22App.Models;

namespace Wm22App.Data
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DbSet<Tipp> Tipps { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Bonustipp> Bonustipps { get; set; }
        public DbSet<Bonusquestion> Bonusquestions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Matchday> Matchdays { get; set; }
        public DbSet<Overview> Overview { get; set; }
        public IConfiguration Configuration { get; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration configuration) : base(options)
        {
            Configuration=configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Seed Roles and Admin START +++
            string ADMIN_ID = Guid.NewGuid().ToString();
            string AI_ID = Guid.NewGuid().ToString();
            string TIPPEXPERTE_ROLE_ID = Guid.NewGuid().ToString();
            string ADMIN_ROLE_ID = Guid.NewGuid().ToString();

            // seed admin and tippexperte role
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { 
                Name = "Admin", 
                NormalizedName = "ADMIN", 
                Id = ADMIN_ROLE_ID,
                ConcurrencyStamp = ADMIN_ROLE_ID
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { 
                Name = "Tippexperte", 
                NormalizedName = "TIPPEXPERTE", 
                Id = TIPPEXPERTE_ROLE_ID,
                ConcurrencyStamp = TIPPEXPERTE_ROLE_ID
            });

            // seed admin
            var adminUser = new User { 
                Id = ADMIN_ID,
                Email = "admin@de.de",
                EmailConfirmed = true, 
                UserName = Configuration.GetValue<string>("Credentials:AdminUsername"),
                NormalizedUserName = Configuration.GetValue<string>("Credentials:AdminUsername").ToUpper(),
                UserIcon = ""
            };

            // seed ai
            var aiUser = new User { 
                Id = AI_ID,
                Email = "ai@de.de",
                EmailConfirmed = true, 
                UserName = Configuration.GetValue<string>("Credentials:AiUsername"),
                NormalizedUserName = Configuration.GetValue<string>("Credentials:AiUsername").ToUpper(),
                UserIcon = "kicat.jpg"
            };

            // set passwords for admin and ai
            PasswordHasher<User> ph = new PasswordHasher<User>();
            adminUser.PasswordHash = ph.HashPassword(adminUser, Configuration.GetValue<string>("Credentials:AdminPassword"));
            aiUser.PasswordHash = ph.HashPassword(aiUser, Configuration.GetValue<string>("Credentials:AiPassword"));

            // add admin and ai
            modelBuilder.Entity<User>().HasData(adminUser);
            modelBuilder.Entity<User>().HasData(aiUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { 
                RoleId = ADMIN_ROLE_ID, 
                UserId = ADMIN_ID 
            });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { 
                RoleId = TIPPEXPERTE_ROLE_ID, 
                UserId = AI_ID 
            });
            // Seed Roles and Admin END +++

            modelBuilder.Entity<Bonusquestion>().ToTable("Bonusquestion");

            modelBuilder.Entity<Team>().ToTable("Team")
                .HasData(SeedService.GetTeams());

            modelBuilder.Entity<Matchday>().ToTable("Matchday")
                .HasData(SeedService.GetMatchdays());

            modelBuilder.Entity<Match>().ToTable("Match")
                .HasData(SeedService.GetMatches());

            modelBuilder.Entity<Tipp>().ToTable("Tipp");
            modelBuilder.Entity<Bonustipp>().ToTable("Bonustipp");
            modelBuilder.Entity<Overview>().ToTable("Overview");
        }
    }
}