using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Webservices
{
    public interface IYrWebservice
    {
        IEnumerable<Forecast> GetForecast(Geoname geoname);
        bool TestYrWebserviceResponse();
    }
}
