using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Polling_System.Models;
using System.Reflection.Emit;

namespace polling_System.Areas.Identity.Data;

public class polling_SystemContext : IdentityDbContext<IdentityUser>
{
    public polling_SystemContext(DbContextOptions<polling_SystemContext> options)
        : base(options)
    {
    }


    public DbSet<Poll> Polls { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }

    //public DbSet<Client> Clients { get; set; }
    public DbSet<ClientAnswer> ClientAnswers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ClientAnswer>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(450);
        });
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
