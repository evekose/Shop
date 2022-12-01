using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models.Spaceship;

namespace Shop.Controllers
{
    public class SpaceshipsController : Controller
    {

        private readonly ShopContext _context;

        public SpaceshipsController
            (
                ShopContext context
            )
        {
            _context = context;
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

        public IActionResult Add()
        {

            return View("Edit");
        }
    }
}
