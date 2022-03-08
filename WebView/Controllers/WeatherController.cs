using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebView.Models;
namespace WebView.Controllers
{
    [Authorize]
    public class WeatherController : Controller
    {
        // GET: WeatherController
        private APICaller apiCaller;
        public WeatherController(IConfiguration config)
        {
            apiCaller = new APICaller(config);
        }

        public ActionResult Index()
        {
            IEnumerable<WeatherForecast> forecasts =APICaller.GetMany<WeatherForecast>("WeatherForecast");
            return View(forecasts);
        }

        // GET: WeatherController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WeatherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WeatherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WeatherForecast forecast)
        {
            try
            {
              var response=  APICaller.Post<WeatherForecast>("WeatherForecast", forecast);
                if(response !=null && response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
                else
                    return View(forecast);
            }
            catch(Exception ex)
            {
                throw ex;
                return View();
            }
        }

        // GET: WeatherController/Edit/5
        public ActionResult Edit(string id)
        {
            WeatherForecast forecast = null;
            IEnumerable<WeatherForecast> forecasts = APICaller.GetMany<WeatherForecast>("WeatherForecast");
            if (forecasts != null)
                forecast=forecasts.Where(t => t.id == id).FirstOrDefault();
            if (forecast != null)
                return View(forecast);
            else
                return View();

        }

        // POST: WeatherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, WeatherForecast forecast)// IFormCollection collection)
        {
            try
            {
                var response = APICaller.Put<WeatherForecast>("WeatherForecast", forecast);
                if (response != null && response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
                else
                    return View(forecast);
            }
            catch
            {
                return View();
            }
        }

        // GET: WeatherController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WeatherController/Delete/5
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
