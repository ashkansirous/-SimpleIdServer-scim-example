using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleIdServer.Scim.Domain;
using SimpleIdServer.Scim.Persistence;
using UseSCIM.Host.Adapter;
using UseSCIM.Host.Models;

namespace UseSCIM.Host.Repository
{
    public class RepresentationQueryRepository : ISCIMRepresentationQueryRepository
    {
        private readonly AppDbContext _dbContext;

        public RepresentationQueryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<SearchSCIMRepresentationsResponse> FindSCIMRepresentations(SearchSCIMRepresentationsParameter parameter)
        {
            throw new NotImplementedException();
        }

        public async Task<SCIMRepresentation> FindSCIMRepresentationById(string representationId)
        {
            var user = await _dbContext.Users.Include(u => u.Emails).FirstOrDefaultAsync(u => u.Id == representationId);

            return user.ToSCIMRepresentation();
        }

        public async Task<SCIMRepresentation> FindSCIMRepresentationById(string representationId, string resourceType)
        {
            if (resourceType == "Users")
            {
                var user = await _dbContext.Users.Include(u => u.Emails).FirstOrDefaultAsync(u => u.Id == representationId);

                return user.ToSCIMRepresentation();
            }
            else
            {
                return new SCIMRepresentation();
            }

        }

        public async Task<IEnumerable<SCIMRepresentation>> FindSCIMRepresentationByIds(IEnumerable<string> representationIds, string resourceType)
        {
            if (resourceType == "Users")
            {
                var users = await _dbContext.Users.Include(u => u.Emails).Where(u => representationIds.Contains(u.Id)).ToListAsync();

                return users.Select(u => u.ToSCIMRepresentation());
            }
            else
            {
                return new List<SCIMRepresentation>();
            }
        }

        public async Task<SCIMRepresentation> FindSCIMRepresentationByAttribute(string attrSchemaId, string value, string endpoint = null)
        {
            var user = (await _dbContext.Users.Include(u => u.Emails).ToListAsync()).Select(u => u.ToSCIMRepresentation()).FirstOrDefault(u => u.Attributes.Any(a => a.SchemaAttribute.Id == attrSchemaId && a.ValuesString.Contains(value)));

            return user;
        }

        public async Task<SCIMRepresentation> FindSCIMRepresentationByAttribute(string attrSchemaId, int value, string endpoint = null)
        {
            var user = (await _dbContext.Users.Include(u => u.Emails).ToListAsync()).Select(u => u.ToSCIMRepresentation()).FirstOrDefault(u => u.Attributes.Any(a => a.SchemaAttribute.Id == attrSchemaId && a.ValuesInteger.Contains(value)));

            return user;
        }

        public async Task<IEnumerable<SCIMRepresentation>> FindSCIMRepresentationByAttributes(string attrSchemaId, IEnumerable<string> values, string endpoint = null)
        {
            return (await _dbContext.Users.Include(u => u.Emails).ToListAsync()).Select(u => u.ToSCIMRepresentation()).Where(r =>
                r.GetAttributesByAttrSchemaId(attrSchemaId).Any(a => a.ValuesString.Any(values.Contains))).ToList();
        }
    }
}
