using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    // Restaurant Entity (The class that gets stored in the database)
    public class Restaurant
    {
        [Key] // Key is always required
        public int Id { get; set; }
        // More we work with databases, the more we'll need ID's
        [Required]
        public string Name { get; set; }
        [Required]
        public double Rating { get; set; }
        // public bool IsRecommended => Rating > 3.5;
        // ^^ most simpilified way to write property below
        public bool IsRecommended
            // not in migration because there is no setter
        {
            get
            {
                // simplified way to return bool
                return (Rating > 3.5);

                //return (Rating > 3.5) ? true : false;
                // Ternary above is the exact same functionality as below
                //if (Rating >= 3.5)
                //{
                    //return true;
                //}
                //else
                //{
                    //return false;
                //}
            }
        }
    }
}