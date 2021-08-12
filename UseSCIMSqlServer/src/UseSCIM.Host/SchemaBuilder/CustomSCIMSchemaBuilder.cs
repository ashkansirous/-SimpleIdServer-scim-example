using SimpleIdServer.Scim.Domain;
using System;
using System.Collections.Generic;

namespace UseSCIM.Host.SchemaBuilder
{
    public class CustomSCIMSchemaBuilder
    {
        private readonly string _id;
        private readonly string _name;
        private readonly string _resourceType;
        private readonly string _description;
        private readonly bool _isRootSchema;
        private readonly ICollection<SCIMSchemaAttribute> _attributes;
        private readonly ICollection<SCIMSchemaExtension> _extensions;

        public CustomSCIMSchemaBuilder(string id)
        {
            this._id = id;
            this._attributes = new List<SCIMSchemaAttribute>();
            this._extensions = new List<SCIMSchemaExtension>();
            this._isRootSchema = true;
        }

        public CustomSCIMSchemaBuilder(string id, string name, string resourceType)
          : this(id)
        {
            this._name = name;
            this._resourceType = resourceType;
        }

        public CustomSCIMSchemaBuilder(
          string id,
          string name,
          string resourceType,
          string description,
          bool isRootSchema = true)
          : this(id, name, resourceType)
        {
            this._description = description;
            this._isRootSchema = isRootSchema;
        }

        public CustomSCIMSchemaBuilder(SCIMSchema scimSchema, bool isRootSchema = false, string resourceType = null)
        {
            this._id = scimSchema.Id;
            this._attributes = scimSchema.Attributes;
            this._name = scimSchema.Name;
            this._description = scimSchema.Description;
            this._extensions = new List<SCIMSchemaExtension>();
            this._isRootSchema = isRootSchema;
            this._resourceType = resourceType;
        }

        public CustomSCIMSchemaBuilder AddSCIMSchemaExtension(string schema, bool isRequired)
        {
            this._extensions.Add(new SCIMSchemaExtension()
            {
                Id = schema,
                Schema = schema,
                Required = isRequired
            });
            return this;
        }

        public CustomSCIMSchemaBuilder AddAttribute(string name)
        {
            this._attributes.Add(new SCIMSchemaAttribute(name)
            {
                Name = name
            });
            return this;
        }

        public CustomSCIMSchemaBuilder AddAttribute(
          string name,
          SCIMSchemaAttributeTypes type,
          Action<CustomSCIMSchemaAttributeBuilder> callback = null,
          bool caseExact = false,
          bool required = false,
          SCIMSchemaAttributeMutabilities mutability = SCIMSchemaAttributeMutabilities.READWRITE,
          SCIMSchemaAttributeReturned returned = SCIMSchemaAttributeReturned.DEFAULT,
          SCIMSchemaAttributeUniqueness uniqueness = SCIMSchemaAttributeUniqueness.NONE,
          string description = null,
          bool multiValued = false,
          ICollection<string> defaulValueStr = null,
          ICollection<int> defaultValueInt = null,
          List<string> canonicalValues = null)
        {
            CustomSCIMSchemaAttributeBuilder attributeBuilder = new CustomSCIMSchemaAttributeBuilder(new SCIMSchemaAttribute(name)
            {
                Name = name,
                MultiValued = multiValued,
                CaseExact = caseExact,
                Required = required,
                Mutability = mutability,
                Returned = returned,
                Uniqueness = uniqueness,
                Type = type,
                Description = description,
                CanonicalValues = canonicalValues,
                DefaultValueInt = defaultValueInt ?? new List<int>(),
                DefaultValueString = defaulValueStr ?? new List<string>()
            });
            callback?.Invoke(attributeBuilder);
            this._attributes.Add(attributeBuilder.Build());
            return this;
        }

        public CustomSCIMSchemaBuilder AddStringAttribute(
          string name,
          Action<CustomSCIMSchemaAttributeBuilder> callback = null,
          bool caseExact = false,
          bool required = false,
          SCIMSchemaAttributeMutabilities mutability = SCIMSchemaAttributeMutabilities.READWRITE,
          SCIMSchemaAttributeReturned returned = SCIMSchemaAttributeReturned.DEFAULT,
          SCIMSchemaAttributeUniqueness uniqueness = SCIMSchemaAttributeUniqueness.NONE,
          string description = null,
          ICollection<string> defaultValue = null,
          bool multiValued = false,
          List<string> canonicalValues = null)
        {
            return this.AddAttribute(name, SCIMSchemaAttributeTypes.STRING, callback, caseExact, required, mutability, returned, uniqueness, description, multiValued, defaultValue, canonicalValues: canonicalValues);
        }

        public CustomSCIMSchemaBuilder AddDecimalAttribute(
          string name,
          Action<CustomSCIMSchemaAttributeBuilder> callback = null,
          bool caseExact = false,
          bool required = false,
          SCIMSchemaAttributeMutabilities mutability = SCIMSchemaAttributeMutabilities.READWRITE,
          SCIMSchemaAttributeReturned returned = SCIMSchemaAttributeReturned.DEFAULT,
          SCIMSchemaAttributeUniqueness uniqueness = SCIMSchemaAttributeUniqueness.NONE,
          string description = null,
          ICollection<string> defaultValue = null,
          bool multiValued = false,
          List<string> canonicalValues = null)
        {
            return this.AddAttribute(name, SCIMSchemaAttributeTypes.DECIMAL, callback, caseExact, required, mutability, returned, uniqueness, description, multiValued, defaultValue, canonicalValues: canonicalValues);
        }

