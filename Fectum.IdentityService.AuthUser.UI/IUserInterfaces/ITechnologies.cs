using Fectum.IdentityService.Model.Models.HttpResponse;
using Fectum.IdentityService.Model.Models.Models;

namespace Fectum.IdentityService.AuthUser.UI.IUserInterfaces
{
    public interface ITechnologies
    {
        public Task<HttpResponseMessage<string>> PostTechSkill(TechnologyDetails technology);
        public Task<HttpResponseMessage<string>> PatchTechSkill(TechnologyDetails technology);
        public Task<HttpResponseMessage<string>> DeleteTechSkill(TechnologyDetails technology);
    }
}
