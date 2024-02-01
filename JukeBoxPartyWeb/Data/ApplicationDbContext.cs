using JukeBoxPartyWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace JukeBoxPartyWeb.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
	{
		private Guid _adminId;
		private Guid _adminRoleId;
		public ApplicationDbContext()
		{
		}

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			AddRoles(builder);
			AddUsers(builder);
			AddUserRoles(builder);
		}
		private void AddUsers(ModelBuilder builder)
		{
			ApplicationUser user = new ApplicationUser()
			{
				Id = Guid.NewGuid(),
				UserName = "admin@admin.com",
				Email = "admin@admin.com",
				LockoutEnabled = false,
				NickName = "Admin",
				SecurityStamp = Guid.NewGuid().ToString()
			};

			PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
			user.NormalizedUserName = user.UserName.ToUpper();
			user.NormalizedEmail = user.Email.ToUpper();

			user.PasswordHash = passwordHasher.HashPassword(user, "Test1234!");
			_adminId = user.Id;
			builder.Entity<ApplicationUser>().HasData(user);
		}
		private void AddUserRoles(ModelBuilder builder)
		{
			builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
			{
				RoleId = _adminRoleId,
				UserId = _adminId
			});
		}
		private void AddRoles(ModelBuilder builder)
		{
			_adminRoleId = Guid.NewGuid();
			builder.Entity<ApplicationRole>().HasData(
				new ApplicationRole()
				{
					Id = _adminRoleId,
					Name = "Admin",
					ImageUrl = "admin.png",
					NormalizedName = "ADMIN"
				},
				 new ApplicationRole()
				 {
					 Id = Guid.NewGuid(),
					 Name = "AccountManager",
					 ImageUrl = "moderator.png",
					 NormalizedName = "ACCOUNTMANAGER"
				 },
				  new ApplicationRole()
				  {
					  Id = Guid.NewGuid(),
					  Name = "SongManager",
					  ImageUrl = "musicmanager.png",
					  NormalizedName = "SONGMANAGER"
				  },
				   new ApplicationRole()
				   {
					   Id = Guid.NewGuid(),
					   Name = "User",
					   ImageUrl = "user.png",
					   NormalizedName = "USER"
				   }
				);
		}
	}
}