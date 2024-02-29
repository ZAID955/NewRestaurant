using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewRestaurant.Dto.Item;
using NewRestaurant.Dto.Meal;
using NewRestaurant.Dto.Waiter;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using static NewRestaurant.Helper.Helper;

namespace NewRestaurant.Controllers
{
    [Route("ItemController")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        #region Get Item
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetItem()
        {
            string query = "SELECT * FROM Item";
            return Ok(ExecuteQuerycommand(query, null));
        }

        #endregion

        #region CRUD Item
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateItem(CreateItemDTO Itemdto)
        {
            string query = "INSERT INTO [dbo].[Item] ([name],[description],[price],[CATAGORY])VALUES(@name,@description,@price,@catagory)";
            Dictionary<String, object> parci = new Dictionary<String, object>();
            parci.Add("@name", Itemdto.name);
            parci.Add("@description", Itemdto.description);
            parci.Add("@price", Itemdto.price);
            parci.Add("@catagory", Itemdto.catagory);
            return Ok(ExecuteNonQuerycommand(query, parci) > 0 ? "Create New Item Done" : "Failed Adding New Item");
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateItem(UpdateItemDTO Itemdto)
        {
            string query = $"UPDATE [dbo].[Item] SET [name] = CASE WHEN @nameItem IS NOT NULL THEN @nameItem ELSE [name] END,[description] = CASE WHEN @descItem IS NOT NULL THEN @descItem ELSE [description] END,[price] = CASE WHEN @price IS NOT NULL THEN @price ELSE [price] END,[CATAGORY] = CASE WHEN @catagoryItem IS NOT NULL THEN @catagoryItem ELSE [CATAGORY] END WHERE [item_id] = @Itemid";
            Dictionary<String, object> parui = new Dictionary<String, object>();
            parui.Add("@Itemid", Itemdto.Id);
            parui.Add("@nameItem", Itemdto.Name);
            parui.Add("@descItem", Itemdto.Description);
            parui.Add("@price", Itemdto.Price);
            parui.Add("@catagoryItem", Itemdto.Catagory);
            return Ok(ExecuteNonQuerycommand(query, parui) > 0 ? "Update  Item Done" : "Failed Update  Item");
        }


        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteItem(DeleteItemDTO Itemdto)
        {
            string query = $"DELETE FROM [dbo].[Item] WHERE [item_id] = @Id";
            Dictionary<String, object> pardi = new Dictionary<String, object>();
            pardi.Add("@Id", Itemdto.Id);
            return Ok(ExecuteNonQuerycommand(query, pardi) > 0 ? "Delete New Item Done" : "Failed Delete  Item");
        }
        #endregion

        #region Get Item With View
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetItemWithView()
        {
            string query = "SELECT * FROM [dbo].[GetItem]";
            return Ok(ExecuteQuerycommand(query, null));
        }
        #endregion

        #region CRUD Item with StoredProcedure
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateItemwithSP(CreateItemDTO Itemdto)
        {
            string query = $"CreatetItem";
            Dictionary<String, object> parci = new Dictionary<String, object>();
            parci.Add("@ItemName", @Itemdto.name);
            parci.Add("@Itemdescription", @Itemdto.description);
            parci.Add("@Price", @Itemdto.price);
            parci.Add("@ItemCATAGORY", @Itemdto.catagory);
            return Ok(ExecuteNonQueryWithStoredProcedure(query, parci) > 0 ? "Create New Item Done" : "Failed Adding New Item");
        }
        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateItemstowithSP(UpdateItemDTO Itemdto)
        {
            string query = $"UPDATEItem";
            Dictionary<String, object> paruisd = new Dictionary<String, object>();
            paruisd.Add("@Itemid", @Itemdto.Id);
            paruisd.Add("@nameItem", @Itemdto.Name);
            paruisd.Add("@descItem", @Itemdto.Description);
            paruisd.Add("@price", @Itemdto.Price);
            paruisd.Add("@catagoryItem", @Itemdto.Catagory);
            return Ok(ExecuteNonQueryWithStoredProcedure(query, paruisd) > 0 ? "Update  Item Done" : "Failed Update  Item");
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteItemwithSP(DeleteItemDTO Itemdto)
        {
            string query = $"DeleteItem";
            Dictionary<String, object> pardi = new Dictionary<String, object>();
            pardi.Add("@ItemID", @Itemdto.Id);
            return Ok(ExecuteNonQueryWithStoredProcedure(query, pardi) > 0 ? "Delete New Item Done" : "Failed Delete  Item");
        }
        #endregion
    }
}
