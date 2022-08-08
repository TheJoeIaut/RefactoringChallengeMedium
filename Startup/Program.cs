using LegacyApp;

var sportsPersonService = new SportsPersonService();
var result = sportsPersonService.AddSportsPerson("max","mustermann",new DateTime(2000,1,1), "max.mustermann@mailer.com",new Soccer()
{
    Experience = 5,
    Position = "Striker",
    Team = "Fc Barcelona"
});

Console.WriteLine(result ? "Success" : "Failure");