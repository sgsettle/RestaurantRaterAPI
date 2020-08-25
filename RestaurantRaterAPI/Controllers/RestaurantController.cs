using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RestaurantController : ApiController
    {
        RestaurantDBContext _context = new RestaurantDBContext();
        
        // CREATE (POST)
        public async Task<IHttpActionResult> PostRestaurant(Restaurant model)
        {
            // check to see if valid model based on criteria in restaurant class
            if (ModelState.IsValid)
            {
                // save it to DBContext
                _context.Restaurants.Add(model);
                // save all changes to DB
                await _context.SaveChangesAsync();

                // return 200 response
                return Ok();
            }

            // if model is not valid, return bad request
            return BadRequest(ModelState);
        }

        // READ (GET)
        // Get by ID

        // Get All

        // UPDATE (PUT)

        // DELETE (DELETE)
    }
}
