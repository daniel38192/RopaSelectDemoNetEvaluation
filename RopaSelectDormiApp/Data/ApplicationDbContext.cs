using Microsoft.AspNetCore.Identity;
using RopaSelectDormiApp.Entities.Clothe;
using RopaSelectDormiApp.Entities.ClotheList;
using RopaSelectDormiApp.Entities.ClotheListElement;
using RopaSelectDormiApp.Entities.Role;
using RopaSelectDormiApp.Entities.User;
using RopaSelectDormiApp.Entities.UserRole;

namespace RopaSelectDormiApp.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : 
    IdentityDbContext<
        User, 
        Role, 
        Guid, 
        IdentityUserClaim<Guid>, 
        UserRole, 
        IdentityUserLogin<Guid>, 
        IdentityRoleClaim<Guid>, 
        IdentityUserToken<Guid>
    >(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        ConfigureAspNetIdentities(builder);
        ConfigureOtherIdentities(builder);
    }

    private void ConfigureAspNetIdentities(ModelBuilder builder)
    {
        builder.Entity<User>(user =>
        {
            /*
            user.HasIndex(u => u.NormalizedUserName)
                .IsUnique();
            user.Property(u => u.NormalizedUserName)
                .IsRequired()
                .HasMaxLength(50);
                */
            user.ToTable("users");
        });
        
        builder.Entity<Role>(role =>
        {
            role.HasIndex(r => r.Name)
                .IsUnique();
            role.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);
            role.ToTable("roles");
        });
        
        builder.Entity<UserRole>(userRole =>
        {
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
            userRole.ToTable("user_roles");
        });

        builder.Entity<IdentityRoleClaim<Guid>>(roleClaim =>
        {
            roleClaim.ToTable("role_claims");
        });

        builder.Entity<IdentityUserLogin<Guid>>(userLogin =>
        {
            userLogin.ToTable("user_logins");
        });
        
        builder.Entity<IdentityUserClaim<Guid>>(userClaim =>
        {
            userClaim.ToTable("user_claims");
        });
        
        builder.Entity<IdentityUserToken<Guid>>(userToken =>
        {
            userToken.ToTable("user_tokens");
        });

    }

    private void ConfigureOtherIdentities(ModelBuilder builder)
    {
        builder.Entity<ClotheListElement>(clotheListElement =>
        {
            clotheListElement
                .HasIndex(x => new { x.ClotheId, x.ClotheListId })
                .IsUnique();
        });
    }

    public DbSet<Clothe> Clothes { get; set; } = null!;
    public DbSet<ClotheList> ClotheLists { get; set; } = null!;
    public DbSet<ClotheListElement> ClotheListElements { get; set; } = null!;
    
}