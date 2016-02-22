using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace InventoryAPI.Models
{
    /// <summary>
    /// MemoryRepository - Defines a type-safe "repository" of Item
    /// Uses the item's name as the key while storing the data into the repository for 
    /// future lookup.
    /// </summary>
    /// <typeparam name="Entity">Allow for Type-safe objects</typeparam>
    public class MemoryRepository<Entity> : IRepository<Entity> where Entity: Item, new()
    {
        /// <summary>
        /// repository - Thread safe collection of items in a repository. Each record
        /// is uniquely identified as a key/value pair.
        /// </summary>
        private static ConcurrentDictionary<string, Entity> repository;

        static MemoryRepository()
        {
            repository = new ConcurrentDictionary<string, Entity>();
        }
        public IEnumerable<Entity> Get()
        {
            return repository.Values.AsEnumerable(); 
        }
        public Entity Find(Entity entity)
        {
            return repository.Where(repo => repo.Key == entity.Name).First().Value;
        }
        /// <summary>
        /// Add a item to the repository
        /// </summary>
        /// <param name="entity">type-safe item</param>
        /// <returns></returns>
        public Entity Add(Entity entity)
        {
            if (entity == null)
                throw new ArgumentNullException((typeof(Entity)).Name);

            bool result = repository.TryAdd(entity.Name, entity);
            return result == false ? null : entity;
        }
        /// <summary>
        /// Update - Update/replace the value of the dictionary if the key "product name"
        /// exists
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Entity Update(Entity entity)
        {
            if (entity == null)
                throw new ArgumentNullException((typeof(Entity)).Name);

            if (!repository.ContainsKey(entity.Name))
                return null;

            repository[entity.Name] = entity;

            return entity;
        }
        /// <summary>
        /// Remove a given item from the repository
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Entity Delete(Entity entity)
        {
            Entity removed;

            if (!repository.ContainsKey(entity.Name))
                return null;

            bool result = repository.TryRemove(entity.Name, out removed);

            return !result ? null : removed;
        }
        /// <summary>
        /// Reset the repository to blank
        /// </summary>
        public void Clear()
        {
            repository.Clear();
        }
        /// <summary>
        /// The method provides for a random set of 100 products
        /// </summary>
        /// <returns></returns>
        public static IRepository<Entity> Factory()
        {
            IRepository<Entity> repo = new MemoryRepository<Entity>();
            Random rnd = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < 100; i++)
            {
                repo.Add(
                    new Entity { Name = "Product" + i, Description = "Description " + i, Price = rnd.Next(0, 1000), Quantity = rnd.Next(0, 500) }
                  );
            }

            return repo;
        }
    }
}