using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Domain.Car;
using Shop.Core.Domain.Spaceship;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.ApplicationServices.Services
{
    public class CarsServices : ICarsServices
    {
        private readonly ShopContext _context;
		private readonly IFilesServices _files;
		public CarsServices
            (
            ShopContext context,
			IFilesServices files
			)
        {
            _context = context;
			_files = files;
		}

		public async Task<Car> Create(CarDto dto)
		{
			Car car = new Car();
			FileToDatabase file = new FileToDatabase();

            car.Id = Guid.NewGuid();
            car.Brand = dto.Brand;
            car.Model = dto.Model;
            car.Color = dto.Color;
            car.NameOfOwner = dto.NameOfOwner;
            car.Price = dto.Price;
            car.Weight = dto.Weight;
            car.EnginePower = dto.EnginePower;
            car.Mileage = dto.Mileage;
            car.BuiltDate = dto.BuiltDate;
            car.MaintanceDate = dto.MaintanceDate;

			if (dto.Files != null)
			{
				_files.UploadFilesToDatabase(dto, car);
			}

			await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return car;
        }

        public async Task<Car> Update(CarDto dto)
        {
            var domain = new Car()
            {
                Id = dto.Id,
                Brand = dto.Brand,
                Model = dto.Model,
                Color = dto.Color,
                NameOfOwner = dto.NameOfOwner,
                Price = dto.Price,
                Weight = dto.Weight,
                EnginePower = dto.EnginePower,
                Mileage = dto.Mileage,
                BuiltDate = dto.BuiltDate,
                MaintanceDate = dto.MaintanceDate,
            };
			if (dto.Files != null)
			{
				_files.UploadFilesToDatabase(dto, domain);
			}

			_context.Cars.Update(domain);
            await _context.SaveChangesAsync();
            return domain;


        }
		//public async Task<Car> GetUpdate(Guid id)
		//{
		//    var result = await _context.Cars
		//        .FirstOrDefaultAsync(x => x.Id == id);

		//    return result;
		//}

		public async Task<Car> Delete(Guid id)
		{
			var carId = await _context.Cars
				.FirstOrDefaultAsync(x => x.Id == id);

			var images = await _context.FileToDatabase
				.Where(x => x.CarId == id)
				.Select(y => new FileToDatabaseDto
				{
					Id = y.Id,
					ImageTitle = y.ImageTitle,
					CarId = y.CarId,
				})
				.ToArrayAsync();

			await _files.RemoveImagesFromDatabase(images);
			_context.Cars.Remove(carId);
			await _context.SaveChangesAsync();

			return carId;
		}

		public async Task<Car> GetAsync(Guid id)
		{
			var result = await _context.Cars
				.FirstOrDefaultAsync(x => x.Id == id);

			return result;
		}
	}
}