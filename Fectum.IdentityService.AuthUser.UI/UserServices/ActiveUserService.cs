using Fectum.IdentityService.AuthUser.UI.Context;
using Fectum.IdentityService.AuthUser.UI.IUserInterfaces;
using Fectum.IdentityService.Model.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Fectum.IdentityService.AuthUser.UI.UserServices
{
    public class ActiveUserService(IdentityContext Context, IConfiguration Configuration) : IActiveUser
    {
        private readonly IdentityContext Context = Context;
        public IConfiguration Configuration { get; } = Configuration;
        public async Task<List<UserInformation>> GetActiveUserInformationListAsync()
        {
            var user = await Context.IUsersInformation.ToListAsync();
            return user ?? throw new Exception("");
        }

        public async Task<List<UserInformation>> GetInActiveUserInformationListAsync()
        {
            var user = await Context.IUsersInformation.ToListAsync();
            return user ?? throw new Exception("");
        }
    }
}
