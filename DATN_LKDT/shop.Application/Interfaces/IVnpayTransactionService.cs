using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Interfaces
{
    public interface IVnpayTransactionService
    {
        public Task<string> BeginTransaction(Guid userId, Guid? voucherId);
        public Task<VnpayTransactions> GetTransaction(string transactionId);
        public Task<bool> CommitTransaction(string transactionId);
        public Task<bool> RollbackTransaction(string transactionId);
    }
}
