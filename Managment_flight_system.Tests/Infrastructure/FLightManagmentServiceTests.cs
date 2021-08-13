using AutoMapper;
using Core.POCO;
using FluentAssertions;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Services;
using Managment_flight_system.Tests.Common;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flight = Infrastructure.Models.Flight;

namespace Managment_flight_system.Tests.Infrastructure
{
  [TestFixture]
  public class FLightManagmentServiceTests
  {
    private Mock<IFlightRepository> _flightRepositoryMock;
    private Mock<IAirportRepository> _airportRepositoryMock;
    private Mock<IRepository<Comment>> _commentRepositoryMock;
    private IOptions<PriceOptions> _optionsFake;
    private PriceOptions _options;
    private Mock<IMapper> _mapperMock;
    private FLightManagmentService _fLightManagmentService;
    private IEnumerable<Core.POCO.Flight> _flights;
    private IEnumerable<Comment> _comments;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      _mapperMock = new Mock<IMapper>();
      _flightRepositoryMock = new Mock<IFlightRepository>();
      _airportRepositoryMock = new Mock<IAirportRepository>();
      _commentRepositoryMock = new Mock<IRepository<Comment>>();
      _options = new PriceOptions()
      {
        CharterFixed = "10",
        Charter = "2",
        LowCost = "3",
        Regular = "4"
      };
      _optionsFake = Options.Create(_options);
      _fLightManagmentService = new FLightManagmentService(_flightRepositoryMock.Object, _airportRepositoryMock.Object, _commentRepositoryMock.Object, _optionsFake, _mapperMock.Object);
      _flights = FlightListCreator.CreateFlightList();
      _comments = CommentsListCreator.CreateTestComments();

      _flightRepositoryMock.Setup(x => x.GetAll())
          .ReturnsAsync(_flights);
      _commentRepositoryMock.Setup(x => x.GetAll())
        .ReturnsAsync(_comments);
    }

    [Test]
    public async Task ListAsyncReturnedFlightList()
    {
      // Arrange
      var expectedFlight = _flights.First();

      // Act
      var result = await _fLightManagmentService.ListAsync();

      // Assert
      result.Should().NotBeNull();
      result.Should().HaveCount(_flights.Count());
      result.First().FlightNumber.Should().Be(expectedFlight.FlightNumber);
      result.First().FlightType.Should().Be(expectedFlight.FlightType);
      result.First().DepartureAirportIATA.Should().Be(expectedFlight.DepartureAirportIATA);
      result.First().ArrivalAirportIATA.Should().Be(expectedFlight.ArrivalAirportIATA);
      result.First().DepartureDateTime.Should().Be(expectedFlight.DepartureDateTime);
      result.First().ArrivalDateTime.Should().Be(expectedFlight.ArrivalDateTime);
      result.First().BasePriceNIS.Should().Be(expectedFlight.BasePriceNIS);
    }


    [Test]
    public async Task ListAsyncReturnedProperComments()
    {
      // Arrange
      var regularcomment = _comments.FirstOrDefault(s => s.FlightType == FlightType.Regular).Text;
      var lowcostcomment = _comments.FirstOrDefault(s => s.FlightType == FlightType.LowCost).Text;
      var chartercomment = _comments.FirstOrDefault(s => s.FlightType == FlightType.Charter).Text;

      // Act
      var result = await _fLightManagmentService.ListAsync();
      var regular = result.FirstOrDefault(s => s.FlightType == FlightType.Regular);
      var lowcost = result.FirstOrDefault(s => s.FlightType == FlightType.LowCost);
      var charter = result.FirstOrDefault(s => s.FlightType == FlightType.Charter);

      // Assert
      foreach (var flight in result)
      {
        flight.Comments.Should().HaveCount(1);
      }

      regular.Comments[0].Should().Be(regularcomment);
      lowcost.Comments[0].Should().Be(lowcostcomment);
      charter.Comments[0].Should().Be(chartercomment);
    }

    [Test]
    public async Task ListAsyncReturnedProperTotalPrice()
    {
      // Act
      var result = await _fLightManagmentService.ListAsync();
      var regular = result.FirstOrDefault(s => s.FlightType == FlightType.Regular);
      var lowcost = result.FirstOrDefault(s => s.FlightType == FlightType.LowCost);
      var charter = result.FirstOrDefault(s => s.FlightType == FlightType.Charter);

      // Assert
      regular.TotalPriceNIS.Should().Be(regular.BasePriceNIS * 4);
      lowcost.TotalPriceNIS.Should().Be(lowcost.BasePriceNIS * 3);
      charter.TotalPriceNIS.Should().Be((charter.BasePriceNIS * 2) + 10);
    }

    [Test]
    public void ListAsyncThrowArgumentExceptionIfAirportCodeNotExist()
    {
      // Arrange
      Filters filters = FiltersCreator.CreateDefault();
      filters.FromAirportIATACode = "YUY";
      _airportRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
      .ReturnsAsync((Airport)null);

      // Act, Assert
      Assert.That(
          async () => await _fLightManagmentService.ListAsync(filters),
          Throws.ArgumentException.With.Message.EqualTo("Searched Airport Not Exist in DB."));

    }

