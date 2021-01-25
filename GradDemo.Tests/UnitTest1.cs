using NUnit.Framework;
using System.Net.Http;

namespace GradDemo.Tests
{
    public class Tests
    {
        private APIWebApplicationFactory? _apiWebApplicationFactory;
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            _apiWebApplicationFactory = new APIWebApplicationFactory();
            _httpClient = _apiWebApplicationFactory.CreateClient();
        }

        [Test]
        public void Test1()
        {


            Assert.Pass();
        }
    }
}