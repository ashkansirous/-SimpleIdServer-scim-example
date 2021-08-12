using Microsoft.EntityFrameworkCore.Storage;
using SimpleIdServer.Scim.Persistence;
using System.Threading;
using System.Threading.Tasks;
using UseSCIM.Host.Models;

namespace UseSCIM.Host.Repository
{
    public class EFTransaction : ITransaction
    {
        private readonly AppDbContext _dbContext;
        private readonly IDbContextTransaction _dbContextTransaction;

        public EFTransaction(AppDbContext appDbContext, IDbContextTransaction dbContextTransaction)
        {
            _dbContext = appDbContext;
            _dbContextTransaction = dbContextTransaction;
        }

        public async Task Commit(CancellationToken token)
        {
            await _dbContext.SaveChangesAsync(token);
            await _dbContextTransaction.CommitAsync(token);
        }

        public void Dispose()
        {
            _dbContextTransaction.Dispose();
        }
    }
}