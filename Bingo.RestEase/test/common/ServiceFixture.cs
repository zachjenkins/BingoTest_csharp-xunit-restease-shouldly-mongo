using Bingo.RestEase.Models.Response;
using Bingo.RestEase.Services;
using MongoDB.Driver;
using RestEase;
using System;
using System.Net.Http;

namespace Bingo.RestEase.Test.Common
{
    public class ServiceFixture : IDisposable
    {
        public ServiceFixture()
        {
            InitializeMongo();

            IntializeWebClient();

            InitializeApiInterface();
        }

        public void Dispose()
        {
      
        }

        public void InitializeMongo()
        {
            MongoClient = new MongoClient(@"mongodb://localhost:27017?connectionTimeout=3000");
            Database = MongoClient.GetDatabase("bingo");
            ExercisesCollection = Database.GetCollection<Exercise>("exercises");
            MusclesCollection = Database.GetCollection<Muscle>("muscles");
            ActivationsCollection = Database.GetCollection<Activation>("activations");
        }

        public void IntializeWebClient()
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(@"http://localhost:63301"),
                Timeout = TimeSpan.FromSeconds(2)
            };
        }

       
        public void InitializeApiInterface()
        {
            ActivationsService = RestClient.For<IActivationsService>(HttpClient);
            ExercisesService = RestClient.For<IExercisesService>(HttpClient);
            MusclesService = RestClient.For<IMusclesService>(HttpClient);
        }

        public IActivationsService ActivationsService { get; set; }
        public IExercisesService ExercisesService { get; set; }
        public IMusclesService MusclesService { get; set; }

        public IMongoCollection<Exercise> ExercisesCollection { get; set; }
        public IMongoCollection<Muscle> MusclesCollection { get; set; }
        public IMongoCollection<Activation> ActivationsCollection { get; set; }
        
        private MongoClient MongoClient { get; set; }
        private IMongoDatabase Database { get; set; }
        
        private HttpClient HttpClient { get; set; }
    }
}
