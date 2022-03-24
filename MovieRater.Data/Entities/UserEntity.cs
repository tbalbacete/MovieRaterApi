using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MovieRater.Data.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id {get; set;}

        [Required( ErrorMessage = "Please enter an e-mail address,")]
        [EmailAddress]
        public string Email {get; set;}

        [Required(ErrorMessage = "Please enter a username")]
        public string Username {get; set;}

        [Required(ErrorMessage = "Please enter a password")]
        public string Password {get; set;}

        [Required]
        public DateTime DateCreated {get; set;}
        public List<MovieEntity> Movies { get; set; }
        public List<ShowEntity> Shows {get; set;}
    }
}