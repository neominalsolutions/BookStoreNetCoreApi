using MongoDB.Bson.Serialization.Attributes;

public class BooksWithCategoryDto {
    public string Id { get; set; }
    
    [BsonElement("title")]
    public string Title { get; set; }
    
    [BsonElement("category_names")]
    public List<string> CategoryNames { get; set; }
}