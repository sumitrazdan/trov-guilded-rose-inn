using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryAPI.Models
{
    public class Order
    {
        public int OrderID { get; }
        public IList<OrderLineItem> item { get; set; }

        public int GetTotal()
        {

        }



    }
}