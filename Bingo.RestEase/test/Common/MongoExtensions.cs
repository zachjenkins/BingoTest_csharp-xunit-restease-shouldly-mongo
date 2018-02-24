using MongoDB.Driver;

namespace Bingo.RestEase.Test.Common
{
    public static class MongoExtensions
    {
        public static T GetEntityById<T>(this IMongoCollection<T> collection, string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).FirstOrDefault();
        }

        public static void DeleteEntityById<T>(this IMongoCollection<T> collection, string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);

            collection.DeleteOne(filter);
        }
    }
}
