using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CRUDelicious.Models;

namespace CRUDelicious.Controllers
{
    public class MainController : Controller
    {

        private DishContext database;
        public MainController(DishContext context)
        {
            database = context;
        }
        
        [HttpGet("")]
        public IActionResult index()
        {
            List<Dish> dishes = database.Dishes.ToList();
            return View(dishes);
        }

        [HttpGet("new")]
        public IActionResult NewDish()
        {
            return View();
        }

        [HttpPost("addDish")]
        public IActionResult AddDish(Dish post_dish)
        {
            database.Add(post_dish);
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("{dish_id}")]
        public IActionResult ViewDish(int dish_id)
        {
            Dish dish = database.Dishes.FirstOrDefault(d => d.DishID == dish_id);
            return View(dish);
        }

        [HttpPost("delete")]
        public IActionResult DeleteDish(int id_from_form)
        {
            Dish dish = database.Dishes.FirstOrDefault(d => d.DishID == id_from_form);

            database.Dishes.Remove(dish);
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("edit/{dish_id}")]
        public IActionResult EditDish(int dish_id)
        {
            Dish dish = database.Dishes.FirstOrDefault(d => d.DishID == dish_id);

            return View(dish);

        }

        [HttpPost("updateDish")]
        public IActionResult UpdateDish(Dish post_dish)
        {
            Dish dish = database.Dishes.FirstOrDefault(d => d.DishID == post_dish.DishID);

            dish.Name = post_dish.Name;
            dish.Chef = post_dish.Chef;
            dish.Tastiness = post_dish.Tastiness;
            dish.Calories = post_dish.Calories;
            dish.Description = post_dish.Description;

            database.SaveChanges();
            
            return Redirect($"{post_dish.DishID}");
        }
    }
}