
using DBMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using PortalLibrary;
using PortalLibrary.Model;

namespace API.Controllers
{
    public class ItemController : ApiController
    {
        EFDBContext db;
        [Route("api/Item/GetAllItemAsync")]
        public async Task<List<ItemViewModel>> GetAllItemAsync()
        {
           
          var itemLst= await ItemHelper.GetItemListAsync();

            return  itemLst;
        }

       
        [HttpPost]
        [Route("api/Item/InsertItemAsyn")]
        public async Task<string> InsertItemAsyn(ItemViewModel model)
        {
            var result = await ItemHelper.InsertItemAsync(model);

            return result;
        }

        [HttpPost]
        [Route("api/Item/UpdateItemAsync")]
        public async Task<string> UpdateItemAsync(ItemViewModel model)
        {
            var result = await ItemHelper.UpdateItemAsync(model);

            return result;
        }

        
        [HttpPost]
        [Route("api/Item/DeleteItemAsync")]
        public async Task<string> DeleteItemAsync(int id)
        {
            var result = await ItemHelper.DeleteItemAsync(id);

            return result;
        }
    }
}
