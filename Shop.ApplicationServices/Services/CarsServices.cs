using Microsoft.EntityFrameworkCore;
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
    public class CarServices : ICarsServices
    {
        private readonly ShopContext _context;
        public CarServices
            (
            ShopContext context
            )
        {
            _context = context;
        }

        public async Task<Car> Add(CarDto dto)
        {
            var domain = new Car()
            {
                Id = Guid.NewGuid(),
                Brand = dto.Brand,
                Model = dto.Model,
                Color = dto.Color,
                NameOfOwner = dto.NameOfOwner,
                Price = dto.Price,
                Weight = dto.Weight,
                EnginePower = dto.EnginePower,
                Mileage = dto.Mileage,
                BuiltDate = DateTime.Now,
                MaintanceDate = DateTime.Now,
            };

            await _context.Cars.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;
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
                MaintanceDate = DateTime.Now,
            };

            _context.Cars.Update(domain);
            await _context.SaveChangesAsync();
            return domain;


        }
        public async Task<Car> GetUpdate(Guid id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Car> Delete(Guid id)
        {
            var carId = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

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
