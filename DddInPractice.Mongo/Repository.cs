using System;
using DddInPractice.Logic.Common;
using DddInPractice.Logic.SharedKernel;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DddInPractice.Mongo
{
    public abstract class Repository
    {
        protected static IMongoDatabase Database;
        static Repository()
        {
            BsonClassMap.RegisterClassMap<Entity>(sm =>
            {
                sm.MapIdProperty(p => p.Id);
            });

            BsonClassMap.RegisterClassMap<Money>(m => m.AutoMap());


            var client = new MongoClient("mongodb://localhost");
            Database = client.GetDatabase("DddInPractice");
        }

    }

    public abstract class Repository<T> : Repository
        where T : AggregateRoot
    {
        protected static readonly IMongoCollection<T> Collection;

        static Repository()
        {
            Collection = Database.GetCollection<T>(typeof (T).Name);
        }

        public T GetById(long id)
        {
            return Collection.FindSync(c => c.Id == id).SingleOrDefault();
        }

        public void Save(T aggregateRoot)
        {
            Collection.ReplaceOne(ar => ar.Id == aggregateRoot.Id, aggregateRoot, new UpdateOptions() { IsUpsert = true});
        }
    }
}