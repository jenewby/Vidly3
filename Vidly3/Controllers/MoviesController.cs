using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly3.Models;
using Vidly3.ViewModels;
using System;
using System.Collections.Generic;
using Vidly3.Migrations;

namespace Vidly3.Controllers
{
    public class MoviesController : Controller
    {

        // GET: Movies
        //public ActionResult Random()
        //{
        //    Movie movie = new Movie() { Name = "Shrek!"};
        //    List<Customer> customers = new List<Customer>
        //    {
        //        new Customer { Name = "Customer 1"},
        //        new Customer { Name = "Customer 2"}

        //    };
        //    var ViewModel = new RandomMovieViewModel()
        //    {
        //        Movie = movie,
        //        Customers = customers
        //    };
        //    return View(ViewModel);
        //}

        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        public ViewResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");
            
            
                return View("ReadOnlyList");
            
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            ViewBag.Title = "New Movie";
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };
            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit Movie";
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            var viewModel = new MovieFormViewModel(movie)
            {
                //Id = movie.Id,
                //Name = movie.Name,
                //ReleaseDate = movie.ReleaseDate,
                //NumberInStock = movie.NumberInStock,
                //GenreId = movie.GenreId,
                Genres = _context.Genres.ToList()
                
            };
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            //if (ModelState.IsValid)
            //{}

            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                  
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            };
                if (movie.Id == 0)
                {
                   
                    movie.DateAdded = DateTime.Now;
                    movie.NumberAvailable = movie.NumberInStock;

                _context.Movies.Add(movie);
                }
                else
                {
                    var dbMovie = _context.Movies.Single(c => c.Id == movie.Id);
                    dbMovie.Name = movie.Name;
                    dbMovie.ReleaseDate = movie.ReleaseDate;
                    dbMovie.GenreId = movie.GenreId;
                    dbMovie.NumberInStock = movie.NumberInStock;
                }
                _context.SaveChanges();
           
            return RedirectToAction("Index", "Movies");
        }



        [Route("movies/released/{year}/{month:regex(\\d{4}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }



    }
}