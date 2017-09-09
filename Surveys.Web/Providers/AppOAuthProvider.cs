using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin.Security;
namespace Surveys.Web.Providers
{
    public class AppOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string publicClientId;
        public AppOAuthProvider (string publicClientId)
        {
            if (string.IsNullOrEmpty(publicClientId)) throw new ArgumentNullException(nameof(publicClientId), "El identificador no es válido");
            this.publicClientId = publicClientId;
        }
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            //aca va la logica ral
            if (context.UserName == "$Libro" && context.Password == "$ytrewq21")
            {
                //crear y preparar el objeto claimIdentity
                var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, "Jorge adrian sanabria"));
                var data = new Dictionary<string, string>
                {
                    { "email", "jorge.adrian.sanabria@gmail.com" }
                };
                var properties = new AuthenticationProperties(data);
                //crea un authenticationTicket con la identidad y las propiedades
                var ticket = new AuthenticationTicket(identity, properties);
                //validar y auntenticar
                context.Validated(ticket);
                return Task.FromResult(true);
            }
            else
            {
                context.SetError("access_denied", "Acceso denegado");
                return Task.FromResult(false);
            }
        }
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => context.Validated());
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }
    }
}