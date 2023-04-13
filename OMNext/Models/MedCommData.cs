using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNext.Models
{
    public class MedCommData
    {
        public string gmt { get; set; }

        public string alert { get; set; }

        public string directives { get; set; }

        public AirQuality airquality { get; set; }

        public WaterQuality waterquality { get; set; }
    }

    public class AirQuality
    {
        public string south { get; set; }

        public string north { get; set; }
    }

    public class WaterQuality
    {
        public Reading loc1 { get; set; }

        public Reading loc2 { get; set; }

        public Reading loc3 { get; set; }

        public Reading loc4 { get; set; }

        public Reading loc5 { get; set; }
    }

    public class Reading
    {
        public string location { get; set; }
        
        public string ph { get; set; }

        public string turbidity { get; set; }

        public string contaminates { get; set; }
    }
}
