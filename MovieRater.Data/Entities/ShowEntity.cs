using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRater.Data.Entities
{
    public class ShowEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Owner))]
        public int OwnerId { get; set; }
        public UserEntity Owner {get; set;}

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }

        public RatingEntity Rating {get; set;}
    }
}