        public CustomSCIMSchemaBuilder AddBinaryAttribute(
          string name,
          Action<CustomSCIMSchemaAttributeBuilder> callback = null,
          bool caseExact = false,
          bool required = false,
          SCIMSchemaAttributeMutabilities mutability = SCIMSchemaAttributeMutabilities.READWRITE,
          SCIMSchemaAttributeReturned returned = SCIMSchemaAttributeReturned.DEFAULT,
          SCIMSchemaAttributeUniqueness uniqueness = SCIMSchemaAttributeUniqueness.NONE,
          string description = null,
          ICollection<string> defaultValue = null,
          bool multiValued = false,
          List<string> canonicalValues = null)
        {
            return this.AddAttribute(name, SCIMSchemaAttributeTypes.BINARY, callback, caseExact, required, mutability, returned, uniqueness, description, multiValued, defaultValue, canonicalValues: canonicalValues);
        }

        public CustomSCIMSchemaBuilder AddBooleanAttribute(
          string name,
          Action<CustomSCIMSchemaAttributeBuilder> callback = null,
          bool caseExact = false,
          bool required = false,
          SCIMSchemaAttributeMutabilities mutability = SCIMSchemaAttributeMutabilities.READWRITE,
          SCIMSchemaAttributeReturned returned = SCIMSchemaAttributeReturned.DEFAULT,
          SCIMSchemaAttributeUniqueness uniqueness = SCIMSchemaAttributeUniqueness.NONE,
          string description = null,
          bool multiValued = false)
        {
            return this.AddAttribute(name, SCIMSchemaAttributeTypes.BOOLEAN, callback, caseExact, required, mutability, returned, uniqueness, description, multiValued);
        }

        public CustomSCIMSchemaBuilder AddDateTimeAttribute(
          string name,
          Action<CustomSCIMSchemaAttributeBuilder> callback = null,
          bool caseExact = false,
          bool required = false,
          SCIMSchemaAttributeMutabilities mutability = SCIMSchemaAttributeMutabilities.READWRITE,
          SCIMSchemaAttributeReturned returned = SCIMSchemaAttributeReturned.DEFAULT,
          SCIMSchemaAttributeUniqueness uniqueness = SCIMSchemaAttributeUniqueness.NONE,
          string description = null,
          bool multiValued = false)
        {
            return this.AddAttribute(name, SCIMSchemaAttributeTypes.DATETIME, callback, caseExact, required, mutability, returned, uniqueness, description, multiValued);
        }

        public CustomSCIMSchemaBuilder AddIntAttribute(
          string name,
          Action<CustomSCIMSchemaAttributeBuilder> callback = null,
          bool caseExact = false,
          bool required = false,
          SCIMSchemaAttributeMutabilities mutability = SCIMSchemaAttributeMutabilities.READWRITE,
          SCIMSchemaAttributeReturned returned = SCIMSchemaAttributeReturned.DEFAULT,
          SCIMSchemaAttributeUniqueness uniqueness = SCIMSchemaAttributeUniqueness.NONE,
          string description = null,
          ICollection<int> defaultValue = null,
          bool multiValued = false)
        {
            return this.AddAttribute(name, SCIMSchemaAttributeTypes.INTEGER, callback, caseExact, required, mutability, returned, uniqueness, description, multiValued, defaultValueInt: defaultValue);
        }

        public CustomSCIMSchemaBuilder AddComplexAttribute(
          string name,
          Action<CustomSCIMSchemaAttributeBuilder> callback = null,
          bool caseExact = false,
          bool required = false,
          SCIMSchemaAttributeMutabilities mutability = SCIMSchemaAttributeMutabilities.READWRITE,
          SCIMSchemaAttributeReturned returned = SCIMSchemaAttributeReturned.DEFAULT,
          SCIMSchemaAttributeUniqueness uniqueness = SCIMSchemaAttributeUniqueness.NONE,
          string description = null,
          bool multiValued = false)
        {
            return this.AddAttribute(name, SCIMSchemaAttributeTypes.COMPLEX, callback, caseExact, required, mutability, returned, uniqueness, description, multiValued);
        }

        public CustomSCIMSchemaBuilder AddAttribute(
          string name,
          Action<CustomSCIMSchemaAttributeBuilder> callback)
        {
            CustomSCIMSchemaAttributeBuilder attributeBuilder = new CustomSCIMSchemaAttributeBuilder(new SCIMSchemaAttribute(name)
            {
                Name = name
            });
            callback(attributeBuilder);
            this._attributes.Add(attributeBuilder.Build());
            return this;
        }

        public SCIMSchema Build() => new SCIMSchema()
        {
            Id = this._id,
            Name = this._name,
            ResourceType = this._resourceType,
            SchemaExtensions = this._extensions,
            Description = this._description,
            Attributes = this._attributes,
            IsRootSchema = this._isRootSchema
        };

        public static CustomSCIMSchemaBuilder Create(string id) => new CustomSCIMSchemaBuilder(id);

        public static CustomSCIMSchemaBuilder Load(SCIMSchema scimSchema, bool isRootSchema = false, string resourceType = null) => new CustomSCIMSchemaBuilder((SCIMSchema)scimSchema.Clone(), isRootSchema, resourceType);

        public static CustomSCIMSchemaBuilder Create(
          string id,
          string name,
          string resourceType)
        {
            return new CustomSCIMSchemaBuilder(id, name, resourceType);
        }

        public static CustomSCIMSchemaBuilder Create(
          string id,
          string name,
          string resourceType,
          string description,
          bool isRootSchema = false)
        {
            return new CustomSCIMSchemaBuilder(id, name, resourceType, description, isRootSchema);
        }
    }
}
