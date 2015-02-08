using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.ViewModels
{
    public class WeatherForecastViewModel
    {
        //Fält
        private Geoname _geoname;
        private IEnumerable<Forecast> _forecast;
        private DateTime _today = DateTime.Now;
        private DateTime _lastLoopedDate;

        //Konstruktor
        public WeatherForecastViewModel(Geoname geoname)
        {
            _geoname = geoname;
            _forecast = geoname.Forecasts;
        }

        //Hjälpmetoder
        public IEnumerable<Forecast> Forecast
        {
            get
            {
                return _forecast;
            }
        }

        public Geoname Geoname
        {
            get
            {
                return _geoname;
            }
        }

        public bool isNewDay(DateTime date)
        {
            if (_lastLoopedDate.Date != date.Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetWeekday(DateTime date)
        {
            _lastLoopedDate = date;
            if (_today.Day == date.Day)
            {
                return "Idag";
            }
            if (_today.Day + 1 == date.Day)
            {
                return "Imorgon";
            }
            else
            {
                return date.DayOfWeek.ToString();
            }
        }
    }
}

