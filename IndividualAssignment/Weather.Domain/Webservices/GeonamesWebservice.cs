using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;

namespace Weather.Domain.Webservices
{
    public class GeonamesWebservice : IGeonamesWebservice
    {
        public IEnumerable<Geoname> GetGeonames(string geoName)
        {
            try
            {
                string rawJson;

                string requestUriString = String.Format("http://api.geonames.org/searchJSON?name={0}&style=full&maxRows=100&username=marikegrinde", geoName);
                var request = (HttpWebRequest)WebRequest.Create(requestUriString);

                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        rawJson = reader.ReadToEnd();
                    }
                }

                JObject results = JObject.Parse(rawJson);
                var cities = new List<Geoname>();
                foreach (var result in results["geonames"])
                {
                    string id = (string)result["geonameId"];
                    string cityName = (string)result["name"];
                    string region = (string)result["adminName1"];
                    string countryName = (string)result["countryName"];
                    string latitude = (string)result["lat"];
                    string langitude = (string)result["lng"];
                    string municipality = (string)result["adminName2"];
                    cities.Add(new Geoname(id, cityName, region, countryName, latitude, langitude, municipality));
                }

                return cities;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        public bool TestGeonamesResponse()
        {
            try
            {
                string rawJsonTest;

                string requestUriString = "http://api.geonames.org/searchJSON?name=Stockholm&style=full&maxRows=100&username=marikegrinde";
                var request = (HttpWebRequest)WebRequest.Create(requestUriString);

                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        rawJsonTest = reader.ReadToEnd();
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
