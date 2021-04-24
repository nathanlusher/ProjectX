using CompanyX.ProjectX.Domain;
using CompanyX.ProjectX.Domain.Interfaces;
using System.Threading.Tasks;

namespace CompanyX.ProjectX.Services
{
    /// <inheritdoc/>
    public class ValueService : IValueService
    {
        /// <inheritdoc/>
        public async Task<Value> GetValueAsync(Value input)
        {
            return await Task.FromResult(input);
        }
    }
}
