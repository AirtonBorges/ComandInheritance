using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// ReSharper disable InconsistentNaming
namespace ComandInheritance.Entities;

public class MongoInstrucao
{
    [BsonId] public ObjectId? _id { get; protected set; } = null;
    public string? mensagem { get; protected set; } = null;
}