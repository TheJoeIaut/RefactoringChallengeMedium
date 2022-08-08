using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public interface ITimeService
    {
        public DateTime Now { get; }
    }

    public class TimeService : ITimeService
    {
        public DateTime Now  => DateTime.Now;
    }
}
