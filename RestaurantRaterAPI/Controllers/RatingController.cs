using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RatingController : ApiController
    {
        private readonly RestaurantDBContext _context = new RestaurantDBContext();

        // Create new ratings
        [HttpPost]
        public async Task<IHttpActionResult> CreateRating(Rating model)
        {
            // Check to make sure model isn't empty
            if (model == null)
                return BadRequest("Your request cannot be empty.");

            // Check to see if the model is NOT valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Find the targeted restaurant 
            // Checking to see if restaurant exists to create rating
            var restaurant = await _context.Restaurants.FindAsync(model.RestaurantId);
            if (restaurant == null)
                return BadRequest($"The target restaurant with the ID of {model.RestaurantId} does not exist.");

            // The restaurant isn't null, so we can successfully rate it
            _context.Ratings.Add(model);

            if (await _context.SaveChangesAsync() == 1)
                return Ok($"You rated {restaurant.Name} successfully!");

            // Means something went wrong with the saving, don't think it's the users fault
            return InternalServerError();
        }

        // Get a rating by its ID?
        [Route("api/Rating/RatingById")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Rating rating = await _context.Ratings.FindAsync(id);

            if (rating != null)
            {
                return Ok(rating);
            }

            return NotFound();
        }

        // Get ALL ratings for a specific restaurant by the restaurant ID
        [HttpGet]
        public async Task<IHttpActionResult> GetAllRatingsByRestaurantId(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            //List<Rating> ratings = await _context.Ratings.ToListAsync();

            if (restaurant != null)
            {
                return Ok(restaurant);
            }

            return NotFound();
        }

        // Update Rating
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRating([FromUri] int id, [FromBody] Rating updatedRating)
        {
            if (ModelState.IsValid)
            {
                Rating rating = await _context.Ratings.FindAsync(id);

                if (rating != null)
                {
                    rating.FoodScore = updatedRating.FoodScore;
                    rating.EnvironmentScore = updatedRating.EnvironmentScore;
                    rating.CleanlinessScore = updatedRating.CleanlinessScore;

                    await _context.SaveChangesAsync();

                    return Ok("Rating has been updated.");
                }

                return NotFound();
            }

            return BadRequest(ModelState);
        }

        // Delete rating
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRatingById(int id)
        {
            Rating entity = await _context.Ratings.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(entity);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("The rating was deleted.");
            }

            return InternalServerError();
        }
    }
}
