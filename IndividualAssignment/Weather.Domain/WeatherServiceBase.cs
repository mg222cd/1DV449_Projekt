using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain
{
    public abstract class WeatherServiceBase : IWeatherService
    {
        public abstract IEnumerable<Geoname> GetGeonames(string input);
        public abstract Geoname AddGeonameToDatabase(Geoname geo);
        public abstract Geoname RefreshForecasts(Geoname geoname);

        //Disposable
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
