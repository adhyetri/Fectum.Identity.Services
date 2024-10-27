using Fectum.IdentityService.AuthUser.UI.DataTransferObject;
using Fectum.IdentityService.Model.Models.HttpResponse;

namespace Fectum.IdentityService.AuthUser.UI.IUserInterfaces
{
    public interface IRegistration
    {
        Task<HttpResponseMessage<string>> UserRegistranstion(RegistrationDTO registration);
    }
}
