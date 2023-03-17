using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.SpaceshipTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Xunit;

namespace Shop.SpaceshipTest
{
	public class SpaceshipTest : TestBase
	{

		[Fact]
		public async Task ShouldNot_AddEmptySpaceship_WhenReturnResult()
		{
			string guid = Guid.NewGuid().ToString();

			SpaceshipDto spaceship = new SpaceshipDto();

			//spaceship.Id = Guid.Parse(guid);
			spaceship.Name = "asd";
			spaceship.Type = "asd";
			spaceship.Crew = 123;
			spaceship.Passengers = 123;
			spaceship.CargoWeight = 123;
			spaceship.FullTripsCount = 123;
			spaceship.MaintenanceCount = 1000;
			spaceship.LastMaintenance = DateTime.Now;
			spaceship.EnginePower = 1000;
			spaceship.MaidenLaunch = DateTime.Now;
			spaceship.BuiltDate = DateTime.Now;
			spaceship.CreatedAt = DateTime.Now;
			spaceship.ModifiedAt = DateTime.Now;

			var result = await Svc<ISpaceshipsServices>().Create(spaceship);

			Assert.NotNull(result);
		}

		[Fact]
		public async Task Should_GetByIdSpaceship_WhenReturnsEqual()
		{
			Guid databaseGuid = Guid.Parse("15b18d85-bb40-4dbe-9093-cc910f22df43");
			Guid getGuid = Guid.Parse("15b18d85-bb40-4dbe-9093-cc910f22df43");
			await Svc<ISpaceshipsServices>().GetAsync(getGuid);

			Assert.Equal(databaseGuid, getGuid);

		}

		[Fact]
		public async Task ShouldNot_GetByIdSpaceship_WhenReturnsNotEqual()
			//keegi teeb vale id-ga päringu
		{
			//Arrange-paikapanemine, 
			Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString());
			Guid guid = Guid.Parse("15b18d85-bb40-4dbe-9093-cc910f22df43");

			//Act
			await Svc<ISpaceshipsServices>().GetAsync(guid);

			//Assert
			Assert.NotEqual(wrongGuid, guid);
		}

		[Fact]
		public async Task Should_DeleteByIdSpaceship_WhenDeleteSpaceship()
		{
			Guid guid = Guid.Parse("15b18d85-bb40-4dbe-9093-cc910f22df43");

			SpaceshipDto spaceship = new SpaceshipDto();

			spaceship.Id = Guid.Parse("15b18d85-bb40-4dbe-9093-cc910f22df43");
			spaceship.Name = "asd";
			spaceship.Type = "asd";
			spaceship.Crew = 123;
			spaceship.Passengers = 123;
			spaceship.CargoWeight = 123;
			spaceship.FullTripsCount = 123;
			spaceship.MaintenanceCount = 1000;
			spaceship.LastMaintenance = DateTime.Now;
			spaceship.EnginePower = 1000;
			spaceship.MaidenLaunch = DateTime.Now;
			spaceship.BuiltDate = DateTime.Now;
			spaceship.CreatedAt = DateTime.Now;
			spaceship.ModifiedAt = DateTime.Now;

			await Svc<ISpaceshipsServices>().Delete(guid);

			Assert.Equal(guid, spaceship.Id);



		}
	}
}