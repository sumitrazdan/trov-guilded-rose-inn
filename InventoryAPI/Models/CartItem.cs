using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryAPI.Models
{
    /// <summary>
    /// Single Unit of shopping Cart item
    /// </summary>
    public class CartItem : Item
    {
        /// <summary>
        /// Provides the total price of the shopping cart item
        /// based on the Unit Price and Number of units bought
        /// </summary>
        public int TotalPrice
        {
            get { return Price * Quantity;}
        }
    }
}