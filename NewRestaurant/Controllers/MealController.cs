using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [Route("MealController")]
    [ApiController]
    public class MealController : ControllerBase
    {
        #region Get Meal
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetMeal()
        {
            string query = "SELECT * FROM Meal";
            return Ok(ExecuteQuerycommand(query, null));
        }

         #endregion

        #region CRUD Meal
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateMeal(CreateMealDTO Mealdto)
        {
            string query = $"INSERT INTO [dbo].[Meal] ([name],[description],[price])VALUES(@name,@description,@price)";
            Dictionary<String, object> parcm = new Dictionary<String, object>();
            parcm.Add("@name", Mealdto.name);
            parcm.Add("@description", Mealdto.description);
            parcm.Add("@price", Mealdto.price);
            return Ok(ExecuteNonQuerycommand(query, parcm) > 0 ? "Create New Meal Done" : "Failed Adding New Meal");
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateMeal(UpdateMealDTO Mealdto)
        {
            string query = $"UPDATE [dbo].[Meal] SET [name] = CASE WHEN @nameMeal IS NOT NULL THEN @nameMeal ELSE [name] END,[description] = CASE WHEN @descMeal IS NOT NULL THEN @descMeal ELSE [description] END,[price] = CASE WHEN @price IS NOT NULL THEN @price ELSE [price] END WHERE [Meal_id] = @Mealid";
            Dictionary<String, object> parum = new Dictionary<String, object>();
            parum.Add("@Mealid", Mealdto.Id);
            parum.Add("@nameMeal", Mealdto.Name);
            parum.Add("@descMeal", Mealdto.Description);
            parum.Add("@price", Mealdto.Price);
            return Ok(ExecuteNonQuerycommand(query, parum) > 0 ? "Update  Meal Done" : "Failed Update  Meal");
        }
        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteMeal(DeleteMealDTO Mealdto)
        {
            string query = $"DELETE FROM [dbo].[Meal] WHERE [Meal_id] = @Id";
            Dictionary<String, object> pardm = new Dictionary<String, object>();
            pardm.Add("@Id", Mealdto.Id);
            return Ok(ExecuteNonQuerycommand(query, pardm) > 0 ? "Delete New Meal Done" : "Failed Delete  Meal");
        }
        #endregion

        #region Get Meal With View
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetMealWithView()
        {
            string query = "SELECT * FROM [dbo].[GetMeal]";
            return Ok(ExecuteQuerycommand(query, null));
        }

        #endregion

        #region CRUD Meal with StoredProcedure
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateMealwithSP(CreateMealDTO Mealdto)
        {
            string query = $"CreatetMeal";
            Dictionary<String, object> parcm = new Dictionary<String, object>();
            parcm.Add("@MealName", @Mealdto.name);
            parcm.Add("@Mealdescription", @Mealdto.description);
            parcm.Add("@Price", @Mealdto.price);
            return Ok(ExecuteNonQueryWithStoredProcedure(query, parcm) > 0 ? "Create New Meal Done" : "Failed Adding New Meal");
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateMealwithSP(UpdateMealDTO Mealdto)
        {
            string query = $"UPDATEMeal";
            Dictionary<String, object> parum = new Dictionary<String, object>();
            parum.Add("@Mealid", @Mealdto.Id);
            parum.Add("@nameMeal", @Mealdto.Name);
            parum.Add("@descMeal", @Mealdto.Description);
            parum.Add("@price", @Mealdto.Price);
            return Ok(ExecuteNonQueryWithStoredProcedure(query, parum) > 0 ? "Update  Meal Done" : "Failed Update  Meal");
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteMealwithSP(DeleteMealDTO Mealdto)
        {
            string query = $"DeleteMeal";
            Dictionary<String, object> pardm = new Dictionary<String, object>();
            pardm.Add("@MealID", Mealdto.Id);
            return Ok(ExecuteNonQueryWithStoredProcedure(query, pardm) > 0 ? "Delete New Meal Done" : "Failed Delete  Meal");
        }
        #endregion
    }
}
