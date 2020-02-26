using MongoDB.Bson;
using System.Linq;
using System.Text;

namespace DalLayout.Mongo
{
    public interface IMongoRepository<TEntity> : IRepository<TEntity, ObjectId> where TEntity : MongoEntity
    {
        IQueryable<TEntity> GetAll();
    }
}
