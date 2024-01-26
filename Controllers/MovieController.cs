using Microsoft.AspNetCore.Mvc;
using Moment_2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Moment_2.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
                ViewBag.Message = "Välkommen till filmsidan!";
                ViewData["Greeting"] = "Här kan du spara dina favoritfilmer.";
            var movieList = new List<Movie>();
            //Hämtar cookies
            var movieListJson = Request.Cookies["MovieList"];

            if (!string.IsNullOrEmpty(movieListJson))
            {
                movieList = JsonConvert.DeserializeObject<List<Movie>>(movieListJson);
            }

            return View(movieList);
        }

        [HttpPost]

public IActionResult SaveMovie(Movie movie)

{


    if (string.IsNullOrEmpty(movie.Title) || string.IsNullOrEmpty(movie.Director))
    {
        // Om något av fälten är tomt
        ModelState.AddModelError(string.Empty, "Fyll i alla fält.");
        
        var movieList = new List<Movie>();
        var movieListJson = Request.Cookies["MovieList"];

        if (!string.IsNullOrEmpty(movieListJson))
        {
            movieList = JsonConvert.DeserializeObject<List<Movie>>(movieListJson);
        }

        // Återvänd till vyn med samma modell (befintlig lista) för att visa felmeddelandet
        return View("Index", movieList);
    }

    if (ModelState.IsValid)
    {
        var movieList = new List<Movie>();
        var movieListJson = Request.Cookies["MovieList"];

        if (!string.IsNullOrEmpty(movieListJson))
        {
            movieList = JsonConvert.DeserializeObject<List<Movie>>(movieListJson);
        }

        movieList.Add(movie);

        CookieOptions options = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(1)
        };
        Response.Cookies.Append("MovieList", JsonConvert.SerializeObject(movieList), options);

        return RedirectToAction("Index");
    }

    // Om valideringen misslyckas, återvänd till vyn med samma modell
    return View("Index", new List<Movie>());
}



    }
}
