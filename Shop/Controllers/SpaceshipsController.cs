using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.Spaceship;

namespace Shop.Controllers
{
    public class SpaceshipsController : Controller
    {

        private readonly ShopContext _context;
        private readonly ISpaceshipsServices _spaceshipsServices;

        public SpaceshipsController
            (
                ShopContext context,
                ISpaceshipsServices spaceshipsServices
            )
        {
            _context = context;
            _spaceshipsServices = spaceshipsServices;
        }

        public IActionResult Index()
        {
            var result = _context.Spaceships
                .OrderByDescending(y => y.CreatedAt)
                .Select(x=>new SpaceshipIndexViewModel
                {
                    Id= x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    Passengers = x.Passengers,
                    EnginePower = x.EnginePower,
                });
            
            return View(result);
        }
        [HttpGet]
        public IActionResult Add()
        {
            SpaceshipEditViewModel spaceship = new SpaceshipEditViewModel();

            return View("Edit", spaceship);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SpaceshipEditViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Type = vm.Type,
                Crew = vm.Crew,
                Passengers = vm.Passengers,
                CargoWeight = vm.CargoWeight,
                FullTripsCount = vm.FullTripsCount,
                MaintenanceCount = vm.MaintenanceCount,
                LastMaintenance = vm.LastMaintenance,
                EnginePower = vm.EnginePower,
                MaidenLaunch = vm.MaidenLaunch,
                BuiltDate = vm.BuiltDate,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _spaceshipsServices.Add(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var spaceship = await _spaceshipsServices.GetUpdate(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            var vm = new SpaceshipEditViewModel()
            {
                Id = spaceship.Id,
                Name = spaceship.Name,
                Type = spaceship.Type,
                Crew = spaceship.Crew,
                Passengers = spaceship.Passengers,
                CargoWeight = spaceship.CargoWeight,
                FullTripsCount = spaceship.FullTripsCount,
                MaintenanceCount = spaceship.MaintenanceCount,
                LastMaintenance = spaceship.LastMaintenance,
                EnginePower = spaceship.EnginePower,
                MaidenLaunch = spaceship.MaidenLaunch,
                BuiltDate = spaceship.BuiltDate,
                CreatedAt = spaceship.CreatedAt,
                ModifiedAt = spaceship.ModifiedAt
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SpaceshipEditViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Type = vm.Type,
                Crew = vm.Crew,
                Passengers = vm.Passengers,
                CargoWeight = vm.CargoWeight,
                FullTripsCount = vm.FullTripsCount,
                MaintenanceCount = vm.MaintenanceCount,
                LastMaintenance = vm.LastMaintenance,
                EnginePower = vm.EnginePower,
                MaidenLaunch = vm.MaidenLaunch,
                BuiltDate = vm.BuiltDate,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };
            var result = await _spaceshipsServices.Update(dto);
            
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }
    }
}
