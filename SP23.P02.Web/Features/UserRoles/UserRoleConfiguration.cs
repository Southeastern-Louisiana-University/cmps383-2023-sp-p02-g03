using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace SP23.P02.Web.Features.UserRoles
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(x => new { x.UserId, x.RoleId });

            builder.HasOne(x => x.Role)
                   .WithMany(x => x.Users)
                   .HasForeignKey(x => x.RoleId)
                   .IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(x => x.Roles)
                   .HasForeignKey(x => x.UserId)
                   .IsRequired();
        }
    }
}
