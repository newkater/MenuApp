using MenuApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MenuApp.Data
{
    public class MenuContext : DbContext
    {
        public MenuContext(DbContextOptions<MenuContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>().Property(d => d.Price).HasPrecision(12, 2);

            modelBuilder.Entity<DishIngredient>().HasKey(di => new
                {
                    di.DishId,
                    di.IngredientId
                });

            modelBuilder.Entity<Dish>().HasData(
                new Dish { Id = 1, Name = "Lasagna", Price= 8.90M, ImageUrl= "https://assets.bonappetit.com/photos/656f48d75b552734225041ba/1:1/w_3129,h_3129,c_limit/20231120-WEB-Lasanga-6422.jpg" },
                new Dish { Id = 2, Name = "Spaghetti Bolognese", Price = 7.50M, ImageUrl = "https://img.taste.com.au/5qlr1PkR/taste/2016/11/spaghetti-bolognese-106560-1.jpeg" }
                );
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id=1, Name= "Lasagna noodles" },
                new Ingredient { Id = 2, Name = "Tomato sause" },
                new Ingredient { Id = 3, Name = "Mozzarella" },
                new Ingredient { Id = 4, Name = "Spaghetti" },
                new Ingredient { Id = 5, Name = "Bolognese sause" }
                );
            modelBuilder.Entity<DishIngredient>().HasData(
                new DishIngredient { DishId = 1, IngredientId = 1 },
                new DishIngredient { DishId = 1, IngredientId = 2 },
                new DishIngredient { DishId = 1, IngredientId = 3 },
                new DishIngredient { DishId = 2, IngredientId = 4 },
                new DishIngredient { DishId = 2, IngredientId = 5 },
                new DishIngredient { DishId = 2, IngredientId = 3 }
                );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
    }
}
