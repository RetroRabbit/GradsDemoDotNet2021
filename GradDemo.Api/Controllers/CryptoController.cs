using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace GradDemo.Api.Controllers
{
    public class CryptoController : Controller
    {
        private static readonly HttpClient client;

        static CryptoController()
        {
            client = new HttpClient()
            {

            };
        }
        // GET: CryptoController
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet("{id}/{currency_sym}")]
        public async Task<string> GetPrice(string id, string currency_sym)
        {
            string path = string.Format("https://api.coingecko.com/api/v3/simple/price?ids={0}&vs_currencies={1}", id, currency_sym);
            HttpResponseMessage response = await client.GetAsync(path);

            string apiResponse = await response.Content.ReadAsStringAsync();
            //var price = JsonConvert.DeserializeObject<CryptoCurrency>(apiResponse);

            return apiResponse;
        }
        // GET: CryptoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CryptoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CryptoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CryptoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CryptoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CryptoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CryptoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
