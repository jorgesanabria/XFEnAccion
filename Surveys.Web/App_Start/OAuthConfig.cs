﻿using Microsoft.Owin.Security.OAuth;
using Owin;
using Surveys.Web.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Surveys.Web.App_Start
{
    public class OAuthConfig
    {
        public static string PublicClientId { get; set; }
        public static OAuthAuthorizationServerOptions OAuthOptions { get; set; }
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        static OAuthConfig()
        {
            PublicClientId = "LibroXamarinForms";
        }
        public static void ConfigureOAuth(IAppBuilder app, HttpConfiguration config)
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new Microsoft.Owin.PathString("/token"),
                AuthorizeEndpointPath = new Microsoft.Owin.PathString("/auth"),
                Provider = new AppOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true
            };
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions
            {
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                Realm = "LibroXamarinForms"
            };
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }
    }
}