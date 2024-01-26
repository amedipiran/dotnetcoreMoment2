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

//Metod för att lägga till film
public IActionResult SaveMovie(Movie movie)
{
    if (string.IsNullOrEmpty(movie.FilmTitle) || string.IsNullOrEmpty(movie.Director))
    {
        // Om något av de obligatoriska fälten är tomt
        ModelState.AddModelError(string.Empty, "Fyll i alla obligatoriska fält.");
        
        var movieList = new List<Movie>();
        var movieListJson = Request.Cookies["MovieList"];

        if (!string.IsNullOrEmpty(movieListJson))
        {
            movieList = JsonConvert.DeserializeObject<List<Movie>>(movieListJson);
        }

        // Återvänd till vyn med befintlig lista och felmeddelanden
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

        // Lägg till den nya filmen i listan
        movieList.Add(movie);

        // Spara listan i cookies
        CookieOptions options = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(1)
        };
        Response.Cookies.Append("MovieList", JsonConvert.SerializeObject(movieList), options);

        return RedirectToAction("Index");
    }

    // Om valideringen misslyckas, återvänd till vyn med samma modell och felmeddelanden
    return View("Index", movie);
}

[HttpPost]
public IActionResult ClearList()
{
    // Rensa hela listan genom att ersätta den med en tom lista
    var movieList = new List<Movie>();
    
    // Spara den tomma listan till cookies
    CookieOptions options = new CookieOptions
    {
        Expires = DateTime.Now.AddDays(1)
    };
    Response.Cookies.Append("MovieList", JsonConvert.SerializeObject(movieList), options);

    return RedirectToAction("Index");
}

//radera enstaka filmer från listan
public IActionResult DeleteMovie(int id)
{
    var movieList = new List<Movie>();
    var movieListJson = Request.Cookies["MovieList"];

    if (!string.IsNullOrEmpty(movieListJson))
    {
        movieList = JsonConvert.DeserializeObject<List<Movie>>(movieListJson);
    }

    // Hitta filmen med det angivna ID och ta bort den från listan
    var movieToRemove = movieList.FirstOrDefault(m => m.Id == id);
    if (movieToRemove != null)
    {
        movieList.Remove(movieToRemove);
        
        // Spara den uppdaterade listan till cookies
        CookieOptions options = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(1)
        };
        Response.Cookies.Append("MovieList", JsonConvert.SerializeObject(movieList), options);
    }

    return RedirectToAction("Index");
}
    }
    
}
