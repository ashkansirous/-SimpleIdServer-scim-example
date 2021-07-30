using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SimpleIdServer.Jwt;
using SimpleIdServer.Jwt.Extensions;
using SimpleIdServer.Scim;
using SimpleIdServer.Scim.Builder;
using SimpleIdServer.Scim.Domain;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace UseSCIM.Host
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Env = env;
            Configuration = configuration;
        }

        public IWebHostEnvironment Env { get; }
        public IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var json = File.ReadAllText("oauth_puk.txt");
            var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            var rsaParameters = new RSAParameters
            {
                Modulus = dic.TryGet(RSAFields.Modulus),
                Exponent = dic.TryGet(RSAFields.Exponent)
            };
            var oauthRsaSecurityKey = new RsaSecurityKey(rsaParameters);
            services.AddMvc(o =>
            {
                o.EnableEndpointRouting = false;
            }).AddNewtonsoftJson(_ => { });
            services.AddLogging();
            services.AddAuthorization(opts =>
            {
                // By pass authorization rules.
                opts.AddPolicy("QueryScimResource", p => p.RequireAssertion(_ => true));
                opts.AddPolicy("AddScimResource", p => p.RequireAssertion(_ => true));
                opts.AddPolicy("DeleteScimResource", p => p.RequireAssertion(_ => true));
                opts.AddPolicy("UpdateScimResource", p => p.RequireAssertion(_ => true));
                opts.AddPolicy("BulkScimResource", p => p.RequireAssertion(_ => true));
                opts.AddPolicy("UserAuthenticated", p => p.RequireAssertion(_ => true));
                opts.AddPolicy("Provison", p => p.RequireAssertion(_ => true));
            });
            services.AddAuthentication(SCIMConstants.AuthenticationScheme)
                .AddJwtBearer(SCIMConstants.AuthenticationScheme, cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "http://localhost:60000",
                        ValidAudiences = new List<string>
                        {
                            "scimClient"
                        },
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = oauthRsaSecurityKey
                    };
                });
            services.AddSIDScim().AddSchemas(CreateScimSchemas());
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseMvc();
        }

        private List<SCIMSchema> CreateScimSchemas()
        {
            var schemasToAdd = new List<SCIMSchema>() { SCIMConstants.StandardSchemas.GroupSchema };

            schemasToAdd.AddRange(CreateUserSCIMSchema());

            return schemasToAdd;
        }

        private IEnumerable<SCIMSchema> CreateUserSCIMSchema()
        {
            var usersSchemas = new List<SCIMSchema>();

            var customUserSchema = SCIMSchemaBuilder.Create("urn:ietf:params:scim:schemas:customUser")
                .AddStringAttribute("customClaim1")
                .Build();

            var userSchema = SCIMSchemaBuilder.Create("urn:ietf:params:scim:schemas:core:2.0:User", "User", SCIMConstants.SCIMEndpoints.User, "User Account", true)
                    .AddStringAttribute("userName", caseExact: true, uniqueness: SCIMSchemaAttributeUniqueness.SERVER)
                    .AddComplexAttribute("name", c =>
                    {
                        c.AddStringAttribute("formatted", description: "The full name");
                        c.AddStringAttribute("familyName", description: "The family name");
                        c.AddStringAttribute("givenName", description: "The given name");
                        c.AddStringAttribute("middleName", description: "The middle name");
                        c.AddStringAttribute("honorificPrefix");
                        c.AddStringAttribute("honorificSuffix");
                    }, description: "The components of the user's real name.")
                    .AddStringAttribute("displayName")
                    .AddStringAttribute("nickName")
                    .AddStringAttribute("profileUrl")
                    .AddStringAttribute("title")
                    .AddStringAttribute("userType")
                    .AddStringAttribute("preferredLanguage")
                    .AddStringAttribute("locale")
                    .AddStringAttribute("timezone")
                    .AddBooleanAttribute("active")
                    .AddStringAttribute("password")
                    .AddComplexAttribute("emails", callback: c =>
                    {
                        c.AddStringAttribute("value");
                        c.AddStringAttribute("display");
                        c.AddStringAttribute("type");
                        c.AddBooleanAttribute("primary");
                    }, multiValued: true)
                    .AddComplexAttribute("phoneNumbers", callback: c =>
                    {
                        c.AddStringAttribute("value");
                        c.AddStringAttribute("display");
                        c.AddStringAttribute("type");
                        c.AddBooleanAttribute("primary");

                    }, multiValued: true)
                    .AddComplexAttribute("ims", callback: c =>
                    {
                        c.AddStringAttribute("value");
                        c.AddStringAttribute("display");
                        c.AddStringAttribute("type");
                        c.AddBooleanAttribute("primary");
                    }, multiValued: true)
                    .AddComplexAttribute("photos", callback: c =>
                    {
                        c.AddStringAttribute("value");
                        c.AddStringAttribute("display");
                        c.AddStringAttribute("type");
                        c.AddBooleanAttribute("primary");
                    }, multiValued: true)
                    .AddComplexAttribute("addresses", callback: c =>
                    {
                        c.AddStringAttribute("formatted");
                        c.AddStringAttribute("streetAddress");
                        c.AddStringAttribute("locality");
                        c.AddStringAttribute("region");
                        c.AddStringAttribute("postalCode");
                        c.AddStringAttribute("country");
                        c.AddStringAttribute("type");
                    }, multiValued: true)
                    .AddComplexAttribute("groups", callback: c =>
                    {
                        c.AddStringAttribute("value");
                        c.AddStringAttribute("$ref");
                        c.AddStringAttribute("display");
                        c.AddStringAttribute("type");
                    }, multiValued: true)
                    .AddComplexAttribute("entitlements", callback: c =>
                    {
                        c.AddStringAttribute("value");
                        c.AddStringAttribute("display");
                        c.AddStringAttribute("type");
                        c.AddBooleanAttribute("primary");
                    }, multiValued: true)
                    .AddComplexAttribute("roles", callback: c =>
                    {
                        c.AddStringAttribute("value");
                        c.AddStringAttribute("display");
                        c.AddStringAttribute("type");
                        c.AddBooleanAttribute("primary");
                    }, multiValued: true)
                    .AddComplexAttribute("x509Certificates", callback: c =>
                    {
                        c.AddStringAttribute("value");
                        c.AddStringAttribute("display");
                        c.AddStringAttribute("type");
                        c.AddBooleanAttribute("primary");
                    }, multiValued: true)
                    .AddComplexAttribute("groups", opt =>
                    {
                        opt.AddStringAttribute("value", mutability: SCIMSchemaAttributeMutabilities.READONLY);
                    }, multiValued: true, mutability: SCIMSchemaAttributeMutabilities.READONLY)
                    .AddSCIMSchemaExtension("urn:ietf:params:scim:schemas:customUser", false)
                    .Build();

            usersSchemas.Add(userSchema);
            usersSchemas.Add(customUserSchema);
            return usersSchemas;
        }
    }
}