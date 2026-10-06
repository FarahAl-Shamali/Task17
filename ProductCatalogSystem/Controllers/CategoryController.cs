using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ProductCatalogSystem.Data;
using ProductCatalogSystem.Models;
using ProductCatalogSystem.ViewModel;

namespace ProductCatalogSystem.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index(int page = 1, string? searchKey = null)
        {
            var query = _db.Categories.Where(x => !x.IsDelete);

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                query = query.Where(x => x.Name.Contains(searchKey));
            }
            var noOfPages = Math.Max(1, (int)Math.Ceiling(await query.CountAsync() / 10.0));
            if (page < 1 || page > noOfPages) page = 1;

            var categories = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * 10)
                .Take(10)
                .ToListAsync();

            var categoryVM = new List<CategoryViewModel>();
            foreach (var category in categories)
            {
                var categoryView = new CategoryViewModel();
                categoryView.Name = category.Name;
                categoryView.Id = category.Id;
                categoryVM.Add(categoryView);
            }

            var result = new PagingResultsViewModel<CategoryViewModel>();
            result.Data = categoryVM;
            result.NumberOfPages = noOfPages;
            result.CurrentPage = page;
            ViewBag.searchKey = searchKey;
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateCategoryViewModel input)
        {
            if (ModelState.IsValid)
            {
                var isExist = _db.Categories.Any(x => !x.IsDelete && x.Name == input.Name);
                if (isExist)
                {
                    TempData["msg"] = "e: Category Already Exists";
                    return View(input);
                }
                var category = new Category()
                {
                    Name = input.Name,
                };
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["msg"] = "s: Category Added Successfuly";
                return RedirectToAction("Index");
            }
            return View(input);
        }
        public IActionResult Delete(int id)
        {
            var category = _db.Categories.Find(id);
            category.IsDelete = true;
            _db.Categories.Update(category);
            _db.SaveChanges();
            TempData["msg"] = "s: Category Deleted Successfuly";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var category = _db.Categories.SingleOrDefault(x => x.Id == id && !x.IsDelete);
            if (category == null)
            {
                return NotFound();
            }
            var vm = new UpdateCategoryViewModel();
            vm.Id = category.Id;
            vm.Name = category.Name;
            return View(vm);
        }
        [HttpPost]
        public IActionResult Update(UpdateCategoryViewModel input) 
        {
            if (ModelState.IsValid)
            {
                var category = _db.Categories.SingleOrDefault(x => x.Id == input.Id && !x.IsDelete);
                if (category == null)
                {
                    return NotFound();
                }
                category.Name = input.Name;
                category.Id = input.Id;
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["msg"] = "s:Item Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(input);
            }
    }
}
