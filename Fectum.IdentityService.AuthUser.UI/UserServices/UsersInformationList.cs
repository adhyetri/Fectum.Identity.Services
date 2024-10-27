using Fectum.IdentityService.AuthUser.UI.Context;
using Fectum.IdentityService.AuthUser.UI.DataTransferObject;
using Fectum.IdentityService.AuthUser.UI.IUserInterfaces;
using Fectum.IdentityService.Model.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Fectum.IdentityService.AuthUser.UI.UserServices
{
    public class UsersInformationList(IdentityContext context) : IUserInformation, IModifyUser
    {
        private readonly IdentityContext _context = context;

        public async Task<UserInformation> GetUserInformationById(int Id)
        {
            var userInformation = await _context.IUsersInformation.SingleOrDefaultAsync(x => x.UserId == Id);
            return userInformation ?? throw new Exception("");
        }

        public async Task<List<UserInformation>> GetUsersInformation()
        {
            /*return await _context.IUsersInformation.Include(x => x.Registration).ThenInclude(x => x.UserRole)
                .Include(x => x.UserAddressDetails).Include(x => x.EducationDetails).ToListAsync();*/
            return null;
        }

        public async Task<UserInformation> UpdateUserInfomation(UserInformationDTO information, int Id)
        {
            UserInformation? userInformation = await _context.IUsersInformation.SingleOrDefaultAsync(x => x.UserId == Id);
            if (userInformation != null)
            {
                userInformation.FName = information.FName;
                userInformation.MobileNumber = information.MobileNumber;
                userInformation.DateOfBirth = information.DateOfBirth;
                userInformation.AadharNumber = information.AadharNumber;
            }
            return userInformation ?? throw new Exception($"User not found for the Id : { Id }");
        }

        public async Task<UserInformation> UpdateUserInformationField(UserInformationDTO information, int Id)
        {
            UserInformation? userInformation = await _context.IUsersInformation.SingleOrDefaultAsync(x => x.UserId == Id);
            if (userInformation != null)
            {
                userInformation.FName = information.FName;
            }

            return userInformation ?? throw new Exception($"User not found for the Id : { Id }");
        }
    }
}
