using EntityLayer.Concretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Traversal.Web.Helpers
{
    internal class CustomClaimsPrincipalFactory :
        UserClaimsPrincipalFactory<User>
    {
        public CustomClaimsPrincipalFactory(
            UserManager<User> userManager,
            IOptions<IdentityOptions> optionsAccessor)
                : base(userManager, optionsAccessor)
        {
        }
 
        public async override Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);

            if (!string.IsNullOrEmpty(user.PhoneNumber))
            {
                if (principal.Identity != null)
                {
                    ((ClaimsIdentity)principal.Identity).AddClaims(
                        new[] { new Claim("ProfilePhotoUrl", user.ProfilePhotoUrl) });
                }
            }

            return principal;
        }
    }
}

