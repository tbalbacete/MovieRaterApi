using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRater.Models.Rating
{
    public class RatingCreate
    {
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between {1} and {2}")]
        public int Rating {get; set;}

        public int MovieId {get; set;}
        public int ShowId {get; set;}
    }
}