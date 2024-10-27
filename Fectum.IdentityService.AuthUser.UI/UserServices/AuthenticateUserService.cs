using Fectum.IdentityService.AuthUser.UI.Context;
using Fectum.IdentityService.AuthUser.UI.DataTransferObject;
using Fectum.IdentityService.AuthUser.UI.Helpers.Encryption;
using Fectum.IdentityService.AuthUser.UI.IUserInterfaces;
using Fectum.IdentityService.Model.Models.Enums;
using Fectum.IdentityService.Model.Models.Models;
using HttpResponseMessage = Fectum.IdentityService.Model.Models.HttpResponse.HttpResponseMessage<string>;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Fectum.IdentityService.AuthUser.UI.UserServices
{
    public class AuthenticateUserService(IdentityContext _context, IConfiguration Configuration, HttpResponseMessage _response) : ILoginUser, IRegistration
    {
        private readonly IdentityContext Context = _context;
        private readonly HttpResponseMessage response = _response;
        public IConfiguration Configuration { get; } = Configuration;

        public async Task<HttpResponseMessage> UserSignIn(Credentials Credentials)
        {
            try
            {
                Registration registration = await Context.IUsersInfoRegistrations.SingleOrDefaultAsync(x => x.Username.Equals(Credentials.Username)) ?? throw new Exception($"Username is not found : {Credentials.Username}");

                if (registration == null || HelperDecrypt.IsValidPassword(Credentials.Password, registration.Password)) 
                {
                    throw new UnauthorizedAccessException("Credentials not valid");
                }
                
                string token = await GenrateToken(Credentials);
                response.SetSuccess(200, "token", token, response.IsSuccess);
                return response;
            }
            catch (UnauthorizedAccessException ex)
            {
                response.SetError(ex.Message);
                response.SetSuccess(401, "Unauthorized", "Not authorized to access", response.IsSuccess);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during SignIn.", ex);
            }

            return response;
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

        public async Task<HttpResponseMessage> UserRegistranstion(RegistrationDTO mappingregistration)
        {
            if (mappingregistration.UserInformation.RoleName != "WorkingProfessional" && mappingregistration.UserInformation.RoleName != "CurrentlyLearning")
            {
                response.SetError($"Registration is not allowed for : {mappingregistration.UserInformation.RoleName}. Role must be either 'WorkingProfessional' or 'CurrentlyLearning'");
                response.SetSuccess(403, response.Content, response.Data, response.IsSuccess);
                return response;
            }

            try
            {
                Registration registration = MappingRegistration(mappingregistration);
                Context.IUsersInfoRegistrations.Add(registration);
                await Context.SaveChangesAsync();

                UserInformation userInformation = MappingUserInformation(mappingregistration.UserInformation);
                userInformation.RegistrationId = registration.RegistrationId;
                Context.IUsersInformation.Add(userInformation);
                await Context.SaveChangesAsync();

                response.SetSuccess(200, "Success", "Successfully Registered!", response.IsSuccess);
            }
            catch (DbUpdateException ex)
            {
                response.SetError(ex.Message);
                response.SetSuccess(500, "ServerError", "Internal Server Error", response.IsSuccess);
            }
            return response;
        }

        private static Registration MappingRegistration(RegistrationDTO mappingregistration)
        {
            Registration registration = new()
            {
                Username = mappingregistration.Username,
                EmailAddress = mappingregistration.EmailAddress,
                Password = HelperEncrypt.Encrpyt(mappingregistration.Password),
                // ConfirmPassword = HelperEncrypt.Encrpyt(mappingregistration.ConfirmPassword) : NOT REQUIRED IN BACKEND
            };
            return registration;
        }

        private static UserInformation MappingUserInformation(UserInformationDTO mappinguserInformation)
        {
            UserInformation userInformation = new();
            if (mappinguserInformation.IsWorkingProfessional)
            {
                userInformation.FName = mappinguserInformation.FName;
                userInformation.MobileNumber = mappinguserInformation.MobileNumber;
                userInformation.AadharNumber = mappinguserInformation.AadharNumber;
                userInformation.DateOfBirth = mappinguserInformation.DateOfBirth;
                userInformation.RoleName = mappinguserInformation.RoleName;
            }
            else
            {
                userInformation.FName = mappinguserInformation.FName;
                userInformation.MobileNumber = mappinguserInformation.MobileNumber;
                userInformation.AadharNumber = mappinguserInformation.AadharNumber;
                userInformation.DateOfBirth = mappinguserInformation.DateOfBirth;
                userInformation.RoleName = mappinguserInformation.RoleName;
            }

            return userInformation;
        }

        /*private static bool IsValidPassword(string pwd, string storedSaltedHashPwd)
        {
            byte[] storedHashBytes = Convert.FromBase64String(pwd);

            byte[] size = new byte[16];
            Array.Copy(storedHashBytes, 0, size, 16, 16);

            //byte[] hash = genratedHash.ComputeHash(Encoding.UTF8.GetBytes(pwd));

            byte[] bytePassword = Encoding.UTF8.GetBytes(pwd);
            byte[] combineStoredSalt = new byte[bytePassword.Length + size.Length];

            Array.Copy(size, 0, combineStoredSalt, 0, 16);
            Array.Copy(bytePassword, 0, combineStoredSalt, size.Length, bytePassword.Length);

            byte[] sand = SHA256.HashData(combineStoredSalt);
            byte[] hasPassAndSalt = new byte[16 + sand.Length];

            Array.Copy(size, 0, hasPassAndSalt, 0, 16);
            Array.Copy(sand, 0, hasPassAndSalt, 16, sand.Length);

            string computedHsh = Convert.ToBase64String(hasPassAndSalt);

            return computedHsh == storedSaltedHashPwd;
        }*/

        /*private static UserRole MappUserRole(UserRoleDTO role)
        {
            UserRoleType result = !role.IsWorkingProffesional ? UserRoleType.CurrentlyLearning : UserRoleType.WorkingProfessional;

            UserRole userRole = new() { RoleName = result };
            return userRole;
        }*/

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
