using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Web;
using System.Net;
using System.Net.Http;
using GradDemo.Api.Models;

namespace GradDemo.Api.Controllers
{
 
    public class CryptoController : Controller
    {
        // GET: CryptoController
        public ActionResult Index()
        {
            return View();
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


        static readonly HttpClient client;
        static CryptoController()
        {
            client = new HttpClient() { };

        }
        

        [HttpGet("{id}/{vsCurrency}")]
        public async Task<string> GetBitCoin(BitCoin bcoin)
        
        {
             
            //https://www.coingecko.com/en/api/simple/price

            
                string clientPath = string.Format("https://api.coingecko.com/api/v3/simple/price?ids={0}&vs_currencies={1}", bcoin.id, bcoin.vsCurrency);
                client.BaseAddress = new Uri(clientPath);
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
                string result = await response.Content.ReadAsStringAsync();

               return result;

        }


    }
}
