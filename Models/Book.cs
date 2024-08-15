using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Book
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("title")]
    public string? Title { get; set; }
    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("gender")]
    public string? Gender { get; set; }

    [BsonElement("num_pages")]
    public int NumPages { get; set; }

    [BsonElement("editorial")]
    public string? Editorial { get; set; }

    [BsonElement("isbn")]
    public long Isbn { get; set; }

    [BsonElement("year_edition")]
    public int YearEdition { get; set; }

    [BsonElement("date_edition")]
    public DateTime DateEdition { get; set; }

    [BsonElement("writer")]
    public string? Writer { get; set; }

    [BsonElement("image")]
    public string? Image { get; set; }

    [BsonElement("tags")]
    public List<string> Tags { get; set; }

    [BsonElement("category_ids")]
    public List<ObjectId> CategoryIds { get; set; }
}