using System;
using System.Linq;
using System.Runtime.CompilerServices;
using FluentAssertions;
using Moq;
using Xunit;

namespace LegacyApp.Tests
{
    public class SportsPersonServiceTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("max", "")]
        [InlineData("", "mustermann")]
        [InlineData(null, "mustermann")]
        [InlineData("max", null)]
        [InlineData("", null)]
        [InlineData(null, null)]
        public void AddSportsPerson_InvalidName(string firstname, string lastname)
        {
            var service = new SportsPersonService();

            var result = service.AddSportsPerson(firstname, lastname, new DateTime(2000, 1, 1), "test@testmailer.com",
                new Soccer()
                {
                    Experience = 5,
                    Position = "Striker",
                    Team = "FC Barcelona"
                });

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("max", "mustermann")]
        public void AddSportsPerson_ValidName(string firstname, string lastname)
        {
            var timeServiceMock = new Mock<ITimeService>();
            timeServiceMock.SetupGet(x => x.Now).Returns(new DateTime(2022, 1, 15));

            var personRepositoryMock = new Mock<IPersonRepository>();
            var measurementLoaderMock = new Mock<IMeasurementLoader>();
            measurementLoaderMock.Setup(x => x.GetMeasurementByName(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((180, 90));

            var service = new SportsPersonService(personRepositoryMock.Object, measurementLoaderMock.Object,
                timeServiceMock.Object);

            var result = service.AddSportsPerson(firstname, lastname, new DateTime(2000, 1, 1), "test@testmailer.com",
                new Soccer()
                {
                    Experience = 5,
                    Position = "Striker",
                    Team = "FC Barcelona"
                });

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("max@mail.com")]
        [InlineData("max@m@ail..")]
        [InlineData(".max@mail")]
        public void AddSportsPerson_ValidEmail(string email)
        {
            var timeServiceMock = new Mock<ITimeService>();
            timeServiceMock.SetupGet(x => x.Now).Returns(new DateTime(2022, 1, 15));

            var personRepositoryMock = new Mock<IPersonRepository>();
            var measurementLoaderMock = new Mock<IMeasurementLoader>();
            measurementLoaderMock.Setup(x => x.GetMeasurementByName(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((180, 90));

            var service = new SportsPersonService(personRepositoryMock.Object, measurementLoaderMock.Object,
                timeServiceMock.Object);

            var result = service.AddSportsPerson("max", "mustermann", new DateTime(2000, 1, 1), email, new Soccer()
            {
                Experience = 5,
                Position = "Striker",
                Team = "FC Barcelona"
            });

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("max@mail")]
        [InlineData("max.mail")]
        [InlineData("maxmail")]
        public void AddSportsPerson_InvalidEmail(string email)
        {
            var service = new SportsPersonService();

            var result = service.AddSportsPerson("max", "mustermann", new DateTime(2000, 1, 1), email, new Soccer()
            {
                Experience = 5,
                Position = "Striker",
                Team = "FC Barcelona"
            });

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("2004-01-16")]
        [InlineData("2005-01-15")]
        [InlineData("2004-02-01")]
        [InlineData("2005-01-01")]
        public void AddSportsPerson_InvalidAge(string birthdate)
        {
            var timeServiceMock = new Mock<ITimeService>();
            timeServiceMock.SetupGet(x => x.Now).Returns(new DateTime(2022, 1, 15));

            var personRepositoryMock = new Mock<IPersonRepository>();
            var measurementLoaderMock = new Mock<IMeasurementLoader>();
            measurementLoaderMock.Setup(x => x.GetMeasurementByName(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((180, 90));

            var service = new SportsPersonService(personRepositoryMock.Object, measurementLoaderMock.Object,
                timeServiceMock.Object);

            var result = service.AddSportsPerson("max", "mustermann", DateTime.Parse(birthdate), "test@testmailer.com",
                new Soccer()
                {
                    Experience = 5,
                    Position = "Striker",
                    Team = "FC Barcelona"
                });

            result.Should().BeFalse();
        }


        [Theory]
        [InlineData("2004-01-15")]
        [InlineData("2003-12-31")]
        [InlineData("2003-01-16")]
        public void AddSportsPerson_ValidAge(string birthdate)
        {
            var timeServiceMock = new Mock<ITimeService>();
            timeServiceMock.SetupGet(x => x.Now).Returns(new DateTime(2022, 1, 15));

            var personRepositoryMock = new Mock<IPersonRepository>();
            var measurementLoaderMock = new Mock<IMeasurementLoader>();
            measurementLoaderMock.Setup(x => x.GetMeasurementByName(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((180, 90));

            var service = new SportsPersonService(personRepositoryMock.Object, measurementLoaderMock.Object,
                timeServiceMock.Object);

            var result = service.AddSportsPerson("max", "mustermann", DateTime.Parse(birthdate), "test@testmailer.com",
                new Soccer()
                {
                    Experience = 5,
                    Position = "Striker",
                    Team = "FC Barcelona"
                });

            result.Should().BeTrue();
        }

        [Fact]
        public void AddSportsPerson_FavSportSoccer()
        {
            var timeServiceMock = new Mock<ITimeService>();
            timeServiceMock.SetupGet(x => x.Now).Returns(new DateTime(2022, 1, 15));

            var personRepositoryMock = new Mock<IPersonRepository>();
            var measurementLoaderMock = new Mock<IMeasurementLoader>();
            measurementLoaderMock.Setup(x => x.GetMeasurementByName(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((180, 90));

            var service = new SportsPersonService(personRepositoryMock.Object, measurementLoaderMock.Object,
                timeServiceMock.Object);

            var result = service.AddSportsPerson("max", "mustermann", new DateTime(2002, 1, 15), "test@testmailer.com",
                new Soccer()
                {
                    Experience = 5,
                    Position = "Striker",
                    Team = "FC Barcelona"
                });

            result.Should().BeTrue();
            personRepositoryMock.Verify(x => x.SavePerson(It.Is<Person>(y =>
                y.FavSportSummary.Contains("max mustermann has 5 Year Experience in Soccer") &&
                y.FavSportSummary.Contains("max mustermann plays the Position Striker") &&
                y.FavSportSummary.Contains("max mustermann plays for Team FC Barcelona"))));
        }

        [Fact]
        public void AddSportsPerson_FavSportTennis()
        {
            var timeServiceMock = new Mock<ITimeService>();
            timeServiceMock.SetupGet(x => x.Now).Returns(new DateTime(2022, 1, 15));

            var personRepositoryMock = new Mock<IPersonRepository>();
            var measurementLoaderMock = new Mock<IMeasurementLoader>();
            measurementLoaderMock.Setup(x => x.GetMeasurementByName(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((180, 90));

            var service = new SportsPersonService(personRepositoryMock.Object, measurementLoaderMock.Object,
                timeServiceMock.Object);

            var result = service.AddSportsPerson("max", "mustermann", new DateTime(2002, 1, 15), "test@testmailer.com",
                new Tennis()
                {
                    Experience = 3,
                    TorunamentsWon = 113
                });

            result.Should().BeTrue();
            personRepositoryMock.Verify(x => x.SavePerson(It.Is<Person>(y =>
                y.FavSportSummary.Contains("max mustermann has 3 Year Experience in Tennis") &&
                y.FavSportSummary.Contains("max mustermann has won 113 Tournaments"))));
        }

        [Fact]
        public void AddSportsPerson_FavSportChess()
        {
            var timeServiceMock = new Mock<ITimeService>();
            timeServiceMock.SetupGet(x => x.Now).Returns(new DateTime(2022, 1, 15));

            var personRepositoryMock = new Mock<IPersonRepository>();
            var measurementLoaderMock = new Mock<IMeasurementLoader>();
            measurementLoaderMock.Setup(x => x.GetMeasurementByName(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((180, 90));

            var service = new SportsPersonService(personRepositoryMock.Object, measurementLoaderMock.Object,
                timeServiceMock.Object);

            var result = service.AddSportsPerson("max", "mustermann", new DateTime(2002, 1, 15), "test@testmailer.com",
                new Chess()
                {
                    Experience = 9,
                    Elo = 2500
                });

            result.Should().BeTrue();
            personRepositoryMock.Verify(x => x.SavePerson(It.Is<Person>(y =>
                y.FavSportSummary.Contains("max mustermann has 9 Year Experience in Chess") &&
                y.FavSportSummary.Contains("max mustermann has an ELO of 2500") )));
        }

        [Theory]
        [InlineData(180,99, "Quarterback")]
        [InlineData(180, 100, "Lineman")]
        [InlineData(180, 140, "Lineman")]

        public void AddSportsPerson_SuggestedSportAmericanFootball(int heigth, int weight, string postion)
        {
            var timeServiceMock = new Mock<ITimeService>();
            timeServiceMock.SetupGet(x => x.Now).Returns(new DateTime(2022, 1, 15));

            var personRepositoryMock = new Mock<IPersonRepository>();
            var measurementLoaderMock = new Mock<IMeasurementLoader>();
            measurementLoaderMock.Setup(x => x.GetMeasurementByName(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((heigth, weight));

            var service = new SportsPersonService(personRepositoryMock.Object, measurementLoaderMock.Object,
                timeServiceMock.Object);

            var result = service.AddSportsPerson("max", "mustermann", new DateTime(2002, 1, 15), "test@testmailer.com",
                new Chess()
                {
                    Experience = 9,
                    Elo = 2500
                });

            result.Should().BeTrue();
            personRepositoryMock.Verify(x => x.SavePerson(It.Is<Person>(y => y.SuggestedSports.Any(z=>z.Position == postion && z.Sport == "American Football"))));
        }

        [Theory]
        [InlineData(180, 99, "Striker")]
        [InlineData(210, 99, "Goalkeeper")]
        [InlineData(200, 99, "Goalkeeper")]

        public void AddSportsPerson_SuggestedSportSoccer(int heigth, int weight, string postion)
        {
            var timeServiceMock = new Mock<ITimeService>();
            timeServiceMock.SetupGet(x => x.Now).Returns(new DateTime(2022, 1, 15));

            var personRepositoryMock = new Mock<IPersonRepository>();
            var measurementLoaderMock = new Mock<IMeasurementLoader>();
            measurementLoaderMock.Setup(x => x.GetMeasurementByName(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((heigth, weight));

            var service = new SportsPersonService(personRepositoryMock.Object, measurementLoaderMock.Object,
                timeServiceMock.Object);

            var result = service.AddSportsPerson("max", "mustermann", new DateTime(2002, 1, 15), "test@testmailer.com",
                new Chess()
                {
                    Experience = 9,
                    Elo = 2500
                });

            result.Should().BeTrue();
            personRepositoryMock.Verify(x => x.SavePerson(It.Is<Person>(y => y.SuggestedSports.Any(z => z.Position == postion && z.Sport == "Soccer"))));
        }

        [Theory]
        [InlineData(180, 99, "Point Guard")]
        [InlineData(200, 99, "Center")]
        [InlineData(210, 99, "Center")]

        public void AddSportsPerson_SuggestedSportBasketball(int heigth, int weight, string postion)
        {
            var timeServiceMock = new Mock<ITimeService>();
            timeServiceMock.SetupGet(x => x.Now).Returns(new DateTime(2022, 1, 15));

            var personRepositoryMock = new Mock<IPersonRepository>();
            var measurementLoaderMock = new Mock<IMeasurementLoader>();
            measurementLoaderMock.Setup(x => x.GetMeasurementByName(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((heigth, weight));

            var service = new SportsPersonService(personRepositoryMock.Object, measurementLoaderMock.Object,
                timeServiceMock.Object);

            var result = service.AddSportsPerson("max", "mustermann", new DateTime(2002, 1, 15), "test@testmailer.com",
                new Chess()
                {
                    Experience = 9,
                    Elo = 2500
                });

            result.Should().BeTrue();
            personRepositoryMock.Verify(x => x.SavePerson(It.Is<Person>(y => y.SuggestedSports.Any(z => z.Position == postion && z.Sport == "Basketball"))));
        }
    }
}