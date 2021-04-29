using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyX.ProjectX.Domain.Interfaces
{
    // TODO: Return null for not found items in dummy repo

    /// <summary>
    /// A repository for saving and retrieving items.
    /// </summary>
    /// <typeparam name="T">The type of item contained within the repository.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Retrieves an item with the supplied id.
        /// </summary>
        /// <param name="id">The id of the item to return.</param>
        /// <returns>The item, if found; otherwise null.</returns>
        Task<T> GetItemAsync(string id);

        /// <summary>
        /// Retrieves all items.
        /// </summary>
        /// <returns>All items.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Saves an item.
        /// </summary>
        /// <param name="item">The item to save.</param>
        /// <returns>The saved item.</returns>
        Task<T> SaveItemAsync(T item);
    }
}
