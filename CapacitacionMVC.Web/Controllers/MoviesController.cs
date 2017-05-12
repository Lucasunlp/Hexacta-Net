namespace CapacitacionMVC.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using CapacitacionMVC.Data;
    using CapacitacionMVC.Entities;
    using CapacitacionMVC.Web.Models;
    using System.Web.Routing;

    public class MoviesController : Controller
    {
        private readonly MovieService movieService;
        private readonly GenreService genreService;

        public MoviesController()
        {
            var context = new MoviesContext();
            this.movieService = new MovieService(context);
            this.genreService = new GenreService(context);
        }

        public ActionResult Index(Guid? genreId = null, string searchText = null)
        {
            return this.View(new MoviesIndexModel { Movies = this.movieService.GetMovies(genreId, searchText), SearchText = searchText, GenreId = genreId });
        }

        public ActionResult Details(Guid id)
        {
            var movie = this.movieService.GetMovieById(id);

            return this.View(new MoviesDetailsModel { Movie = movie });
        }

      

        [HttpPost]
        public ActionResult Create(MoviesCreateModel movieVM, IEnumerable<Guid> selectedGenres)
        {
            var movie = new Movie();
            movie.Id = Guid.NewGuid();
            if (selectedGenres != null)
            {
                foreach (Guid g in selectedGenres)
                {

                    var gen = this.genreService.GetGenreById(g);


                    movie.Genres.Add(gen);

                }

            }
            if (this.ModelState.IsValid)
            {
                movie.Name = movieVM.Name;
                movie.Plot = movieVM.Plot;
                movie.ReleaseDate = movieVM.ReleaseDate;
                movie.Runtime = movieVM.Runtime;
                movie.CoverLink = movieVM.CoverLink;
                //movie.Genres = movieVM.selectedGenres;
                


                
              this.movieService.AddMovie(movie);
              TempData["Aviso"] = "Se ha agregado exitosamente la pelicula: " + movie.Name;
                // Esto va aca o en el service db.SaveChanges();
            }
            ViewData["success"] = "Error ";
            //return this.View(new MoviesCreateModel() { ViewAction = ViewAction.Create, Movie = movie });
            
            
            return this.RedirectToAction("Index", "Movies");

        }


        public ActionResult Create()
        {


            return this.View(new MoviesCreateModel()
            {
                ViewAction = ViewAction.Create,
                Movie = new Movie(),
                Genres = this.genreService.GetGenres()
            });
        }
       
        public ActionResult Edit(Guid id)
        {

            {
                var movie = this.movieService.GetMovieById(id);

                return this.View("Create", new MoviesCreateModel() { ViewAction = ViewAction.Edit,
                    Movie = new Movie(),movie = movie, Genres = this.genreService.GetGenres() });
            }
        }

        [HttpPost]
        public ActionResult Edit(Movie movie, IEnumerable<Guid> selectedGenres)
        {
            if (this.ModelState.IsValid)
            {
                var movieDb = this.movieService.GetMovieById(movie.Id);

                movieDb.Name = movie.Name;
                movieDb.ReleaseDate = movie.ReleaseDate;
                movieDb.Runtime = movie.Runtime;
                movieDb.CoverLink = movie.CoverLink;
                movieDb.Plot = movie.Plot;

                this.movieService.Update(movie);

                this.TempData["successmessage"] = "Se ha actualizado exitosamente la pelicula: " + movie.Name;

                return this.RedirectToAction("Index");
            }
            else
            {
                return this.View("Create", new MoviesCreateModel() { ViewAction = ViewAction.Edit, Movie = movie });
            }
        }

        public ActionResult Delete(Guid id)
        {
            this.movieService.DeleteMovie(id);
            this.TempData["successmessage"] = "Se ha eliminado exitosamente la pelicula ";
            return this.RedirectToAction("Index");
            //return this.View(new MoviesIndexModel { Movies = this.movieService.GetMovies() });

        }
    }
}