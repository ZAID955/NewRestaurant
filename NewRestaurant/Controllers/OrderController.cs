using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewRestaurant.Dto.Item;
using NewRestaurant.Dto.Meal;
using NewRestaurant.Dto.order;
using NewRestaurant.Dto.Waiter;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using static NewRestaurant.Helper.Helper;

namespace NewRestaurant.Controllers
{
    [Route("Orderontroller")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region Get Order
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetMeal()
        {
            string query = "SELECT * FROM [dbo].[Order]";
            return Ok(ExecuteQuerycommand(query, null));

        }

        #endregion

        #region CRUD order
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateOrder(CreateOrderDTO Orderdto)
        {
            string query = $"INSERT INTO [dbo].[Order]([waiter_id],[date_time])VALUES(@WaiterId,@datetime)";
            Dictionary<String, object> parco = new Dictionary<String, object>();
            parco.Add("@datetime", Orderdto.Created);
            parco.Add("@WaiterId", Orderdto.WaiterId);
            return Ok(ExecuteNonQuerycommand(query, parco) > 0 ? "Create New Meal Done" : "Failed Adding New Meal");
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateWaiter(UpdateOrderDTO orderdto)
        {
            string query = $"UPDATE [dbo].[Order] SET [waiter_id] = CASE WHEN @WaiterId IS NOT NULL THEN @WaiterId ELSE[waiter_id] END, [date_time] = CASE WHEN @datetime IS NOT NULL THEN @datetime ELSE [date_time] END WHERE [order_id] = @OrderId";
            Dictionary<String, object> paruo = new Dictionary<String, object>(); 
            paruo.Add("@OrderId", orderdto.Id);
            paruo.Add("@WaiterId", orderdto.WaiterId);
            paruo.Add("@datetime", orderdto.Created);
            return Ok(ExecuteNonQuerycommand(query, paruo) > 0 ? "Update  Order Done" : "Failed Update  Order");
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteWaiter(DeleteOrderDTO Orderdto)
        {
            string query = $"DELETE FROM [dbo].[Order] WHERE [order_id] = @OrderId";
            Dictionary<String, object> pardo = new Dictionary<String, object>();
            pardo.Add("@OrderId", Orderdto.Id);
            return Ok(ExecuteNonQuerycommand(query, pardo) > 0 ? "Delete New Waiter Done" : "Failed Delete  Waiter");
        }
        #endregion

        #region Get Order With View

        // Get All Order With View
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetOrderWithView()
        {
            string query = "SELECT * FROM [dbo].[GetOrder]";
            return Ok(ExecuteQuerycommand(query, null));

        }

        // Get last twenty order With View
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetlasttwentyorderWithView()
        {
            string query = "SELECT* FROM[lasttwentyorder]";
            return Ok(ExecuteQuerycommand(query, null));

        }

        // Get Orders filter With DataTime  With View
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetOrdersfilter([FromQuery] OrdersfilterWithDataTimeDto Orderdto)
        {
            string query = "GetOrdersfilter";
            Dictionary<String, object> parof = new Dictionary<String, object>();
            parof.Add("@StartDate", Orderdto.StartDateTime);
            parof.Add("@EndDate", Orderdto.EndtDateTime);
            return Ok(ExecuteQuerycommandWithStoredProcedure(query, parof));
        }
        #endregion

        #region CRUD Order with StoredProcedure
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateOrderwithSP(CreateOrderDTO Orderdto)
        {
            string query = $"CreatetOrder";
            Dictionary<String, object> parcw = new Dictionary<String, object>();
            parcw.Add("@datetime", Orderdto.Created);
            parcw.Add("@WaiterId", Orderdto.WaiterId);
            return Ok(ExecuteNonQueryWithStoredProcedure(query, parcw) > 0 ? "Create New Order Done" : "Failed Adding New Order");
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateOrderwithSP(UpdateOrderDTO Orderdto)
        {
            string query = $"UPDATEOrder";
            Dictionary<String, object> paruw = new Dictionary<String, object>();
            paruw.Add("@OrderId", Orderdto.Id);
            paruw.Add("@datetime", Orderdto.Created);
            paruw.Add("@WaiterId", Orderdto.WaiterId);
            return Ok(ExecuteNonQueryWithStoredProcedure(query, paruw) > 0 ? "Update  Order Done" : "Failed Update Order");
        }
        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteOrderwithSP(DeleteOrderDTO Orderdto)
        {
            string query = $"DeleteOrder";
            Dictionary<String, object> pardw = new Dictionary<String, object>();
            pardw.Add("@OrderId", Orderdto.Id);
            return Ok(ExecuteNonQueryWithStoredProcedure(query, pardw) > 0 ? "Delete New Order Done" : "Failed Delete Order");
        }
        #endregion



    }
}
