﻿using RestaurantRaterAPI.Models;
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
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDBContext _context = new RestaurantDBContext();
        
        // CREATE (POST)
        public async Task<IHttpActionResult> PostRestaurant(Restaurant model)
        {
            if (model == null)
            {
                return BadRequest("Your request body cannot be empty.");
            }
            // check to see if valid model based on criteria in restaurant class
            if (ModelState.IsValid)
            {
                // save it to DBContext
                _context.Restaurants.Add(model);
                // save all changes to DB
                await _context.SaveChangesAsync();

                // return 200 response
                return Ok("You created a restaurant and it was saved!");
            }

            // if model is not valid, return bad request
            return BadRequest(ModelState);
        }

        // READ (GET)
        // Get by ID
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant != null)
            {
                return Ok(restaurant);
            }

            return NotFound();
        }

        // Get All
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
        }

        // UPDATE (PUT)

        // DELETE (DELETE)
    }
}
