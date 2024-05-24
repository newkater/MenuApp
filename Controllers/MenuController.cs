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

        public async Task<IActionResult> Index()
        {
            return View(await _menuContext.Dishes.ToListAsync());
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
