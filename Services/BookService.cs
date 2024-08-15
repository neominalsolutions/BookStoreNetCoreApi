using MongoDB.Bson;
using MongoDB.Driver;

public class BookService
{

  private readonly IMongoCollection<Book> _bookCollection;

  public BookService(IMongoClient mongoClient)
  {
    // ilgili veri tabanı
     var database = mongoClient.GetDatabase("booksDb");
     // koleksiyona bağlantı
     _bookCollection = database.GetCollection<Book>("books");
  }

  public async Task<List<BookDto>> GetAsync() {
    var books = await _bookCollection.Find(x=> true).Skip(0).Limit(10).ToListAsync();
    var booksDto = books.Select(book=> ToDto(book)).ToList();
    return booksDto;
  }

  private BookDto ToDto(Book book) {
    return new BookDto {
        Title = book.Title,
        Description = book.Description,
        Gender = book.Gender,
        NumPages = book.NumPages,
        Editorial = book.Editorial,
        Isbn = book.Isbn,
        YearEdition = book.YearEdition,
        DateEdition = book.DateEdition,
        Writer = book.Writer,
        Image = book.Image,
        Tags = book.Tags,
        CategoryIds = book.CategoryIds.Select(a=> a.ToString()).ToList()
    };
  }

   private Book ToModel(BookDto book) {
    return new Book {
        Title = book.Title,
        Description = book.Description,
        Gender = book.Gender,
        NumPages = book.NumPages,
        Editorial = book.Editorial,
        Isbn = book.Isbn,
        YearEdition = book.YearEdition,
        DateEdition = book.DateEdition,
        Writer = book.Writer,
        Image = book.Image,
        Tags = book.Tags,
        CategoryIds = book.CategoryIds.Select(a=> new ObjectId(a)).ToList()
    };
  }

    
}