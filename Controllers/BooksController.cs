
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// api/books

[ApiController]
[Route("api/[controller]")]
public class BooksController
{
  

    [HttpGet]
    public String GetBooks() {
        return "Ok";
    }
    
}