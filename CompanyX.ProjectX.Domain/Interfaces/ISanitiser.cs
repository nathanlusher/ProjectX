namespace CompanyX.ProjectX.Domain.Interfaces
{
    /// <summary>
    /// Sanitises item by removing any sensitive information.
    /// </summary>
    public interface ISanitiser<T>
    {
        /// <summary>
        /// Sanitises the supplied item by removing any sensitive information.
        /// </summary>
        /// <param name="item">The item to sanitise.</param>
        /// <returns>The sanitised item.</returns>
        public T Sanitise(T item);
    }
}
