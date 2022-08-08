namespace LegacyApp;
public class SportsPersonService
{
    public bool AddSportsPerson(string fname, string lastname, DateTime birthdate, string email, object favSport)
    {
        if(string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lastname))
            return false;

        if (!email.Contains("@") && !email.Contains("."))
            return false;

        var now = DateTime.Now;
        int age = now.Year - birthdate.Year;

        if (now.Month < birthdate.Month || (now.Month == birthdate.Month && now.Day < birthdate.Day))
            age--;

        if (age < 18)
        {
            return false;
        }

        var person = new Person();

        person.FirstName = fname;
        person.Birthdate = birthdate;
        person.Email = email;
        person.LastName = lastname;

        if (favSport.GetType() == typeof(Soccer))
        {
            person.FavSportSummary = person.FirstName + " " + person.LastName + " has " + ((Soccer)favSport).Experience + " Year Experience in Soccer\n";
            person.FavSportSummary += person.FirstName + " " + person.LastName + " plays the Position " + ((Soccer)favSport).Position+"\n";
            person.FavSportSummary += person.FirstName + " " + person.LastName + " plays for Team " + ((Soccer)favSport).Team+"\n";
        }
        if (favSport.GetType() == typeof(Tennis))
        {
            person.FavSportSummary = person.FirstName + " " + person.LastName + " has " + ((Tennis)favSport).Experience + " Year Experience in Tennis\n";
            person.FavSportSummary += person.FirstName + " " + person.LastName + " has won " + ((Tennis)favSport).TorunamentsWon + " Tournaments\n";
        }
        if (favSport.GetType() == typeof(Chess))
        {
            person.FavSportSummary = person.FirstName + " " + person.LastName + " has " + ((Chess)favSport).Experience + " Year Experience in Chess\n";
            person.FavSportSummary += person.FirstName + " " + person.LastName + " has an ELO of " + ((Chess)favSport).Elo + "\n";
        }

        if (favSport.GetType() != typeof(Soccer) && favSport.GetType() != typeof(Tennis) && favSport.GetType() != typeof(Chess))
        {
            return false;
        }

        var measurementLoader = new MeasurementLoader();
        var measurements = measurementLoader.GetMeasurementByName(person.FirstName, person.LastName);

        if (measurements == null)
            return false;

        //Basketball
        if (measurements.Value.heigth >= 180 && measurements.Value.weigth < 100)
        {
            var suggestSport = new Person.SuggestedSport();

            suggestSport.Sport = "Basketball";
            if (measurements.Value.heigth < 200)
                suggestSport.Position = "Point Guard";

            else
            {
                suggestSport.Position = "Center";
            }

        }

        //American Football
        if (measurements.Value.heigth >= 180)
        {
            var suggestSport = new Person.SuggestedSport();

            suggestSport.Sport = "American Football";
            if (measurements.Value.weigth < 100)
                suggestSport.Position = "Quarter Back";

            else
            {
                suggestSport.Position = "Lineman";
            }
        }

        //Soccer
        if (measurements.Value.weigth < 100)
        {
            var suggestSport = new Person.SuggestedSport();

            suggestSport.Sport = "Soccer";
            if (measurements.Value.heigth < 200)
                suggestSport.Position = "Striker";

            else
            {
                suggestSport.Position = "Goalkeeper";
            }
        }

        PersonRepository.SavePerson(person);

        return true;

    }
}
