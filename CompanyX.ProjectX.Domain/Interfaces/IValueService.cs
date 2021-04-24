using System.Threading.Tasks;

namespace CompanyX.ProjectX.Domain.Interfaces
{
    /// <summary>
    /// Service for processing values.
    /// </summary>
    public interface IValueService
    {
        /// <summary>
        /// Gets a value based on the supplied input value.
        /// </summary>
        /// <param name="input">The input value.</param>
        /// <returns>A value based on the supplied input value.</returns>
        Task<Value> GetValueAsync(Value input);
    }
}
