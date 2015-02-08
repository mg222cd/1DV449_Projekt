using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Repositories;
using Weather.Domain.Webservices;

namespace Weather.Domain
{
    public class WeatherService : WeatherServiceBase
    {
        private IWeatherRepository _repository;
        private IGeonamesWebservice _geonamesWebservice;
        private IYrWebservice _yrWebservice;

        public WeatherService()
            : this(new WeatherRepository(), new GeonamesWebservice(), new YrWebservice())
        {
            //Empty!
        }

        public WeatherService(IWeatherRepository repository, IGeonamesWebservice geonamesWebservice, IYrWebservice yrWebservice)
        {
            _repository = repository;
            _geonamesWebservice = geonamesWebservice;
            _yrWebservice = yrWebservice;
        }

        public override IEnumerable<Geoname> GetGeonames(string input)
        {
            IEnumerable<Geoname> cities;
            //Om Geonames-Webservice är nerkopplad sker sökning mot databasen...
            if (_geonamesWebservice.TestGeonamesResponse() == false)
            {
                cities = _repository.FindGeonameByName(input);
            }
            //...annars mot GeonamesWebservice
            else
            {
                cities = _geonamesWebservice.GetGeonames(input);
            }
            return cities;
        }

        public override Geoname AddGeonameToDatabase(Geoname geo)
        {
            //Försök hämta Geonamet från databasen
            var geoname = _repository.FindGeonameByGeonameId(geo.geonameId);

            //Om geoname == null har den ej hittats i databasen, 
            //då ska istället objektet som hämtats från webservicen sparas i databasen.
            if (geoname == null)
            {
                geoname = geo;
                _repository.AddGeoname(geoname);
                _repository.Save();
            }

            return geoname;
        }

        public override Geoname RefreshForecasts(Geoname geoname)
        {
            //Nya prognoser ska hämtas om det inte finns några prognoser för valit id, i databasen,
            //eller om giltighetstiden för prognoserna gått ut.
            //detta görs enbart om YrWebservice fungerar
            if (geoname.Forecasts == null || !geoname.Forecasts.Any() || geoname.nextUpdate < DateTime.Now && _yrWebservice.TestYrWebserviceResponse() == true)
            {
                //radera gamla prognoser
                foreach (var forecast in geoname.Forecasts.ToList())
                {
                    _repository.RemoveForecast(forecast.forecastId);
                }
                //hämta nya från webbservicen
                foreach (var forecast in _yrWebservice.GetForecast(geoname))
                {
                    _repository.AddForecast(forecast);
                }
                //spara
                _repository.Save();
            }
            return geoname;
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
