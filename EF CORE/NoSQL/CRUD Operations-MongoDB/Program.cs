using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace CRUD_Operations_MongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            //ex.1?!
            string jsonString = File.ReadAllText("articles.json");

            var client = new MongoClient("mongodb://127.0.0.1:27017/?compressors=disabled&gssapiServiceName=mongodb");
            var database = client.GetDatabase("articles");

            //var bDoc = BsonSerializer.Deserialize<BsonDocument>(jsonString);


            var collection = database.GetCollection<BsonDocument>("test");

            //collection.InsertOne(bDoc);
            var articles = collection.Find(new BsonDocument()).ToList();
            //ex.2!?
            //foreach (var bsonElement in articles)
            //{
            //    string name = bsonElement.;
            //    Console.WriteLine(name);
            //}
            //ex.3
            //var article = new BsonDocument()
            //{
            //    {"author","Steve Jobs"},
            //     {"date","05-05-2005"},
            //     {"name","The story of Apple"},
            //     {"rating","60"}
            //};
            //collection.InsertOne(article);

            //ex.4
            foreach (var article in articles)
            {
                int newRating = int.Parse(article.GetElement("rating").Value.AsString) + 10;
                var filterQuery = Builders<BsonDocument>.Filter.Eq("_id", article.GetElement("_id").Value);
                var updateQuery = Builders<BsonDocument>.Update.Set("rating", newRating.ToString());
                collection.UpdateOne(filterQuery, updateQuery);
            }
            //ex.5


        }
    }
}
