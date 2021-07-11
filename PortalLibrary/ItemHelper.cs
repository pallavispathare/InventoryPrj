
using DBMapper;
using DBMapper.Models;
using PortalLibrary.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PortalLibrary.DataMapper;

namespace PortalLibrary
{
    public class ItemHelper
    {
        public static async Task<List<ItemViewModel>> GetItemListAsync()
        {
            EFDBContext db = null;

            List<ItemViewModel> lstSpecialityModel = new List<ItemViewModel>();
            List<Item> lstSpeciality = new List<Item>();

            try
            {
                using (db = new EFDBContext())
                {
                    lstSpeciality = await db.items.OrderBy(x => x.ItemName).ToListAsync();
                    GenericMapper<ItemViewModel, Item> objGenericMapper = new GenericMapper<ItemViewModel, Item>();
                    lstSpecialityModel = objGenericMapper.MapDataList(lstSpeciality);
                }
            }
            catch (Exception ex)
            {
               // Logging.Logging.WriteErrorLog(ex);
            }

            return lstSpecialityModel;
        }

        public async static Task<string> InsertItemAsync(ItemViewModel ItemViewModel)
        {
            EFDBContext db = null;
            try
            {
                using (db = new EFDBContext())
                {
                    GenericMapper<Item, ItemViewModel> objGenericMapper = new GenericMapper<Item, ItemViewModel>();
                    var item = objGenericMapper.MapData(ItemViewModel);
                        db.items.Add(item);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Logging.Logging.WriteErrorLog(ex);
            }
            return "Item Added Successfully";
        }

        public async static Task<string> UpdateItemAsync(ItemViewModel item)
        {
            EFDBContext db = null;
            try
            {
                using (db = new EFDBContext())
                {
                    Item EditItem = await db.items.FindAsync(item.ID);
                    EditItem.ItemName = item.ItemName;
                    EditItem.ItemDescription = item.ItemDescription;
                    EditItem.ItemPrice = item.ItemPrice;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Logging.Logging.WriteErrorLog(ex);
            }
            return "Item Updated Successfully";
        }

        public static async Task<string> DeleteItemAsync(int id)
        {
            EFDBContext db = null;
            try
            {
                using (db = new EFDBContext())
                {
                    Item DeleteItem = await db.items.FindAsync(id);
                    db.items.Remove(DeleteItem);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Logging.Logging.WriteErrorLog(ex);
            }
            return "Item Deleted Successfully";
        }


    }
}
