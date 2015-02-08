using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weather.Domain;
using Weather.Domain.Webservices;
using Weather.Domain.ViewModels;

namespace Weather.MVC.Controllers
{
    public class WeatherController : Controller
    {
        //Fields and constructors
        private IWeatherService _service;

        public WeatherController()
            : this(new WeatherService())
        {
            //Empty!
        }

        public WeatherController(IWeatherService service)
        {
            _service = service;
        }

        // GET: /Weather/
        public ActionResult Index()
        {
            return View();
        }

        // POST: /Weather/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Geoname")] WeatherIndexViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Geonames = _service.GetGeonames(model.Geoname);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View(model);
        }

        // GET: /Weather/
        public ActionResult Forecast(Geoname geoname)
        {
            try
            {
                geoname = _service.AddGeonameToDatabase(geoname);
                geoname = _service.RefreshForecasts(geoname);
                //geonames-modellen skickas med i konstruktorn till vymodellen
                var viewModel = new WeatherForecastViewModel(geoname);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                ModelState.AddModelError(String.Empty, ex.Message);
            }
            YrWebservice _yrWebservice = new YrWebservice();
            return View("ForecastError", _yrWebservice);
        }
    }
}