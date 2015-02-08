using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain
{
    public interface IWeatherService : IDisposable
    {
        IEnumerable<Geoname> GetGeonames(string input);
        Geoname AddGeonameToDatabase(Geoname geo);
        Geoname RefreshForecasts(Geoname geoname);
    }
}
