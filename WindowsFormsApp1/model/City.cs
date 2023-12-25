using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.model
{
    public struct City
    {
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public City(string name, string lat, string lon)
        {
            this.Name = name;
            this.Latitude = lat;
            this.Longitude = lon;
        }
    }
}
