namespace CapacitacionMVC.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class Movie
    {
        /*Etiquetas disponibles*/
        //  [Required]
        //  [StringLength(...)]
        //  [DisplayName("Algo")]
        //  [Range(30, 300)]

        public Movie()
        {
            this.Genres = new HashSet<Genre>();
        }

        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name="Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "ReleaseDate")]
        public DateTime? ReleaseDate { get; set; }
        [Required]
        [Display(Name = "Plot")]
        public string Plot { get; set; }
        [Required]
        [Display(Name = "CoverLink")]
        public string CoverLink { get; set; }
        [Required]
        [Display(Name = "Runtime")]
        [Range(30,300)]
        public int? Runtime { get; set; }
        // No aparece como requerido [Required]
        [Display(Name = "Genres")]
        //[Required]
        public virtual ICollection<Genre> Genres { get; set; }
    }
}