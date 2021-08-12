using SimpleIdServer.Scim.Domain;
using System.Collections.Generic;
using System.Linq;
using UseSCIM.Host.Models;
using UseSCIM.Host.SchemaBuilder;

namespace UseSCIM.Host.Adapter
{
    public static class SchimObjectsAdapter
    {
        public static SCIMRepresentation ToSCIMRepresentation(this User user)
        {
            if (user is null)
                return null;

            var emails = new List<SCIMRepresentationAttribute>();
            foreach (var email in user.Emails)
            {
                var emailAttribute = new SCIMRepresentationAttribute("emails",
                    CustomSchemas.UserSchema.Attributes.FirstOrDefault(attr => attr.Name == "emails")
                );

                emailAttribute.Values.Add(new SCIMRepresentationAttribute("primary",
                    CustomSchemas.UserSchema.Attributes.FirstOrDefault(attr => attr.Name == "emails")?.SubAttributes.FirstOrDefault(attr => attr.Name == "primary"),
                    valuesBoolean: new List<bool>() { email.Primary })
                {
                    Parent = emailAttribute
                });
                emailAttribute.Values.Add(new SCIMRepresentationAttribute("type",
                    CustomSchemas.UserSchema.Attributes.FirstOrDefault(attr => attr.Name == "emails")?.SubAttributes.FirstOrDefault(attr => attr.Name == "type"),
                    valuesString: new List<string>() { email.Type })
                {
                    Parent = emailAttribute
                });
                emailAttribute.Values.Add(new SCIMRepresentationAttribute("value",
                    CustomSchemas.UserSchema.Attributes.FirstOrDefault(attr => attr.Name == "emails")?.SubAttributes.FirstOrDefault(attr => attr.Name == "value"),
                    valuesString: new List<string>() { email.Value })
                {
                    Parent = emailAttribute
                });

                emails.Add(emailAttribute);
            }


            var attributes = new List<SCIMRepresentationAttribute>
            {
                new("userName",
                    CustomSchemas.UserSchema.Attributes.FirstOrDefault(attr => attr.Name == "userName"),
                    valuesString: new List<string> {user.UserName}),
                new ("displayName",
                    CustomSchemas.UserSchema.Attributes.FirstOrDefault(attr => attr.Name == "displayName"),
                    valuesString: new List<string> {user.DisplayName}),
                new ("active",
                    CustomSchemas.UserSchema.Attributes.FirstOrDefault(attr => attr.Name == "active"),
                    valuesBoolean: new List<bool> {user.Active})
            };
            attributes.AddRange(emails);
            var scimRepresentation =
                new SCIMRepresentation(new List<SCIMSchema> {CustomSchemas.UserSchema}, attributes)
                {
                    Id = user.Id, ExternalId = user.ExternalId, ResourceType = "Users"
                };
            return scimRepresentation;
        }
    }
}
