using SimpleIdServer.Scim.Domain;
using System;

namespace UseSCIM.Host.SchemaBuilder
{
    public static class CustomSchemas
    {
        public static SCIMSchema GroupSchema = CustomSCIMSchemaBuilder.Create("urn:ietf:params:scim:schemas:core:2.0:Group", "Group", "Groups", "Group", true).AddStringAttribute("displayName").AddComplexAttribute("members", c =>
        {
            c.AddStringAttribute("value");
            c.AddStringAttribute("$ref");
            c.AddStringAttribute("display");
        }, multiValued: true).Build();

        public static SCIMSchema UserSchema = CustomSCIMSchemaBuilder
            .Create("urn:ietf:params:scim:schemas:core:2.0:User", "User", "Users", "User Account", true)
            .AddStringAttribute("userName", caseExact: true, uniqueness: SCIMSchemaAttributeUniqueness.SERVER)
            .AddComplexAttribute("name", c =>
            {
                c.AddStringAttribute("formatted", description: "The full name");
                c.AddStringAttribute("familyName", description: "The family name");
                c.AddStringAttribute("givenName", description: "The given name");
                c.AddStringAttribute("middleName", description: "The middle name");
                c.AddStringAttribute("honorificPrefix");
                c.AddStringAttribute("honorificSuffix");
            }, description: "The components of the user's real name.").AddStringAttribute("displayName")
            .AddStringAttribute("nickName").AddStringAttribute("profileUrl").AddStringAttribute("title")
            .AddStringAttribute("userType").AddStringAttribute("preferredLanguage").AddStringAttribute("locale")
            .AddStringAttribute("timezone").AddBooleanAttribute("active").AddStringAttribute("password")
            .AddComplexAttribute("emails", c =>
            {
                c.AddStringAttribute("value");
                c.AddStringAttribute("display");
                c.AddStringAttribute("type");
                c.AddBooleanAttribute("primary");
            }, multiValued: true).AddComplexAttribute("phoneNumbers", c =>
            {
                c.AddStringAttribute("value");
                c.AddStringAttribute("display");
                c.AddStringAttribute("type");
                c.AddBooleanAttribute("primary");
            }, multiValued: true).AddComplexAttribute("ims", c =>
            {
                c.AddStringAttribute("value");
                c.AddStringAttribute("display");
                c.AddStringAttribute("type");
                c.AddBooleanAttribute("primary");
            }, multiValued: true).AddComplexAttribute("photos", c =>
            {
                c.AddStringAttribute("value");
                c.AddStringAttribute("display");
                c.AddStringAttribute("type");
                c.AddBooleanAttribute("primary");
            }, multiValued: true).AddComplexAttribute("addresses", c =>
            {
                c.AddStringAttribute("formatted");
                c.AddStringAttribute("streetAddress");
                c.AddStringAttribute("locality");
                c.AddStringAttribute("region");
                c.AddStringAttribute("postalCode");
                c.AddStringAttribute("country");
                c.AddStringAttribute("type");
            }, multiValued: true).AddComplexAttribute("groups", c =>
            {
                c.AddStringAttribute("value");
                c.AddStringAttribute("$ref");
                c.AddStringAttribute("display");
                c.AddStringAttribute("type");
            }, multiValued: true).AddComplexAttribute("entitlements", c =>
            {
                c.AddStringAttribute("value");
                c.AddStringAttribute("display");
                c.AddStringAttribute("type");
                c.AddBooleanAttribute("primary");
            }, multiValued: true).AddComplexAttribute("roles", c =>
            {
                c.AddStringAttribute("value");
                c.AddStringAttribute("display");
                c.AddStringAttribute("type");
                c.AddBooleanAttribute("primary");
            }, multiValued: true).AddComplexAttribute("x509Certificates",
                c =>
                {
                    c.AddStringAttribute("value");
                    c.AddStringAttribute("display");
                    c.AddStringAttribute("type");
                    c.AddBooleanAttribute("primary");
                }, multiValued: true).AddComplexAttribute("groups",
                opt =>
                    opt.AddStringAttribute("value", mutability: SCIMSchemaAttributeMutabilities.READONLY),
                mutability: SCIMSchemaAttributeMutabilities.READONLY, multiValued: true).Build();

        public static SCIMSchema CustomUserExtensionSchema = CustomSCIMSchemaBuilder.Create("urn:ietf:params:scim:schemas:customUser", string.Empty, string.Empty, string.Empty, isRootSchema: false)
            .AddStringAttribute("customClaim1")
            .Build();

