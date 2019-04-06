using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Library.Data;
using Library.Models;

namespace Library.Controllers
{
    public class LibraryController : Controller
    {
        public IActionResult Index()
        {
			using (var db = new LibraryDbContext())
			{
				var allTasks = db.Books.ToList();
				return View(allTasks);
			}
		}

        [HttpGet]
        public IActionResult Create()
        {
			return this.View();
		}

        [HttpPost]
        public IActionResult Create(Book book)
        {
			if (double.IsNaN(book.Price) || book.Price <=1)
			{
				return RedirectToAction("Index");
			}
			using (var db = new LibraryDbContext())
			{
				db.Books.Add(book);
				db.SaveChanges();
			}
			return RedirectToAction("Index");
		}

        [HttpGet]
        public IActionResult Edit(int id)
        {
			using (var db = new LibraryDbContext())
			{
				var taskToEdit = db.Books.FirstOrDefault(t => t.Id == id);
				if (taskToEdit == null)
				{
					return RedirectToAction("Index");
				}
				return this.View(taskToEdit);
			}
		}

        [HttpPost]
        public IActionResult Edit(Book book)
        {
			using (var db = new LibraryDbContext())
			{
				var taskToEdit = db.Books.FirstOrDefault(t => t.Id == book.Id);
				taskToEdit.Title = book.Title;
				taskToEdit.Author = book.Author;
				taskToEdit.Price = book.Price;
				db.SaveChanges();
			}
			return RedirectToAction("Index");
		}

        [HttpGet]
        public IActionResult Delete(int id)
        {
			using (var db = new LibraryDbContext())
			{
				Book bookDetails = db.Books.FirstOrDefault(t => t.Id == id);
				if (bookDetails == null)
				{
					return RedirectToAction("Index");
				}
				return View(bookDetails);
			}
		}

        [HttpPost]
        public IActionResult Delete(Book book)
        {
			using (var db = new LibraryDbContext())
			{
				var bookToDelete = db.Books.FirstOrDefault(t => t.Id == book.Id);
				if (bookToDelete == null)
				{
					RedirectToAction("Index");
				}
				db.Books.Remove(bookToDelete);
				db.SaveChanges();

			}
			return RedirectToAction("Index");
		}
    }
}