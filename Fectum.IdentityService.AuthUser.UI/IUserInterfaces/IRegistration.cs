using Fectum.IdentityService.AuthUser.UI.DataTransferObject;

namespace Fectum.IdentityService.AuthUser.UI.IUserInterfaces
{
    public interface IRegistration
    {
        Task<string> UserRegistranstion(RegistrationDTO registration);
    }
}
