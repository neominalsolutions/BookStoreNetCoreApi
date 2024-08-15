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

  public async Task<List<BookDto>> GetAsync()
  {
    var books = await _bookCollection.Find(x => true).Skip(0).Limit(10).ToListAsync();
    var booksDto = books.Select(book => ToDto(book)).ToList();
    return booksDto;
  }

  public async Task<BookDto> GetAsyncById(string id)
  {
    var book = await _bookCollection.Find(x => x.Id == new ObjectId(id)).FirstOrDefaultAsync();

    // id bulamaz ise uygulama hata versin
    ArgumentNullException.ThrowIfNull(book);

    return ToDto(book);

  }

  public async Task<BookDto> CreateAsync(BookDto dto)
  {
    var book = ToModel(dto);
    await _bookCollection.InsertOneAsync(book);
    var response = ToDto(book);
    return response;
  }

  public async Task<BookDto> UpdateAsync(string id, BookDto dto)
  {
    // var update = Builders<Book>.Update.Set(x => x.Title, dto.Title)
    //     .Set(x => x.Description, dto.Description)
    //     .Set(x => x.Editorial, dto.Editorial);

    var updateDefination = Builders<Book>.Update
    .Set(book => book.Title, dto.Title);
    // await _bookCollection.UpdateOneAsync(x=> x.Id == new ObjectId(id),updateDefination);

    var model = ToModel(dto);
    model.Id = new ObjectId(id);
    // dtodan gelen bütün alanları document üzerinden güncellmek için replace.
    await _bookCollection.ReplaceOneAsync(x => x.Id == new ObjectId(id), model);

    return ToDto(model);
  }

  public async Task<BookDto> DeleteAsync(string id) {
    var book =  await _bookCollection.FindOneAndDeleteAsync(x=> x.Id == new ObjectId(id));

    ArgumentNullException.ThrowIfNull(book);

    return ToDto(book);

  }

  public async Task<List<BookDto>> GetFiltersAsync()
  {
    // Java Spring Data Mongo Template denk gelir.
    var filter = Builders<Book>.Filter;
    var filter1 = filter.Eq("title","Net Core Deneme");
    var filter2 = filter.Regex("gender","/Narrativas/i");
    var filter3 = filter.And(filter.Gte("num_pages",100),filter.Lte("num_pages",500));
    var filter4 = filter.Exists("tags");

    var orFilter = filter.Or(filter1,filter2,filter3);

    var books =  await _bookCollection.Find(orFilter).Skip(0).Limit(10).ToListAsync();

    return books.Select(x=> ToDto(x)).ToList();
  }

  private BookDto ToDto(Book book)
  {
    return new BookDto
    {
      Id = book.Id.ToString(),
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
      CategoryIds = book.CategoryIds.Select(a => a.ToString()).ToList()
    };
  }

  private Book ToModel(BookDto book)
  {
    return new Book
    {
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
      CategoryIds = book.CategoryIds.Select(a => new ObjectId(a)).ToList()
    };
  }


}