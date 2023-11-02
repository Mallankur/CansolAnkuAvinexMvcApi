using IdentityServer4.Models;
using IdentityServer4;

namespace IDSERVER.iDCONFIG
{
    public class IDCLIENT
    {
        //private bool RequirePkce;

        //public string ClientId { get; private set; }
        //public string ClientName { get; private set; }
        //public ICollection<string> AllowedGrantTypes { get; private set; }
        //public List<Secret> ClientSecrets { get; private set; }
        //public List<string> AllowedScopes { get; private set; }
        //public List<string> RedirectUris { get; private set; }
        //public bool AllowPlainTextPkce { get; private set; }

        public static IEnumerable<Client> Get()
        {
            return new List<Client>
        {
            new Client
            {
                ClientId = "weatherApi",
                ClientName = "ASP.NET Core Weather Api",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret> {new Secret("ProCodeGuide".Sha256())},
                AllowedScopes = new List<string> {"weatherApi.read"}
            },
            new Client
            {
                ClientId = "oidcMVCApp",
                ClientName = "Sample ASP.NET Core MVC Web App",
                ClientSecrets = new List<Secret> {new Secret("ProCodeGuide".Sha256())},

                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = new List<string> {"https://localhost:44346/signin-oidc"},
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "role",
                    "weatherApi.read"
                },

                RequirePkce = true,
                AllowPlainTextPkce = false
            }
        };
        }

    }
}
