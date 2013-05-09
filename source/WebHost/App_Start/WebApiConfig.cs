﻿/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license.txt
 */

using System.Web.Http;
using Thinktecture.IdentityModel.Tokens.Http;

namespace Thinktecture.AuthorizationServer.WebHost
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "OAuth2 Token Endpoint",
                routeTemplate: "oauth/{appName}/token",
                defaults: new { Controller = "Token" },
                constraints: null,
                handler: new AuthenticationHandler(CreateClientAuthConfig(), config)
            );
        }

        public static AuthenticationConfiguration CreateClientAuthConfig()
        {
            var authConfig = new AuthenticationConfiguration
            {
                InheritHostClientIdentity = false,
            };

            // accept arbitrary credentials on basic auth header,
            // validation will be done in the protocol endpoint
            authConfig.AddBasicAuthentication((id, secret) => true, retainPassword: true);
            return authConfig;
        }
    }
}