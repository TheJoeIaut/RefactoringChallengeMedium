using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public class MeasurementLoader
    {
        public (int heigth, int weigth)? GetMeasurementByName(string firstname, string lastname)
        {
            var lookup = new Dictionary<string, (int heigth, int weigth)>
            {
                { "maxmustermann", (180, 75) },
                { "maxinemusterfrau", (160, 50) },
                { "stefanSchwermann", (180, 120) },
                { "gregorgroßmann", (210, 75) },
            };

            if(lookup.TryGetValue(firstname + lastname, out var result))
                return result;

            return null;
        }
    }
}
    