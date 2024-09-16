using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace testApi.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloWorldController : ControllerBase
{
    public readonly MovieContext context;
    public HelloWorldController(MovieContext _ctx)
    {
        context=_ctx;
    }

    [HttpGet(Name = "GetHelloWorldMessage")]
    public string GetHelloWorldMessage()
    {
        return "Hello World!";
    }

    [HttpPost]
    [Route ("/postMovie")]
    public async Task<ActionResult<Movie>> PostMovie(){
        var movie=new Movie();
        movie.Genre="Drama";
        movie.Title="Title 1";
        movie.ReleaseDate =DateTime.Now.ToUniversalTime();

        context.Movies.Add(movie);
        await context.SaveChangesAsync();

        return Ok(movie);
    }

    [HttpGet]
    [Route("/getMovies")]

    public async Task<ActionResult<List<Movie>>> GetMovies(){

        return Ok(await context.Movies.ToListAsync());
    }

}
