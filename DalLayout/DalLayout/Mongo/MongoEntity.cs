using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DalLayout.Mongo
{
    public abstract class MongoEntity : IMongoEntity<ObjectId>
    {
        public ObjectId Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Created { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Updated { get; set; }
    }

}
