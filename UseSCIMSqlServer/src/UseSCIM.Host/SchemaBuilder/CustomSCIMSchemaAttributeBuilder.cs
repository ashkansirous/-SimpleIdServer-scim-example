using System;
using System.Collections.Generic;
using SimpleIdServer.Scim.Domain;

namespace UseSCIM.Host.SchemaBuilder
{
    public class CustomSCIMSchemaAttributeBuilder
    {
        private readonly SCIMSchemaAttribute _scimSchemaAttribute;

        public CustomSCIMSchemaAttributeBuilder(SCIMSchemaAttribute scimSchemaAttribute) => this._scimSchemaAttribute = scimSchemaAttribute;

        public CustomSCIMSchemaAttributeBuilder SetType(
          SCIMSchemaAttributeTypes type)
        {
            this._scimSchemaAttribute.Type = type;
            return this;
        }

        public CustomSCIMSchemaAttributeBuilder SetMultiValued(bool mutliValued)
        {
            this._scimSchemaAttribute.MultiValued = mutliValued;
            return this;
        }

        public CustomSCIMSchemaAttributeBuilder SetRequired(bool required)
        {
            this._scimSchemaAttribute.Required = required;
            return this;
        }

        public CustomSCIMSchemaAttributeBuilder SetCaseExact(bool caseExact)
        {
            this._scimSchemaAttribute.CaseExact = caseExact;
            return this;
        }

        public CustomSCIMSchemaAttributeBuilder SetMutability(
          SCIMSchemaAttributeMutabilities mutability)
        {
            this._scimSchemaAttribute.Mutability = mutability;
            return this;
        }

        public CustomSCIMSchemaAttributeBuilder SetReturned(
          SCIMSchemaAttributeReturned returned)
        {
            this._scimSchemaAttribute.Returned = returned;
            return this;
        }

        public CustomSCIMSchemaAttributeBuilder SetUniqueness(
          SCIMSchemaAttributeUniqueness uniqueness)
        {
            this._scimSchemaAttribute.Uniqueness = uniqueness;
            return this;
        }

        public CustomSCIMSchemaAttributeBuilder AddAttribute(
          string name,
          Action<CustomSCIMSchemaAttributeBuilder> callback)
        {
            CustomSCIMSchemaAttributeBuilder attributeBuilder = new CustomSCIMSchemaAttributeBuilder(new SCIMSchemaAttribute(name)
            {
                Name = name
            });
            callback(attributeBuilder);
            this._scimSchemaAttribute.AddSubAttribute(attributeBuilder.Build());
            return this;
        }

        public CustomSCIMSchemaAttributeBuilder AddAttribute(
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
                CanonicalValues = canonicalValues
            });
            callback?.Invoke(attributeBuilder);
            this._scimSchemaAttribute.AddSubAttribute(attributeBuilder.Build());
            return this;
        }

        public CustomSCIMSchemaAttributeBuilder AddStringAttribute(
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
            return this.AddAttribute(name, SCIMSchemaAttributeTypes.STRING, callback, caseExact, required, mutability, returned, uniqueness, description, multiValued);
        }

        public CustomSCIMSchemaAttributeBuilder AddDecimalAttribute(
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
            return this.AddAttribute(name, SCIMSchemaAttributeTypes.DECIMAL, callback, caseExact, required, mutability, returned, uniqueness, description, multiValued);
        }

        public CustomSCIMSchemaAttributeBuilder AddBinaryAttribute(
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
            return this.AddAttribute(name, SCIMSchemaAttributeTypes.BINARY, callback, caseExact, required, mutability, returned, uniqueness, description, multiValued);
        }

        public CustomSCIMSchemaAttributeBuilder AddBooleanAttribute(
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

        public CustomSCIMSchemaAttributeBuilder AddDateTimeAttribute(
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

        public CustomSCIMSchemaAttributeBuilder AddIntAttribute(
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
            return this.AddAttribute(name, SCIMSchemaAttributeTypes.INTEGER, callback, caseExact, required, mutability, returned, uniqueness, description, multiValued);
        }

        public CustomSCIMSchemaAttributeBuilder AddComplexAttribute(
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

        internal SCIMSchemaAttribute Build() => this._scimSchemaAttribute;
    }
}
