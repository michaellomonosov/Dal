using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DalLayout.Mongo
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : MongoEntity
    {
        private IMongoDatabase _database;
        private IMongoCollection<TEntity> _collection;

        public MongoRepository(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            MongoUrl mongoUrl = MongoUrl.Create(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
            _collection = SetupCollection();
        }

        protected virtual IMongoCollection<TEntity> SetupCollection()
        {
            try
            {
                var collectionName = BuildCollectionName();
                var collection = _database.GetCollection<TEntity>(collectionName);
                return collection;
            }
            catch (MongoException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected virtual string BuildCollectionName()
        {
            var typeName = typeof(TEntity).Name.ToLower();
            var pluralizedName = typeName.EndsWith("s") ? typeName : typeName + "s";
            return pluralizedName;
        }

        public void Delete(TEntity entity)
        {
            _collection.FindOneAndDelete(x => x.Id.Equals(entity.Id) & x.Created.Equals(entity.Created));
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public TEntity GetById(ObjectId id)
        {
            return _collection.Find(e => e.Id.Equals(id)).FirstOrDefault();
        }

        public void Insert(TEntity entity)
        {
            try
            {
                if (entity.Id == ObjectId.Empty)
                {
                    entity.Id = ObjectId.GenerateNewId();
                }
                entity.Created = DateTime.Now;
                entity.Updated = DateTime.Now;

                _collection.InsertOne(entity);
            }
            catch (MongoWriteException ex)
            {
                throw new Exception("Insert failed because the entity already exists!", ex);
            }
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            _collection.InsertMany(entities);
        }

        public void Update(TEntity entity)
        {
            var idFilter = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id) & Builders<TEntity>.Filter.Eq(e => e.Created, entity.Created);
            _collection.ReplaceOne(idFilter, entity);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                Update(item);
            }
        }

        public async Task<TEntity> GetByIdAsync(ObjectId id)
        {
            return await _collection.Find(e => e.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(TEntity entity)
        {
            try
            {
                if (entity.Id == ObjectId.Empty)
                {
                    entity.Id = ObjectId.GenerateNewId();
                }
                entity.Created = DateTime.Now;
                entity.Updated = DateTime.Now;

                await _collection.InsertOneAsync(entity);
            }
            catch (MongoWriteException ex)
            {
                throw new Exception("Insert failed because the entity already exists!", ex);
            }
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            await _collection.InsertManyAsync(entities);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var idFilter = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id) & Builders<TEntity>.Filter.Eq(e => e.Created, entity.Created);
            await _collection.ReplaceOneAsync(idFilter, entity);
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                await UpdateAsync(item);
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            //var idFilter = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id) & Builders<TEntity>.Filter.Eq(e => e.Created, entity.Created);
            //await _collection.DeleteOneAsync(idFilter);
            await _collection.FindOneAndDeleteAsync(x => x.Id.Equals(entity.Id) & x.Created.Equals(entity.Created));
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                await DeleteAsync(item);
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return _collection.AsQueryable();
        }

        public List<TEntity> All()
        {
            return _collection.Find(_ => true).ToList();
        }

        //public async Task<ICollection<TEntity>> GetAllAsyn()
        //{
        //    return await _collection.Find(_ => true).ToListAsync();
        //}
    }
}
