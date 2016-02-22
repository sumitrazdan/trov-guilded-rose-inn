using InventoryAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace InventoryAPI.Tests
{
    [TestClass]
    public class ShoppingCartTest
    {
        IRepository<Item> repository;
        Cart<CartItem> shoppingCart;

        [TestInitialize]
        public void Initialize()
        {
            IList<Item> products = GetItems();
            repository = new MemoryRepository<Item>();

            foreach (var item in products)
                repository.Add(item);

            shoppingCart = new Cart<CartItem>();
        }

        [TestMethod]
        public void ShoppingCart_Is_Empty()
        {
            Assert.IsTrue(shoppingCart.IsEmpty());
        }

        [TestMethod]
        public void ShoppingCart_AddItem()
        {
            Item repoItem = new Item { Name = "Product 2" };
            repoItem = repository.Find(repoItem);
            int qty = repoItem.Quantity;
            CartItem cItem = new CartItem { Name = "Product 2", Quantity=5 };
            shoppingCart.Add(cItem);
            List<CartItem> cItems = new List<CartItem>(shoppingCart.GetItem());

            Assert.AreEqual(shoppingCart.Size(), 1);
            Assert.AreEqual(repoItem.Quantity, qty - cItem.Quantity);
        }
        [TestMethod]
        public void ShoppingCart_UpdateItem()
        {
            Item repoItem = new Item { Name = "Product 2" };
            repoItem = repository.Find(repoItem);
            int qty = repoItem.Quantity;
            CartItem cItem = new CartItem { Name = "Product 2", Quantity = 3 };
            shoppingCart.Add(cItem);
            List<CartItem> cItems = new List<CartItem>(shoppingCart.GetItem());

            Assert.AreEqual(shoppingCart.Size(), 1);
            Assert.AreEqual(repoItem.Quantity, qty - cItem.Quantity);
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
