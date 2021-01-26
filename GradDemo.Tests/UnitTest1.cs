using GradDemo.Api;
using GradDemo.Api.Entities;
using GradDemo.Api.Models;
using GradDemo.Api.Models.CoinGecko;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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
        public async Task AsyncTest()
        {
            var tester = GetName();
            var testerasync = GetNameAsync();
            var testerasync2 = GetNameAsync();
            var testerasync3 = GetNameAsync();


            var res = await Task.WhenAll(testerasync, testerasync2, testerasync3);

            var test = testerasync.Result;

            Assert.Pass();
        }

        [Test]
        public async Task NotFoundTest()
        {
            var shouldFail = await CallHelper.GetAndDeserialize<IList<WeatherForecast>>(_httpClient, "/asdasdasdas");

            Assert.IsTrue(shouldFail.httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [Test]
        public async Task ErrorFromApi()
        {
            var shouldFail = await CallHelper.GetAndDeserialize<Response<string>>(_httpClient, "/Demo/should-fail");

            Assert.IsTrue(shouldFail.httpResponse.IsSuccessStatusCode && !shouldFail.content.Success);

        }

        [Test]
        public async Task BasicApiTest()
        {
            var shouldFail = await CallHelper.GetAndDeserialize<IList<WeatherForecast>>(_httpClient, "/Demo");

            Assert.IsTrue(shouldFail.httpResponse.IsSuccessStatusCode);
            
            var niceer = shouldFail.content;
                        
            Assert.NotNull(niceer);
            Assert.IsTrue(niceer.Count > 0);
        }

        [Test]
        public async Task TestAPost()
        {
            var inputName = "Tommy";

            var shouldFail = await CallHelper.PostAndDeserialize<Response<string>>(_httpClient, "/Demo/say-hello", inputName);

            Assert.IsTrue(shouldFail.httpResponse.IsSuccessStatusCode);

            var resultContent = shouldFail.content.Payload;

            Assert.NotNull(resultContent);
            Assert.IsTrue($"Hello, {inputName}!" == resultContent);
        }

        [Test]
        public async Task TestAddContact()
        {
            var contacts2 = await CallHelper.GetAndDeserialize<Response<ICollection<Contact>>>(_httpClient, "Demo/contacts");

            Assert.IsTrue(contacts2.content.Payload.Count == 0);

            var test = await CallHelper.PostAndDeserialize<Response<string>>(_httpClient,
                "/Demo/add-contact", new ContactRequest()
                {
                    ContactNumber = "0821231234",
                    Name = "Tommy",
                    LastName = "Suranem"
                }
                );

            Assert.IsTrue(test.httpResponse.IsSuccessStatusCode);

            var contacts = await CallHelper.GetAndDeserialize<Response<ICollection<Contact>>>(_httpClient, "Demo/contacts");

            Assert.IsTrue(contacts.content.Payload.Count > 0);

        }


        private async Task<string> GetNameAsync()
        {
            await Task.Yield();

            return "Tommy";
        }

        private string GetName()
        {
            return "Tommy";
        }
        
        [Test]
        public async Task TestGetCurrency()
        {
            string coinId = "bitcoin";
            string currency = "usd"; 
            var usdResult = await CallHelper.GetAndDeserialize<Response<CryptoCoinResponse>>(_httpClient, $"crypto/value/for/{coinId}/currency/{currency}");

            Assert.IsTrue(usdResult.httpResponse.IsSuccessStatusCode);

            currency = "zar";
            var zarresult = await CallHelper.GetAndDeserialize<Response<CryptoCoinResponse>>(_httpClient, $"crypto/value/for/{coinId}/currency/{currency}");

            Assert.IsTrue(usdResult.httpResponse.IsSuccessStatusCode);

            Assert.IsTrue(usdResult.content.Payload.Value < zarresult.content.Payload.Value);
        }
    }
}