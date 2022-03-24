using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRater.Data.Entities
{
    public class RatingEntity
    {
        [Key]
        public int Id {get; set;}

        [ForeignKey(nameof(Movie))]

        public int? MovieId {get; set;}
        public MovieEntity Movie {get; set;}


        [ForeignKey(nameof(Show))]
        public int? ShowId {get; set;}
        public ShowEntity Show {get; set;}


        [Required]
        public int Rating {get; set;}

        [Required] 
        public DateTimeOffset CreatedUtc {get; set;}
        public DateTimeOffset? ModifiedUtc {get; set;}
                
    }
}