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
        // Primary Key
        // This key is foreign to Rating class Key
        [Key] // Key is always required
        public int Id { get; set; }
        // More we work with databases, the more we'll need ID's
        [Required]
        public string Name { get; set; }
        [Required]
        public double Rating
        {
            get
            {
                // Calculate a total average score based on Ratings
                double totalAverageRating = 0;

                foreach (var rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                    // += above is doing the same as comment below
                    // totalAverageRating = totalAverageRating + rating.AverageRating;
                }

                // ideal situation, but could also be divided by zero
                // return totalAverageRating / Ratings.Count;

                // ternary takes above and allows us to check if count is greater than 0 and return average, if not return 0
                return (Ratings.Count > 0) ? Math.Round(totalAverageRating / Ratings.Count, 2) : 0;
            }
        }

        // Average Food Rating
        [Required]
        public double FoodRating
        {
            get
            {
                double totalAverageRating = 0;

                foreach (var foodRating in Ratings)
                {
                    totalAverageRating += foodRating.FoodScore;
                }

                return (Ratings.Count > 0) ? Math.Round(totalAverageRating / Ratings.Count, 2) : 0;
            }
        }

        // Average Enironment Rating
        [Required]
        public double EnvironmentRating
        {
            get
            {
                // does the same thing as the foreach loop in the method above
                // scores is actually an IEnumerable(which is like a list or collection)
                var scores = Ratings.Select(rating => rating.EnvironmentScore);

                double totalAverageRating = scores.Sum();

                return (Ratings.Count > 0) ? Math.Round(totalAverageRating / Ratings.Count, 2) : 0;
            }
        }

        // Average Cleanliness Rating
        [Required]
        public double CleanlinessRating
        {
            get
            {
                // Ternary of the foreach loop to show the most simplified way to write the code in one line instead of 2 steps and multiple lines of code
                return (Ratings.Count > 0) ? Math.Round(Ratings.Select(r => r.CleanlinessScore).Sum() / Ratings.Count, 2) : 0;
            }
        }

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

        // All of the associated Rating objects from the database
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
    }
}