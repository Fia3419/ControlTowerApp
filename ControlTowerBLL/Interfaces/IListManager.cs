namespace ControlTowerBLL.Interfaces
{
    public interface IListManager<T>
    {
        /// <summary>
        /// Adds a new object to the list.
        /// </summary>
        /// <param name="item">The object to add.</param>
        void Add(T item);

        /// <summary>
        /// Removes an object from the list at a specified position.
        /// </summary>
        /// <param name="index">The position of the object to remove.</param>
        void RemoveAt(int index);

        /// <summary>
        /// Replaces an object at a specified position with a new object.
        /// </summary>
        /// <param name="index">The position of the object to replace.</param>
        /// <param name="item">The new object.</param>
        void ReplaceAt(int index, T item);

        /// <summary>
        /// Returns an object from a specified position in the list.
        /// </summary>
        /// <param name="index">The position of the object to return.</param>
        /// <returns>The object at the specified position.</returns>
        T GetAt(int index);

        /// <summary>
        /// Returns an array of strings where each string represents an object.
        /// </summary>
        /// <returns>An array of strings representing the objects.</returns>
        string[] ToStringArray();
    }
}