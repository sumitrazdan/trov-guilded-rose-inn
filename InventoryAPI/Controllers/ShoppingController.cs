using InventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace InventoryAPI.Controllers
{
    [RoutePrefix("api/Shop")]
    public class ShoppingController : ApiController
    {
        ICart<CartItem> shopCart;

        public ShoppingController()
        {
            shopCart = new Cart<CartItem>();
        }
        [HttpPost]
        [Route("Order")]
        public string Order([FromBody]CartItem product)
        {
            shopCart.Add(new CartItem { Name = product.Name, Quantity = product.Quantity });

            JavaScriptSerializer json = new JavaScriptSerializer();
            return json.Serialize(shopCart.GetItem());
        }
    }
}
