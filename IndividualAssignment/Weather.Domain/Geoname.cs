using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain
{
    public partial class Geoname
    {
        public Geoname(string id, string cityName, string region, string country, string latitude, string langitude, string municipality)
            : this()
        {
            geonameId = id;
            name = cityName;
            adminName1 = region;
            countryName = country;
            lat = latitude;
            lng = langitude;
            adminName2 = municipality;
        }
    }
}
