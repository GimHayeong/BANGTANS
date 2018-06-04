using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BANGTANS.Models
{
    // ApplicationUser 클래스에 더 많은 속성을 추가하여 사용자에 대한 프로필 데이터를 추가할 수 있습니다. 자세히 알아보려면 http://go.microsoft.com/fwlink/?LinkID=317594를 방문하십시오.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // authenticationType은 CookieAuthenticationOptions.AuthenticationType에 정의된 항목과 일치해야 합니다.
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 여기에 사용자 지정 사용자 클레임 추가
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>());
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<BANGTANS.Models.ArtistViewModel> ArtistViewModels { get; set; }

        public System.Data.Entity.DbSet<BANGTANS.Models.AlbumViewModel> AlbumViewModels { get; set; }

        public System.Data.Entity.DbSet<BANGTANS.Models.MemberViewModel> MemberViewModels { get; set; }
    }
}