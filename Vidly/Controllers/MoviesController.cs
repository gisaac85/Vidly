using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Migrations;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

        public ViewResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel
            {
                Movies = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }


        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);

        }

        [HttpPost]
        public ActionResult Save(MovieFormViewModel movie)
        {
            if (movie.Movies.Id == 0)
            {
                movie.Movies.DateAdded = DateTime.Now;
                _context.Movies.Add(movie.Movies);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Movies.Id);
                movieInDb.Name = movie.Movies.Name;
                movieInDb.GenreId = movie.Movies.GenreId;
                movieInDb.NumberInStock = movie.Movies.NumberInStock;
                movieInDb.ReleaseDate = movie.Movies.ReleaseDate;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }
}