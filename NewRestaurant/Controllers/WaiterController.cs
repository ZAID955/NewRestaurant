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
    [Route("WaiterController")]
    [ApiController]
    public class WaiterController : ControllerBase
    {
        #region Get Waiter
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetMeal()
        {
            string query = "SELECT * FROM Waiter";
            return Ok(ExecuteQuerycommand(query, null));

        }

        #endregion

        #region CRUD Waiter
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateWaiter(CreateWaiterDTO Waiterdto)
        {
            string query = $"INSERT INTO [dbo].[Waiter]([name],[email],[password])VALUES(@name,@email,@password)";
            Dictionary<String, object> parcw = new Dictionary<String, object>();
            parcw.Add("@name", Waiterdto.name);
            parcw.Add("@email", Waiterdto.email);
            parcw.Add("@password", Waiterdto.password);
            return Ok(ExecuteNonQuerycommand(query, parcw) > 0 ? "Create New Waiter Done" : "Failed Adding New Waiter");
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateWaiter(UpdateWaiterDTO Waiterdto)
        {
            string query = $"UPDATE [dbo].[Waiter] SET [name] = CASE WHEN @nameWaiter IS NOT NULL THEN @nameWaiter ELSE [name] END,[email] = CASE WHEN @emailWaiter IS NOT NULL THEN @emailWaiter ELSE [email] END,[password] = CASE WHEN @passwordWaiter IS NOT NULL THEN @passwordWaiter ELSE [password] END WHERE [waiter_id] = @Waiterid";
            Dictionary<String, object> paruw = new Dictionary<String, object>();
            paruw.Add("@Waiterid", Waiterdto.Id);
            paruw.Add("@nameWaiter", Waiterdto.Name);
            paruw.Add("@emailWaiter", Waiterdto.Email);
            paruw.Add("@passwordWaiter", Waiterdto.Password);
            return Ok(ExecuteNonQuerycommand(query, paruw) > 0 ? "Update  Waiter Done" : "Failed Update  Waiter");
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteWaiter(DeleteWaiterDTO Waiterdto)
        {
            string query = $"DELETE FROM [dbo].[Waiter] WHERE [Waiter_id] = @Id";
            Dictionary<String, object> pardw = new Dictionary<String, object>();
            pardw.Add("@Id", Waiterdto.Id);
            return Ok(ExecuteNonQuerycommand(query, pardw) > 0 ? "Delete New Waiter Done" : "Failed Delete  Waiter");
        }
        #endregion

        #region Get Waiter With View
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetWaiterWithView()
        {
            string query = "SELECT* FROM[dbo].[GetWaiter]";
            return Ok(ExecuteQuerycommand(query, null));

        }

        #endregion

        #region CRUD Waiter with StoredProcedure
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateWaiterwithSP(CreateWaiterDTO Waiterdto)
        {
            string query = $"CreatetWaiter";
            Dictionary<String, object> parcw = new Dictionary<String, object>();
            parcw.Add("@WaiterName", @Waiterdto.name);
            parcw.Add("@Waiteremail", @Waiterdto.email);
            parcw.Add("@Waiterpassword", @Waiterdto.password);
            return Ok(ExecuteNonQueryWithStoredProcedure(query, parcw) > 0 ? "Create New Waiter Done" : "Failed Adding New Waiter");
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateWaiterwithSP(UpdateWaiterDTO Waiterdto)
        {
            string query = $"UPDATEWaiter";
            Dictionary<String, object> paruw = new Dictionary<String, object>();
            paruw.Add("@Waiterid", @Waiterdto.Id);
            paruw.Add("@nameWaiter", @Waiterdto.Name);
            paruw.Add("@emailWaiter", @Waiterdto.Email);
            paruw.Add("@passwordWaiter", @Waiterdto.Password);
            return Ok(ExecuteNonQueryWithStoredProcedure(query, paruw) > 0 ? "Update  Waiter Done" : "Failed Update  Waiter");
        }
        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteWaiterwithSP(DeleteWaiterDTO Waiterdto)
        {
            string query = $"DeleteWaiter";
            Dictionary<String, object> pardw = new Dictionary<String, object>();
            pardw.Add("@WaiterID", @Waiterdto.Id);
            return Ok(ExecuteNonQueryWithStoredProcedure(query, pardw) > 0 ? "Delete New Waiter Done" : "Failed Delete  Waiter");
        }
        #endregion
    }
}
