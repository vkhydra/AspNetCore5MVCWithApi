using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TesteWebApp.App_Start
{
    public class AppJwtTokens
    {
        public const string Issuer = "STS";
        public const string Audience = "KmonApiUser";
        public const string Key = "Ct09r050 K367885";
        public const string AuthSchemes = "Identity.Application" + "," + JwtBearerDefaults.AuthenticationScheme;
    }
}
