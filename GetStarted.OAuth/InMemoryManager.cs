using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Services.Protocols;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;

namespace GetStarted.OAuth
{
    public static class InMemoryManager
    {
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "getstartedresourceowner",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "Get Started Resource Owner",
                    Flow = Flows.ResourceOwner,
                    AllowedScopes = new List<string>()
                    {
                        Constants.StandardScopes.OpenId,
                        "read"
                    },
                    Enabled = true,
                },
                new Client
                {
                    ClientId = "getstartedimplicit",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "Get Started Implicit",
                    Flow = Flows.Implicit,
                    AllowedScopes = new List<string>()
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        "read"
                    },
                    RedirectUris = new List<string>
                    {
                        "http://localhost:64584/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:64584/"
                    },
                    Enabled = true,
                },
                new Client
                {
                    ClientId = "getstartedauthorizecode",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "Get Started Authorize Code",
                    Flow = Flows.AuthorizationCode,
                    AllowedScopes = new List<string>()
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        "read"
                    },
                    RedirectUris = new List<string>
                    {
                        "http://localhost:61393/",
                        "http://localhost:61393/AuthorizationCallback"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:61393/"
                    },
                    Enabled = true,
                },
                new Client
                {
                    ClientId = "getstartedresourceownerpassword",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "Get Started Authorize Code",
                    Flow = Flows.ResourceOwner,
                    AllowedScopes = new List<string>()
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        "read"
                    },
                    RedirectUris = new List<string>
                    {
                        "http://localhost:64904/",
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:64904/"
                    },
                    Enabled = true,
                }
            };
        }

        public static IEnumerable<Scope> GetScopes()
        {
            return new[]
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.OfflineAccess
            };
        }

        public static List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "rdiegoni@domain.com",
                    Username = "rdiegoni",
                    Password = "password",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.Name, "Romain Diegoni")
                    }
                }
            };
        }
    }
}