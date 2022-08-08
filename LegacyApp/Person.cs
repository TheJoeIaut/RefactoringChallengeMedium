using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public string FavSportSummary { get; set; }

        public List<SuggestedSport> SuggestedSports { get; set; }


        public class SuggestedSport
        {
            public string Sport { get; set; }
            public string Position { get; set; }
        }

    }
}
