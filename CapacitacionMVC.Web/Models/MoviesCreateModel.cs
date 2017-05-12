namespace CapacitacionMVC.Web.Models
{

    using CapacitacionMVC.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Http;

    public class MoviesCreateModel
    {
            public ViewAction ViewAction { get; set; }
                public Movie movie;
                //public Genre genre;
       /*Completar con las properties que faltan*/

            public Movie Movie { get; set; }

           // public Genre Genres { get; set; }

           

            

            [Required]
            public string Name { get; set; }
            [Required]
            public DateTime? ReleaseDate { get; set; }
            [Required]
            public string Plot { get; set; }
            [Required]
            public string CoverLink { get; set; }
            [Required]
            public int? Runtime { get; set; }
            //[Required]
            public IEnumerable<Genre> Genres { get; set; }

            //public IEnumerable<Guid> selectedGenres { get; set; }
            
            
    }
}