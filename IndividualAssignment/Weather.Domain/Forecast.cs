using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain
{
    public partial class Forecast
    {
        public Forecast()
        {
            // Empty!
        }

        public Forecast(DateTime lastUpdate, DateTime nextUpdate, DateTime timefrom, DateTime timeto, int timeperiod, int temperature, string symbolId, Geoname geoname)
        {
            lastupdate = lastUpdate;
            nextupdate = nextUpdate;
            timeFrom = timefrom;
            timeTo = timeto;
            period = timeperiod;
            temp = temperature;
            symbol = symbolId;
            geoId = geoname.geoId;
            geoname.nextUpdate = nextupdate;
            Geoname = geoname;
        }
    }
}
