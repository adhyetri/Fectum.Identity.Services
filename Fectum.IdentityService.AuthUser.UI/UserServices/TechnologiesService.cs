using Fectum.IdentityService.AuthUser.UI.Context;
using Fectum.IdentityService.AuthUser.UI.IUserInterfaces;
using Fectum.IdentityService.Model.Models.HttpResponse;
using Fectum.IdentityService.Model.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Fectum.IdentityService.AuthUser.UI.UserServices
{
    public class TechnologiesService(IdentityContext context, HttpResponseMessage<string> _response) : ITechnologies
    {
        private readonly IdentityContext _context = context;
        private readonly HttpResponseMessage<string> response = _response;

        public async Task<HttpResponseMessage<string>> DeleteTechSkill(TechnologyDetails technology)
        {
            UserInformation userInformation = _context.IUsersInformation.FirstOrDefault(x => x.UserId == technology.UserId) ?? throw new Exception($"User not found : {technology.UserId}");

            try
            {
                _context.IUsersInformation.Remove(userInformation);
                await _context.SaveChangesAsync();
                response.SetSuccess(200, "Success", "Successfully deleted the record", response.IsSuccess);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
                response.SetSuccess(500, "Error", "Failed to Delete the record", response.IsSuccess);
            }

            return response;
        }

        public async Task<HttpResponseMessage<string>> PostTechSkill(TechnologyDetails technology)
        {
            UserInformation userInformation = _context.IUsersInformation.FirstOrDefault(x => x.UserId == technology.UserId) ?? throw new Exception($"User not found");
            try
            {
                _context.IUserInfoTechnologyDetails.Add(technology);
                await _context.SaveChangesAsync();

                response.SetSuccess(200, "Success", "Technology skill added successfully", response.IsSuccess);
            }
            catch (DbUpdateException ex)
            {
                response.SetError(ex.Message);
                response.SetSuccess(500, "Error", "Failed to add new technology skill", response.IsSuccess);
            }

            return response;
        }

        public async Task<HttpResponseMessage<string>> PatchTechSkill(TechnologyDetails technology)
        {
            UserInformation userInformation = _context.IUsersInformation.FirstOrDefault(x => x.UserId == technology.UserId) ?? throw new Exception("");

            try
            {
                if (!string.IsNullOrEmpty(userInformation.FName))
                {
                    technology.Technologies[1] = technology.Technologies[1];
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                response.SetError(ex.Message);
                response.SetSuccess(500, response.Content, response.Data, response.IsSuccess);
            }
            return response;
        }
    }
}
