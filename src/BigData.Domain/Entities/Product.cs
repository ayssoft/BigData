using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BigData.Domain.Entities;

/// <summary>
/// Product entity stored in MongoDB
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// MongoDB ObjectId representation
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public new string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    /// <summary>
    /// Product name
    /// </summary>
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Product description
    /// </summary>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Product price
    /// </summary>
    [BsonElement("price")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }

    /// <summary>
    /// Product category
    /// </summary>
    [BsonElement("category")]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Creation date
    /// </summary>
    [BsonElement("createdDate")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public new DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Last update date
    /// </summary>
    [BsonElement("updatedDate")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public new DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
}
