
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// api/books

[ApiController]
[Route("api/[controller]")]
public class BooksController:ControllerBase
{
  private readonly BookService _bookService;

  public BooksController(BookService bookService)
  {
    _bookService = bookService;
  }

    [HttpGet]
    public async Task<IActionResult> GetAsync() {

        var response = await _bookService.GetAsync();
        return Ok(response); // 200 result
    }

    // api/books/filters
    [HttpGet("filters")]
    public async Task<IActionResult> GetFiltersAsync(){
     var response =  await _bookService.GetFiltersAsync();
     return Ok(response);
    }

     [HttpGet("aggregates")]
    public async Task<IActionResult> GetAggregatesAsync(){
     var response =  await _bookService.GetAggregatesAsync();
     return Ok(response);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] string id) {

        var response = await _bookService.GetAsyncById(id);
        return Ok(response); // 200 result
    }


    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] BookDto request){
       var bookDto = await  _bookService.CreateAsync(request);
       return Created($"/api/books/{bookDto.Id}",bookDto); // 201 Result
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute] string id, [FromBody] BookDto request){
        await _bookService.UpdateAsync(id,request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id){
      await _bookService.DeleteAsync(id);
      return NoContent();
    }
    
}