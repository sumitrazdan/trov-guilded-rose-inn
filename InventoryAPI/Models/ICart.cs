using System.Collections.Generic;

namespace InventoryAPI.Models
{
    /// <summary>
    /// Interface defines a generic shopping cart
    /// </summary>
    /// <typeparam name="T">enables for a type-safe cart</typeparam>
    public interface ICart<T> where T : Item
    {
        bool IsEmpty();
        int Size();
        IEnumerable<T> GetItem();
        void Add(T item);
        void Remove(T item);
        void Clear();
        int SubTotal();
    }
}