    [Test]
    public void ListAsyncThrowArgumentExceptionIfToAirportIATACodeNotExist()
    {
      // Arrange
      Filters filters = FiltersCreator.CreateDefault();
      filters.ToAirportIATACode = "YUY";
      _airportRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
     .ReturnsAsync((Airport)null);
     
      // Act, Assert
      Assert.That(
          async () => await _fLightManagmentService.ListAsync(filters),
          Throws.ArgumentException.With.Message.EqualTo("Searched Airport Not Exist in DB."));

    }

    [Test]
    public async Task ListAsyncReturnPagedResponseAsync()
    {
      // Arrange
      Filters filters = FiltersCreator.CreateDefault();
      filters.ToAirportIATACode = null;
      filters.FromAirportIATACode = null;

      var filtred = (_flights, 22);
      _flightRepositoryMock.Setup(x => x.GetFiltredPagedFlightsAsync(It.IsAny<Filters>()))
          .ReturnsAsync(filtred);


      // Act
      var result =  await _fLightManagmentService.ListAsync(filters);

      // Assert
      result.Data.Should().NotBeEmpty();
      result.TotalRecords.Should().Be(22);
      result.PageSize.Should().Be(2);
      result.PageNumber.Should().Be(1);
      result.PreviousPage.Should().Be(false);
      result.NextPage.Should().Be(true);
      result.TotalPages.Should().Be(11);
    }

    [Test]
    public void UpdateAsyncDoesNotThrow()
    {
      // Arrange
      var flightForUpdate = _flights.First();
      var updated = new Flight()
      {
        FlightNumber = "AB445",
        DepartureDateTime = DateTimeOffset.Now,
        ArrivalDateTime = DateTimeOffset.Now.AddHours(5),
        ArrivalAirportIATA = "AAQ",
        DepartureAirportIATA = "BHG",
        FlightType = FlightType.LowCost,
        BasePriceNIS = 100.00M,
        TotalPriceNIS = 90.00M
      };

      _flightRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
         .ReturnsAsync(flightForUpdate);

      // Assert
      Func<Task> sutMethod = async () => { await _fLightManagmentService.UpdateAsync(updated); };
      sutMethod.Should().NotThrow();
    }


    [Test]
    public void AddAsyncThrowArgumentExceptionIfAirportCodeNotExist()
    {
      // Arrange
      var flight = new Flight()
      {
        FlightNumber = "AB445",
        DepartureDateTime = DateTimeOffset.Now,
        ArrivalDateTime = DateTimeOffset.Now.AddHours(5),
        ArrivalAirportIATA = "BHG",
        DepartureAirportIATA = "YUY",
        FlightType = FlightType.LowCost,
        BasePriceNIS = 100.00M,
        TotalPriceNIS = 90.00M
      };

      // Act, Assert
      Assert.That(
          async () => await _fLightManagmentService.AddAsync(flight),
          Throws.ArgumentException.With.Message.EqualTo("Searched Airport Not Exist in DB."));

    }

    [Test]
    public void AddAsyncThrowArgumentExceptionIfToAirportIATACodeNotExist()
    {
      // Arrange
      var flight = new Flight()
      {
        FlightNumber = "AB445",
        DepartureDateTime = DateTimeOffset.Now,
        ArrivalDateTime = DateTimeOffset.Now.AddHours(5),
        ArrivalAirportIATA = "YUY",
        DepartureAirportIATA = "BHG",
        FlightType = FlightType.LowCost,
        BasePriceNIS = 100.00M,
        TotalPriceNIS = 90.00M
      };


      // Act, Assert
      Assert.That(
          async () => await _fLightManagmentService.AddAsync(flight),
          Throws.ArgumentException.With.Message.EqualTo("Searched Airport Not Exist in DB."));

    }

    [Test]
    public async Task AddGenerateFlightNumberAsync()
    {
      // Arrange
      var flight = new Flight()
      {
        FlightNumber = "AB445",
        DepartureDateTime = DateTimeOffset.Now,
        ArrivalDateTime = DateTimeOffset.Now.AddHours(5),
        ArrivalAirportIATA = "KBP",
        DepartureAirportIATA = "TLV",
        FlightType = FlightType.LowCost,
        BasePriceNIS = 100.00M,
        TotalPriceNIS = 90.00M
      };

      _airportRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
         .ReturnsAsync(new Airport { Code = "TLV", Name = "TLV" });
      
      _mapperMock.Setup(x => x.Map<Flight, Core.POCO.Flight>(It.IsAny<Flight>()))
        .Returns(_flights.First());

      // Act
      var result = await _fLightManagmentService.AddAsync(flight);

      // Assert
      result.Should().StartWith("TK");
      result.Length.Should().BeGreaterOrEqualTo(3);
    }

    [Test]
    public void UpdateAsyncThrowArgumentException()
    {
      // Arrange
      Core.POCO.Flight updated = null;
      _flightRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
         .ReturnsAsync(updated);
      
      // Act, Assert
      Assert.That(
          async () => await _fLightManagmentService.UpdateAsync(new Flight() { FlightNumber = "123"}),
          Throws.ArgumentException.With.Message.EqualTo("This Flight Number does not exist in DB."));
    }
  }
}
