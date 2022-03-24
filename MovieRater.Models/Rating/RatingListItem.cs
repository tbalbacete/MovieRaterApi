using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRater.Models.Rating
{
    public class RatingListItem
    {
        public int Id {get; set;}
        public MovieEntity Movie {get; set;}
        public ShowEntity Show {get; set;}
        public DateTimeOffset CreatedUtc{get; set;}
    }
}