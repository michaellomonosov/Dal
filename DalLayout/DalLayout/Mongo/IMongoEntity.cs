using DalLayout.Entity;
using System;

namespace DalLayout.Mongo
{
    public interface IMongoEntity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        DateTime Created { get; set; }
        DateTime Updated { get; set; }
    }

}
