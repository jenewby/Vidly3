using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly3.Models;
using Vidly3.Models.Dtos;
using Vidly3.ViewModels;

namespace Vidly3.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        ////GET api/rental
        //public IHttpActionResult Index()
        //{
        //    var customers = _context.Customers.Include(c => c.MembershipType).ToList();
        //    var movies = _context.Movies.Include(m => m.Genre).ToList();
        //    var viewModel = new RentalFormViewModel();

        //    return Ok(customers, movies);
        //}

        //POST api/NewRentals
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {

            var customer = _context.Customers.Single(c => c.Id == newRental.CustomerId);
         
            var movies = _context.Movies.Where(m=> newRental.MovieIds.Contains(m.Id)).ToList();

            if (movies.Count != newRental.MovieIds.Count)
                return BadRequest("One or more movies was not loaded");
            //this above section basically just makes sure that all movies in the rental list are equal to movies we found ids for, else one or more movies may not have been loaded

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available");
                movie.NumberAvailable--;

                var rental = new Rental()
                {
                DateRented = DateTime.Now,
                Customer = customer,
                Movie = movie
                };

                
                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();
            return Ok();


        }
    }
}
