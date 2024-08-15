
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// api/books

[ApiController]
[Route("api/[controller]")]
public class BooksController:ControllerBase
{
  

    [HttpGet]
    public IActionResult GetBooks() {
        return Ok();
    }
    
}