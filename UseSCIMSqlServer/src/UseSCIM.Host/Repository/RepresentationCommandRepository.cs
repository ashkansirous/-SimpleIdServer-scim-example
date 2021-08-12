using Microsoft.EntityFrameworkCore;
using SimpleIdServer.Scim.Domain;
using SimpleIdServer.Scim.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using UseSCIM.Host.Models;

namespace UseSCIM.Host.Repository
{
    public class RepresentationCommandRepository : ISCIMRepresentationCommandRepository
    {
        private readonly AppDbContext _dbContext;

        public RepresentationCommandRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        public async Task<ITransaction> StartTransaction(CancellationToken token = new CancellationToken())
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync(token);
            return new EFTransaction(_dbContext, transaction);
        }

        public Task<bool> Add(SCIMRepresentation data, CancellationToken token = new CancellationToken())
        {
            if (data.ResourceType == "Users")
            {
                var record = ToUserModel(data);
                _dbContext.Users.Add(record);
            }
            else
            {
                throw new InvalidDataContractException();
            }
            return Task.FromResult(true);
        }

        public async Task<bool> Update(SCIMRepresentation data, CancellationToken token = new CancellationToken())
        {
            if (!await Delete(data, token))
            {
                return false;
            }

            await Add(data, token);
            return true;
        }

        public async Task<bool> Delete(SCIMRepresentation data, CancellationToken token = new CancellationToken())
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == data.Id, cancellationToken: token);
            if (result == null)
            {
                return await Task.FromResult(false);
            }

            _dbContext.Users.Remove(result);

            return await Task.FromResult(true);
        }

        private static User ToUserModel(SCIMRepresentation representation)
        {
            var scimRepresentationEmails = representation.Attributes.Where(attr => attr.SchemaAttribute.Name == "emails").ToList();

            var emails = scimRepresentationEmails.Select(email => new Email
                {
                    Id = Guid.NewGuid().ToString(),
                    Primary = email.Values.SingleOrDefault(attr => attr.SchemaAttribute.Name == "primary")?.ValuesBoolean.FirstOrDefault() ?? false,
                    Type = email.Values.SingleOrDefault(attr => attr.SchemaAttribute.Name == "type")?.ValuesString.FirstOrDefault(),
                    Value = email.Values.SingleOrDefault(attr => attr.SchemaAttribute.Name == "value")?.ValuesString.FirstOrDefault(),
                    UserId = representation.Id
                })
                .ToList();

            var result = new User
            {
                Id = representation.Id,
                ExternalId = representation.ExternalId,
                UserName = representation.Attributes.SingleOrDefault(attr => attr.SchemaAttribute.Name == "userName")?.ValuesString.FirstOrDefault(),
                Active = representation.Attributes.SingleOrDefault(attr => attr.SchemaAttribute.Name == "active")?.ValuesBoolean.FirstOrDefault() ?? false,
                DisplayName = representation.Attributes.SingleOrDefault(attr => attr.SchemaAttribute.Name == "displayName")?.ValuesString.FirstOrDefault(),
                Emails = emails
            };
            return result;
        }
    }
}
