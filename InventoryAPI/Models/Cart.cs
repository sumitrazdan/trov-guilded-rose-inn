using InventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryAPI.Models
{
    public class Cart <T>: ICart<T> where T : CartItem 
    {
        IList<T> itemList;
        static IRepository<Item> repository;
        static readonly object mylock = new object();
        static Cart()
        {
            repository = new MemoryRepository<Item>();
        }
        public Cart() { }

        public bool IsEmpty() { return (itemList == null || itemList.Count()<=0) ? true: false; }
        public int Size() { return itemList.Count(); }
        public bool HasItem(T item) { return itemList.Where(prd => prd.Name != item.Name).Count()>0 ? true : false; }
        public IEnumerable<T> GetItem()
        {
            return itemList;
        }
        public void Add(T item)
        {
            lock (mylock)
            {
                if (IsEmpty() || !HasItem(item))
                {
                    itemList = new List<T>();
                    
                    if (repository.Find(item).Quantity - item.Quantity > 0)
                    {
                        repository.Find(item).Quantity -= item.Quantity;
                        item.Price = repository.Find(item).Price;
                        itemList.Add(item);
                    }
                    else
                    {
                        throw new InvalidOperationException("Add: Out of stock");
                    }
                }
                else
                    Update(item);
            }
        }
        private void Update(T item)
        {
            lock (mylock)
            {
                Item found;

                found = itemList.FirstOrDefault(i => i.Name == item.Name);

                if (found != null)
                {
                    if (repository.Find(item).Quantity - item.Quantity > 0)
                    {
                        found.Quantity = item.Quantity;
                        repository.Find(item).Quantity -= item.Quantity;
                    }
                    else
                    {
                        throw new InvalidOperationException("Update: Out of stock");
                    }
                }
            }
        }
        public void Remove(T item)
        {
            lock (mylock)
            {
                T found;

                found = itemList.FirstOrDefault(i => i.Name == item.Name);

                if (found != null)
                    found.Quantity--;

                if(found.Quantity<=0)
                {
                    itemList.Remove(found);
                }
            }
        }
        public void Clear()
        {
            itemList.Clear();
        }
        public int SubTotal()
        {
            int subTotal = 0;
            foreach(CartItem item in itemList)
            {
                subTotal += item.TotalPrice;
            }
            return subTotal;
        }
    }
}
