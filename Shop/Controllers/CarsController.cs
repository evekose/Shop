using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IFilesServices _filesServices;

        public CarsController
            (
                ShopContext context,
                ICarsServices carsServices,
                IFilesServices filesServices
            )
        {
            _context = context;
            _carsServices = carsServices;
            _filesServices = filesServices;
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
        public IActionResult Create()
        {
            CarCreateUpdateViewModel car = new CarCreateUpdateViewModel();

            return View("CreateUpdate", car);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarCreateUpdateViewModel vm)
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
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    CarId = x.CarId,
                }).ToArray()
            };

            var result = await _carsServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var car = await _carsServices.GetAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabase
                .Where(x => x.CarId == id)
                .Select(y => new ImageViewModel
                {
                    CarId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();


            var vm = new CarCreateUpdateViewModel();
            {
                vm.Id = car.Id;
                vm.Brand = car.Brand;
                vm.Model = car.Model;
                vm.Color = car.Color;
                vm.NameOfOwner = car.NameOfOwner;
                vm.Price = car.Price;
                vm.Weight = car.Weight;
                vm.EnginePower = car.EnginePower;
                vm.Mileage = car.Mileage;
                vm.BuiltDate = car.BuiltDate;
                vm.MaintanceDate = car.MaintanceDate;
                vm.Image.AddRange(photos);

                return View("CreateUpdate", vm);
            }
        }
            

                [HttpPost]
                public async Task<IActionResult> Update(CarCreateUpdateViewModel vm)
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
                    Files = vm.Files,
                    Image = vm.Image.Select(x => new FileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        CarId = x.CarId,
                    }).ToArray()
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

                var photos = await _context.FileToDatabase
                    .Where(x => x.CarId == id)
                    .Select(y => new ImageViewModel
                    {
                        CarId = y.Id,
                        ImageId = y.Id,
                        ImageData = y.ImageData,
                        ImageTitle = y.ImageTitle,
                        Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                    }).ToArrayAsync();

                var vm = new CarDetailsViewModel();

                vm.Id = car.Id;
                vm.Brand = car.Brand;
                vm.Model = car.Model;
                vm.Color = car.Color;
                vm.NameOfOwner = car.NameOfOwner;
                vm.Price = car.Price;
                vm.Weight = car.Weight;
                vm.EnginePower = car.EnginePower;
                vm.Mileage = car.Mileage;
                vm.BuiltDate = car.BuiltDate;
                vm.MaintanceDate = car.MaintanceDate;
                vm.Image.AddRange(photos);


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

                var photos = await _context.FileToDatabase
                    .Where(x => x.CarId == id)
                    .Select(y => new ImageViewModel
                    {
                        CarId = y.Id,
                        ImageId = y.Id,
                        ImageData = y.ImageData,
                        ImageTitle = y.ImageTitle,
                        Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                    }).ToArrayAsync();

                var vm = new CarDeleteViewModel();

                vm.Id = car.Id;
                vm.Brand = car.Brand;
                vm.Model = car.Model;
                vm.Color = car.Color;
                vm.NameOfOwner = car.NameOfOwner;
                vm.Price = car.Price;
                vm.Weight = car.Weight;
                vm.EnginePower = car.EnginePower;
                vm.Mileage = car.Mileage;
                vm.BuiltDate = car.BuiltDate;
                vm.MaintanceDate = car.MaintanceDate;
                vm.Image.AddRange(photos);

                return View(vm);
            }

            [HttpPost]
            public async Task<IActionResult> RemoveImage(ImageViewModel file)
            {
                var dto = new FileToDatabaseDto()
                {
                    Id = file.ImageId
                };

                var image = await _filesServices.RemoveImage(dto);

                if (image == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Index));

            }

        }
    }
