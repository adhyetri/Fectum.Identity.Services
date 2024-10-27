using HttpResponseMessage = Fectum.IdentityService.Model.Models.HttpResponse.HttpResponseMessage<string>;
using Fectum.IdentityService.Model.Models.Models;

namespace Fectum.IdentityService.AuthUser.UI.IUserInterfaces
{
    public interface ILoginUser
    {
        Task<HttpResponseMessage> UserSignIn(Credentials Credentials);
    }
}
