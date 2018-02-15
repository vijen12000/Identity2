using Core.Entities;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebSite.Core.Model
{
    public class ExtendedUser:IdentityUser
    {        
        public string FullName { get; set; }        
        public bool IsActive { get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ExtendedUser, string> manager)
        {            
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);         
            return userIdentity;
        }
    }
}