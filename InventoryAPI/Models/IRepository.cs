using System.Collections.Concurrent;
using System.Collections.Generic;

namespace InventoryAPI.Models
{
    /// <summary>
    /// Generical repository interface used to process various Repository 
    /// </summary>
    /// <typeparam name="Entity">Generic </typeparam>
    public interface IRepository<Entity> where Entity : Item
    {
        /// <summary>
        /// Get a List of Items in the repository
        /// </summary>
        /// <returns>Enumerable list of items</returns>
        IEnumerable<Entity> Get();
        /// <summary>
        /// Find a item in a repository collection
        /// </summary>
        /// <param name="entity">type-safe datatype</param>
        /// <returns></returns>
        Entity Find(Entity entity);
        Entity Add(Entity entity);
        Entity Update(Entity entity);
        Entity Delete(Entity entity);
        void Clear();
    }
}
