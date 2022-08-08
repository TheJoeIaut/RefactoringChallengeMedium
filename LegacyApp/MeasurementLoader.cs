using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public interface IMeasurementLoader
    {
        (int heigth, int weight)? GetMeasurementByName(string firstname, string lastname);
    }

    public class MeasurementLoader : IMeasurementLoader
    {
        public (int heigth, int weight)? GetMeasurementByName(string firstname, string lastname)
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
    