using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryAPI.Models;
using System.Data.Entity;
using System.Collections.Generic;

namespace InventoryAPI.Tests
{
    [TestClass]
    public class RepositoryTest
    {
        IList<Item> products;
        IRepository<Item> repository;

        [TestInitialize]
        public void Initialize()
        {
            products = GetItems();
            repository = new MemoryRepository<Item>();

            foreach (var item in products)
                repository.Add(item);
        }
        
        [TestMethod]
        public void Repository_ShouldReturn_AllProducts()
        {
            List<Item> item = new List<Item>(repository.Get());

            Assert.AreEqual(item.Count, products.Count);
        }
        
        [TestMethod]
        public void Repository_Find_Item_and_Match_Values()
        {
            Item item = repository.Find(
                new Item { Name = "Product 6", Description = "Description 6", Price = 60, Quantity = 60 }
                );

            Assert.AreEqual(item.Name, "Product 6");
            Assert.AreEqual(item.Description, "Description 6");
            Assert.AreEqual(item.Price, 60);
            Assert.AreEqual(item.Quantity, 60);
        }

        [TestMethod]
        public void Repository_Add_Item()
        {
            repository.Add(new Item { Name = "Product 8", Description = "Description 8", Price = 80, Quantity = 80 });
            List<Item> item = new List<Item>(repository.Get());
            Assert.AreEqual(item.Count, products.Count + 1);
        }
        [TestMethod]
        public void Repository_Update_Item()
        {
            Item product = new Item { Name="Product 2", Description = "Changed Product 2 price and quantity", Price=25, Quantity=25 };
            repository.Update(product);

            Item afterUpdate = repository.Find(new Item { Name = "Product 2" });

            Assert.AreEqual(product.Name, afterUpdate.Name);
            Assert.AreEqual(product.Description, afterUpdate.Description);
            Assert.AreEqual(product.Price, afterUpdate.Price);
            Assert.AreEqual(product.Quantity, afterUpdate.Quantity);
        }
        [TestMethod]
        public void Repository_Item_Remove()
        {
            Item product = new Item { Name = "Product 7", Description = "Description 7", Price = 70 };
            repository.Delete(product);
            List<Item> item = new List<Item>(repository.Get());

            Assert.AreEqual(item.Count, products.Count);
        }
        [TestMethod]
        public void Repository_Item_RemoveAll()
        {            
            repository.Clear();
            List<Item> item = new List<Item>(repository.Get());
            Assert.AreEqual(item.Count, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Repository_Item_Find_and_InvalidOperationException()
        {
            Item item = repository.Find(new Item {  });

            Assert.AreEqual(item.Name, "Product 6");
            Assert.AreEqual(item.Description, "Description 6");
            Assert.AreEqual(item.Price, 60);
            Assert.AreEqual(item.Quantity, 60);
        }

        private IList<Item> GetItems()
        {
            IList<Item> items = new List<Item>
            {

                new Item { Name = "Product 1", Description = "Description 1", Price = 10, Quantity=10 },
                new Item { Name = "Product 2", Description = "Description 2", Price = 20, Quantity=20 },
                new Item { Name = "Product 3", Description = "Description 3", Price = 30, Quantity=30 },
                new Item { Name = "Product 4", Description = "Description 4", Price = 40, Quantity=40 },
                new Item { Name = "Product 5", Description = "Description 5", Price = 50, Quantity=50 },
                new Item { Name = "Product 6", Description = "Description 6", Price = 60, Quantity=60 },
                new Item { Name = "Product 7", Description = "Description 7", Price = 70, Quantity=70 }
            };

            return items;
        }
    }
}
