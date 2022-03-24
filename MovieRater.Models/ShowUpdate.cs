using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRater.Models
{
    public class ShowUpdate
    {
        [Required]
        public int Id { get; set; }
        [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength(100, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string Title { get; set; }
        [Required]
        [MaxLength(9999, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string Description { get; set; }
    }
}