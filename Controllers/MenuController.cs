using MenuApp.Data;
using MenuApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MenuApp.Controllers
{
    public class MenuController : Controller
    {
        private readonly MenuContext _menuContext;

        public MenuController(MenuContext menuContext)
        {
            _menuContext = menuContext;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var dishes = _menuContext.Dishes.Select(x => x);
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                dishes = dishes.Where(d => d.Name.Contains(searchString));
            }
            return View(await dishes.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            if (await _menuContext.Dishes
                .Include(d => d.DishIngredients)
                .ThenInclude(di => di.Ingredient)
                .FirstOrDefaultAsync(d => d.Id == id) is not Dish dish)
            {
                return NotFound();
            }
            return View(dish);
        }
    }
}
