using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class Rating
    {
        // Primary Key
        [Key]
        public int Id { get; set; }

        // Foreign Key (Restaurant Key)
        // Restaurant Id will match Id from Restaurant table
        // Need to have 1. Key 2. Navigation property
        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }
        // Foreign Key Navigation Property
        // Virtual means we can override functionality 
        // tells that they're connected
        public virtual Restaurant Restaurant { get; set; }

        [Required]
        [Range(0, 10)]
        public double FoodScore { get; set; }

        // Can put attributes next to each other instead of one on top of the other
        [Required, Range(0, 10)]
        public double EnvironmentScore { get; set; }

        [Required]
        [Range(0, 10)]
        public double CleanlinessScore { get; set; }

        // Add all scores and get the average out of 10
        public double AverageRating
        {
            get
            {
                var totalScore = FoodScore + EnvironmentScore + CleanlinessScore;
                return totalScore / 3;
            }
        }
    }
}