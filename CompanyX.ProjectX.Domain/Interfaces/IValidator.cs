using CompanyX.ProjectX.Domain.Models;

namespace CompanyX.ProjectX.Domain.Interfaces
{
    /// <summary>
    /// Performs validation on objects.
    /// </summary>
    /// <typeparam name="T">The type of object on which to perform validation.</typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Performs validation on the supplied item.
        /// </summary>
        /// <param name="item">The item on which to perform validation.</param>
        /// <returns>A result representing the validation outcome.</returns>
        ValidationResult Validate(T item);
    }
}
