using ControlTowerBLL.Interfaces;

namespace ControlTowerBLL.Managers
{
    /// <summary>
    /// Provides basic list management functionalities such as adding, removing, replacing, and accessing items in a list.
    /// </summary>
    /// <typeparam name="T">The type of items to manage.</typeparam>
    public class ListManager<T> : IListManager<T>
    {
        protected readonly List<T> items;

        /// <summary>
        /// Initializes a new instance of the ListManager class.
        /// </summary>
        public ListManager()
        {
            items = new List<T>();
        }

        /// <summary>
        /// Adds an item to the list.
        /// </summary>
        /// <param name="item">The item to add to the list.</param>
        /// <exception cref="ArgumentNullException">Thrown when the item is null.</exception>
        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            items.Add(item);
        }

        /// <summary>
        /// Removes an item at the specified index from the list.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the index is out of bounds.</exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= items.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of bounds.");
            items.RemoveAt(index);
        }

        /// <summary>
        /// Replaces an item at the specified index with a new item.
        /// </summary>
        /// <param name="index">The zero-based index of the item to replace.</param>
        /// <param name="item">The new item to set at the specified index.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the index is out of bounds.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the item is null.</exception>
        public void ReplaceAt(int index, T item)
        {
            if (index < 0 || index >= items.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of bounds.");
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            items[index] = item;
        }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to get.</param>
        /// <returns>The item at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the index is out of bounds.</exception>
        public T GetAt(int index)
        {
            if (index < 0 || index >= items.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of bounds.");
            return items[index];
        }

        /// <summary>
        /// Converts the list of items to an array of strings.
        /// </summary>
        /// <returns>An array of strings representing the items in the list.</returns>
        public string[] ToStringArray()
        {
            string[] result = new string[items.Count];
            for (int i = 0; i < items.Count; i++)
            {
                result[i] = items[i].ToString();
            }
            return result;
        }
    }
}
