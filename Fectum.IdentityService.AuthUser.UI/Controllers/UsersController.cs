﻿using Azure.Core;
using Fectum.IdentityService.AuthUser.UI.DataTransferObject;
using Fectum.IdentityService.AuthUser.UI.IUserInterfaces;
using HttpResponseMessage = Fectum.IdentityService.Model.Models.HttpResponse.HttpResponseMessage<string>;
using Fectum.IdentityService.Model.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Fectum.IdentityService.AuthUser.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(ILoginUser loginuser, IRegistration registeruser, IActiveUser activeUser, IUserInformation userInformationList, IModifyUser updateUser, HttpResponseMessage response) : ControllerBase
    {
        private readonly ILoginUser loginuser = loginuser;
        private readonly IRegistration registeruser = registeruser;
        private readonly IActiveUser activeUser = activeUser;
        private readonly IUserInformation userInformationList = userInformationList;
        private readonly IModifyUser updateUser = updateUser;
        private readonly HttpResponseMessage response = response;

        [HttpPost("userinfo/login")]
        public async Task<IActionResult> UserSignInAsync([FromBody] Credentials credentials)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException();
            }
            HttpResponseMessage result = await loginuser.UserSignIn(credentials).ConfigureAwait(false);
            return Ok(new { AccessToken = result });
        }

        [HttpPost("userinfo/registranstion")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegistrationDTO registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(registration);
            }

            HttpResponseMessage response = await registeruser.UserRegistranstion(registration);

            return Ok(new { response });
        }

        [HttpGet("userinfo/inactive")]
        public async Task<IActionResult> GetInActiveUserInformationListAsync()
        {
            List<UserInformation> response = await activeUser.GetInActiveUserInformationListAsync();

            return Ok(response);
        }

        [HttpGet("userinfo/active")]
        public async Task<IActionResult> GetActiveUserInformationListAsync()
        {
            List<UserInformation> response = await activeUser.GetActiveUserInformationListAsync();

            return Ok(response);
        }

        [HttpGet("userinfo/list")]
        public async Task<IActionResult> GetUsersInformationAsync()
        {
            try
            {
                List<UserInformation> users = await userInformationList.GetUsersInformation();

                if (users == null || users.Count == 0)
                {
                    return NotFound(new { Message = $"User not found" });
                }
                return StatusCode(200, users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching user information", error = ex.Message });
            }
        }

        [HttpGet(template: "userinfo/{Id:int}")]
        public async Task<IActionResult> GetUserInformationById(int Id)
        {
            try
            {
                UserInformation user = await userInformationList.GetUserInformationById(Id);

                if (user == null)
                {
                    return NotFound(new { Message = $"Unable to find user information for id : { Id }" });
                }
                return StatusCode(200, user);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = $"Something went wrong", error = ex.Message });
            }
        }

        [HttpPut(template: "userinfo/{Id:int}")]
        public async Task<IActionResult> UpdateUserInfoAsync([FromBody] UserInformationDTO userInformation, int Id)
        {
            UserInformation information = await updateUser.UpdateUserInfomation(userInformation, Id);

            return Ok(information);
        }

        [HttpPatch(template: "userinfo/{Id:int}")]
        public async Task<IActionResult> UpdateUserInformationFieldAsync([FromBody] UserInformationDTO userInformation, int Id)
        {
            UserInformation information = await updateUser.UpdateUserInformationField(userInformation, Id);

            return Ok(information);
        }
    }
}
