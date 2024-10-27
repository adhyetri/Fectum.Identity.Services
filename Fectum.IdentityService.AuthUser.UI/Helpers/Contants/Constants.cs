namespace Fectum.IdentityService.AuthUser.UI.Helpers.Contants
{
    public class Constants
    {
        public const string cookies = "Cookies";
        public const string CookiesName = "MyCookies";
        public const string ConnectionString = "MyCookies";
        public const string EnrollPolicyName = "Authorized";
        public const string UnEnrollPolicyName = "Guest";
        public static Uri GetUri { get; set; } = new Uri("https://localhost:44398/");
    }
}