        public static SCIMSchema CustomUserSchema = CustomSCIMSchemaBuilder.Create("urn:ietf:params:scim:schemas:core:2.0:User", "User", "Users", "User Account", true).AddStringAttribute("userName", caseExact: true, uniqueness: SCIMSchemaAttributeUniqueness.SERVER).AddComplexAttribute("name", (Action<CustomSCIMSchemaAttributeBuilder>)(c =>
        {
            c.AddStringAttribute("formatted", description: "The full name");
            c.AddStringAttribute("familyName", description: "The family name");
            c.AddStringAttribute("givenName", description: "The given name");
            c.AddStringAttribute("middleName", description: "The middle name");
            c.AddStringAttribute("honorificPrefix");
            c.AddStringAttribute("honorificSuffix");
        }), description: "The components of the user's real name.").AddStringAttribute("displayName").AddStringAttribute("nickName").AddStringAttribute("profileUrl").AddStringAttribute("title").AddStringAttribute("userType").AddStringAttribute("preferredLanguage").AddStringAttribute("locale").AddStringAttribute("timezone").AddBooleanAttribute("active").AddStringAttribute("password").AddComplexAttribute("emails", (Action<CustomSCIMSchemaAttributeBuilder>)(c =>
        {
            c.AddStringAttribute("value");
            c.AddStringAttribute("display");
            c.AddStringAttribute("type");
            c.AddBooleanAttribute("primary");
        }), multiValued: true).AddComplexAttribute("phoneNumbers", (Action<CustomSCIMSchemaAttributeBuilder>)(c =>
        {
            c.AddStringAttribute("value");
            c.AddStringAttribute("display");
            c.AddStringAttribute("type");
            c.AddBooleanAttribute("primary");
        }), multiValued: true).AddComplexAttribute("ims", (Action<CustomSCIMSchemaAttributeBuilder>)(c =>
        {
            c.AddStringAttribute("value");
            c.AddStringAttribute("display");
            c.AddStringAttribute("type");
            c.AddBooleanAttribute("primary");
        }), multiValued: true).AddComplexAttribute("photos", (Action<CustomSCIMSchemaAttributeBuilder>)(c =>
        {
            c.AddStringAttribute("value");
            c.AddStringAttribute("display");
            c.AddStringAttribute("type");
            c.AddBooleanAttribute("primary");
        }), multiValued: true).AddComplexAttribute("addresses", (Action<CustomSCIMSchemaAttributeBuilder>)(c =>
        {
            c.AddStringAttribute("formatted");
            c.AddStringAttribute("streetAddress");
            c.AddStringAttribute("locality");
            c.AddStringAttribute("region");
            c.AddStringAttribute("postalCode");
            c.AddStringAttribute("country");
            c.AddStringAttribute("type");
        }), multiValued: true).AddComplexAttribute("groups", (Action<CustomSCIMSchemaAttributeBuilder>)(c =>
        {
            c.AddStringAttribute("value");
            c.AddStringAttribute("$ref");
            c.AddStringAttribute("display");
            c.AddStringAttribute("type");
        }), multiValued: true).AddComplexAttribute("entitlements", (Action<CustomSCIMSchemaAttributeBuilder>)(c =>
        {
            c.AddStringAttribute("value");
            c.AddStringAttribute("display");
            c.AddStringAttribute("type");
            c.AddBooleanAttribute("primary");
        }), multiValued: true).AddComplexAttribute("roles", (Action<CustomSCIMSchemaAttributeBuilder>)(c =>
        {
            c.AddStringAttribute("value");
            c.AddStringAttribute("display");
            c.AddStringAttribute("type");
            c.AddBooleanAttribute("primary");
        }), multiValued: true).AddComplexAttribute("x509Certificates", (Action<CustomSCIMSchemaAttributeBuilder>)(c =>
        {
            c.AddStringAttribute("value");
            c.AddStringAttribute("display");
            c.AddStringAttribute("type");
            c.AddBooleanAttribute("primary");
        }), multiValued: true).AddComplexAttribute("groups", (Action<CustomSCIMSchemaAttributeBuilder>)(opt => opt.AddStringAttribute("value", mutability: SCIMSchemaAttributeMutabilities.READONLY)), mutability: SCIMSchemaAttributeMutabilities.READONLY, multiValued: true)
                .AddSCIMSchemaExtension("urn:ietf:params:scim:schemas:customUser", false)
                .Build();
    }
}
