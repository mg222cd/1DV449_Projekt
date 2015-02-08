using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Repositories
{
    public abstract class WeatherRepositoryBase : IWeatherRepository
    {
        //Geoname
        protected abstract IQueryable<Geoname> QueryGeonames();

        public IEnumerable<Geoname> FindAllGeonames()
        {
            return QueryGeonames().ToList();
        }

        public Geoname FindGeonameById(int id)
        {
            return QueryGeonames().SingleOrDefault(g => g.geoId == id);
        }

        public Geoname FindGeonameByGeonameId(string geonameid)
        {
            return QueryGeonames().SingleOrDefault(g => g.geonameId == geonameid);
        }

        public IEnumerable<Geoname> FindGeonameByName(string name)
        {
            var geonames =
                (from g in QueryGeonames()
                 where g.name.Contains(name)
                 select g).OrderBy(x => x.adminName2);
            return geonames.ToList();
        }

        public abstract void AddGeoname(Geoname geoname);
        public abstract void UpdateGeoname(Geoname geoname);
        public abstract void RemoveGeoname(int id);

        //Forecast
        protected abstract IQueryable<Forecast> QueryForecasts();

        public IEnumerable<Forecast> FindAllForecasts()
        {
            return QueryForecasts().ToList();
        }

        public Forecast FindForecastById(int id)
        {
            return QueryForecasts().SingleOrDefault(f => f.forecastId == id);
        }

        public abstract void AddForecast(Forecast forecast);
        public abstract void UpdateForecast(Forecast forecast);
        public abstract void RemoveForecast(int id);

        //Save
        public abstract void Save();

        //IDisposable
        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }
    }
}
