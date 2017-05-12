  namespace CapacitacionMVC.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CapacitacionMVC.Entities;

    public class MovieService : IMovieService
    {
        private readonly MoviesContext moviesContext;

        public MovieService()
        {
            this.moviesContext = new MoviesContext();
        }

        public MovieService(MoviesContext moviesContext)
        {
            this.moviesContext = moviesContext;
        }

        public IEnumerable<Movie> GetMovies(Guid? genreIdFilter = null, string nameFilter = null)
        {
            var movies = this.moviesContext.Movies.AsQueryable();

            if (genreIdFilter != null)
            {
                movies = movies.Where(w => w.Genres.Any(a => a.Id == genreIdFilter));
            }

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                movies = movies.Where(w => w.Name.ToLower().Contains(nameFilter.ToLower()));
            }

            return movies.OrderBy(o => o.Name);
        }

        public Movie GetMovieById(Guid id)
        {
            return this.moviesContext.Movies.FirstOrDefault(f => f.Id == id);
        }

        public void AddMovie(Movie movie)
        {
            this.moviesContext.Movies.Add(movie);
            this.moviesContext.SaveChanges();
            //return this.RedirectToAction("Index","MoviesController");
           

            
            
        }

        public void Update(Movie movie)
        {
            this.moviesContext.SaveChanges();
        }

        public void DeleteMovie(Guid id)
        {
            var m = this.GetMovieById(id);
            if (m != null)
            {
                this.moviesContext.Movies.Remove(m);
                this.moviesContext.SaveChanges();
            }
           


        }
    }
}