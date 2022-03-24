using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRater.Models.Show
{
    public class ShowListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}