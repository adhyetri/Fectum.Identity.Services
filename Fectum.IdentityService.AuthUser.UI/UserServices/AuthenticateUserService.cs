using Fectum.IdentityService.AuthUser.UI.Context;
using Fectum.IdentityService.AuthUser.UI.DataTransferObject;
using Fectum.IdentityService.AuthUser.UI.Helpers.Encryption;
using Fectum.IdentityService.AuthUser.UI.IUserInterfaces;
using Fectum.IdentityService.Model.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Fectum.IdentityService.AuthUser.UI.UserServices
{
    public class AuthenticateUserService(IdentityContext Context, IConfiguration Configuration) : ILoginUser, IRegistration
    {
        private readonly IdentityContext Context = Context;
        public IConfiguration Configuration { get; } = Configuration;

        public async Task<string> SignInAsync(Credentials Credentials)
        {
            Registration registration = await Context.IUsersInfoRegistrations.SingleOrDefaultAsync(x => x.Username.Equals(Credentials.Username) && x.Password.Equals(Credentials.Password)) ?? throw new Exception("Credentials not valid");
            if (registration != null)
            {
                string token = await GenrateToken(Credentials);
                return token;
            }
            throw new Exception("User not found in the Database");
        }

        public Task<string> GenrateToken(Credentials credential)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:KEY"]!));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            List<Claim> claims =
            [
                new(JwtRegisteredClaimNames.Sub, Configuration["JWT:SUBJECT"]!),
                new(ClaimTypes.Name,credential.Username)
            ];

            JwtSecurityToken token = new(Configuration["JWT:ISSUER"], Configuration["JWT:AUDIENCE"], claims, expires: DateTime.Now.AddMinutes(10), signingCredentials: credentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Task.FromResult(jwtToken);
        }

        public async Task<string> UserRegistranstion(RegistrationDTO mappingregistration)
        {
            UserRole userRole = MappUserRole(mappingregistration.UserRole);
            Context.IUsersInfoRole.Add(userRole);
            await Context.SaveChangesAsync();

            Registration registration = MappingRegistration(mappingregistration);
            registration.RoleId = userRole.RoleId;
            Context.IUsersInfoRegistrations.Add(registration);
            await Context.SaveChangesAsync();

            UserInformation userInformation = MappingUserInformation(mappingregistration.UserInformation);
            userInformation.RegistrationId = registration.RegistrationId;
            Context.IUsersInformation.Add(userInformation);
            await Context.SaveChangesAsync();

            UserAddressDetails userAddress = MappingUserAddress(mappingregistration.UserAddressDetails);
            userAddress.UserId = userInformation.UserId;
            Context.IUsersInfoAddress.Add(userAddress);

            EducationDetails educationDetails = MappingEducationDetails(mappingregistration.EducationDetails);
            educationDetails.UserId = userInformation.UserId;
            Context.IUsersInfoEducation.Add(educationDetails);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }

            return "Successfully Registered!";
        }

        private static Registration MappingRegistration(RegistrationDTO mappingregistration)
        {
            Registration registration = new()
            {
                Username = mappingregistration.Username,
                EmailAddress = mappingregistration.EmailAddress,
                Password = HelperEncrypt.Encrpyt(mappingregistration.Password),
                ConfirmPassword = HelperEncrypt.Encrpyt(mappingregistration.ConfirmPassword)
            };
            return registration;
        }

        private static UserInformation MappingUserInformation(UserInformationDTO mappinguserInformation)
        {
            UserInformation userInformation = new()
            {
                FName = mappinguserInformation.FName,
                MobileNumber = mappinguserInformation.MobileNumber,
                AadharNumber = mappinguserInformation.AadharNumber,
                DateOfBirth = mappinguserInformation.DateOfBirth
            };

            return userInformation;
        }

        private static UserRole MappUserRole(UserRoleDTO mappingrole)
        {
            UserRole userRole = new()
            {
                RoleId = mappingrole.RoleId,
                RoleName = mappingrole.RoleName
            };

            return userRole;
        }

        private static UserAddressDetails MappingUserAddress(UserAddressDetailsDTO mappinguserAddressDetails)
        {
            UserAddressDetails userAddressDetails = new()
            {
                AddressType = mappinguserAddressDetails.AddressType,
                Country = mappinguserAddressDetails.Country,
                StreetAddress = mappinguserAddressDetails.StreetAddress,
                CityName = mappinguserAddressDetails.CityName,
                StateName = mappinguserAddressDetails.StateName,
                PostalCode = mappinguserAddressDetails.PostalCode,
                IsSameAsPermanent = mappinguserAddressDetails.IsSameAsPermanent
            };
            return userAddressDetails;
        }

        private static EducationDetails MappingEducationDetails(EducationDetailsDTO mappingEducationDetails)
        {
            EducationDetails educationDetails = new()
            {
                DegreeName = mappingEducationDetails.DegreeName,
                UniversityName = mappingEducationDetails.UniversityName,
                DepartmentName = mappingEducationDetails.DepartmentName,
                CompletionYear = mappingEducationDetails.CompletionYear
            };
            return educationDetails;
        }
    }
}
