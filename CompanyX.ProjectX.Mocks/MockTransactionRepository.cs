using CompanyX.ProjectX.Domain.Interfaces;
using CompanyX.ProjectX.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyX.ProjectX.Mocks
{
    public class MockTransactionRepository : IRepository<Transaction>
    {
        private readonly Dictionary<string, Transaction> _items;

        public MockTransactionRepository()
        {
            _items = new Dictionary<string, Transaction>();
        }

        public Task<Transaction> GetItemAsync(string id)
        {
            return Task.FromResult(_items.FirstOrDefault(i => i.Key == id).Value);
        }

        public Task<Transaction> SaveItemAsync(Transaction item)
        {
            string id = item.Response.Id;

            id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;

            _items[id] = item;

            return Task.FromResult(item);
        }
    }
}
