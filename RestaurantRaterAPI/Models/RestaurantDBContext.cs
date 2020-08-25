using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class RestaurantDBContext : DbContext
    {
        public RestaurantDBContext() : base("DefaultConnection") { }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}