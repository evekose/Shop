using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.Car;

namespace Shop.Controllers
{
    public class CarsController : Controller
    {

        private readonly ShopContext _context;
        private readonly ICarsServices _carsServices;

        public CarsController
            (
                ShopContext context,
                ICarsServices carsServices
            )
        {
            _context = context;
            _carsServices = carsServices;
        }

        public IActionResult Index()
        {
            var result = _context.Cars
                .OrderByDescending(y => y.BuiltDate)
                .Select(x => new CarIndexViewModel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    Color = x.Color,
                    NameOfOwner = x.NameOfOwner,
                    Price = x.Price,
                    Weight = x.Weight,
                    EnginePower = x.EnginePower,
                    Mileage = x.Mileage,
                });

            return View(result);
        }
        [HttpGet]
        public IActionResult Add()
        {
            CarEditViewModel car = new CarEditViewModel();

            return View("Edit", car);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CarEditViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = vm.Id,
                Brand = vm.Brand,
                Model = vm.Model,
                Color = vm.Color,
                NameOfOwner = vm.NameOfOwner,
                Price = vm.Price,
                Weight = vm.Weight,
                EnginePower = vm.EnginePower,
                Mileage = vm.Mileage,
                BuiltDate = vm.BuiltDate,
                MaintanceDate = vm.MaintanceDate,
            };

            var result = await _carsServices.Add(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var car = await _carsServices.GetUpdate(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarEditViewModel()
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Color = car.Color,
                NameOfOwner = car.NameOfOwner,
                Price = car.Price,
                Weight = car.Weight,
                EnginePower = car.EnginePower,
                Mileage = car.Mileage,
                BuiltDate = car.BuiltDate,
                MaintanceDate = car.MaintanceDate,
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarEditViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = vm.Id,
                Brand = vm.Brand,
                Model = vm.Model,
                Color = vm.Color,
                NameOfOwner = vm.NameOfOwner,
                Price = vm.Price,
                Weight = vm.Weight,
                EnginePower = vm.EnginePower,
                Mileage = vm.Mileage,
                BuiltDate = vm.BuiltDate,
                MaintanceDate = vm.MaintanceDate,
            };
            var result = await _carsServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var carId = await _carsServices.Delete(id);
            if (carId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _carsServices.GetAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarDetailsViewModel()
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Color = car.Color,
                NameOfOwner = car.NameOfOwner,
                Price = car.Price,
                Weight = car.Weight,
                EnginePower = car.EnginePower,
                Mileage = car.Mileage,
                BuiltDate = car.BuiltDate,
                MaintanceDate = car.MaintanceDate,
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _carsServices.GetAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarDeleteViewModel()
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Color = car.Color,
                NameOfOwner = car.NameOfOwner,
                Price = car.Price,
                Weight = car.Weight,
                EnginePower = car.EnginePower,
                Mileage = car.Mileage,
                BuiltDate = car.BuiltDate,
                MaintanceDate = car.MaintanceDate,
            };

            return View(vm);
        }

    }
}
