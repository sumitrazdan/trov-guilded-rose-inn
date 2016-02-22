using InventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace InventoryAPI.Controllers
{
    [RoutePrefix("api/Inventory")]
    public class InventoryController : ApiController
    {
        IRepository<Item> repo;
        public InventoryController()
        {
            repo = new MemoryRepository<Item>();
            if (repo.Get().Count() <= 0)
                repo = MemoryRepository<Item>.Factory();
        }

        [HttpGet]
        [Route("Get")]
        public string GetInventory()
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            return json.Serialize(repo.Get());
        }

    }
}
