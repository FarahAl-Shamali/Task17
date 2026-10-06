using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ProductCatalogSystem.Data;
using ProductCatalogSystem.Models;
using ProductCatalogSystem.Services.Email;
using ProductCatalogSystem.Services.Files;
using ProductCatalogSystem.ViewModel;

namespace ProductCatalogSystem.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        private IFileService _fileService;
        private IEmailService _emailService;
        public ProductController(ApplicationDbContext db, IFileService fileService, IEmailService emailService)
        {
            _db = db;
            _fileService = fileService;
            _emailService = emailService;
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["categoryList"] =new SelectList(_db.Categories.Where(x=> !x.IsDelete).ToList(),"Id","Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel input)
        {
            if (ModelState.IsValid)
            {
                var isExist = _db.Products.Any(x => !x.IsDelete && x.Name == input.Name);
                if (isExist)
                {
                    TempData["msg"] = "e:Product Already Exists...";
                    return View(input);
                }
                var product = new Product();
                product.Name = input.Name;
                product.Description = input.Description;
                product.CreatedAt = DateTime.Now;
                product.Price = input.Price;
                product.CategoryId = input.CategoryId;
                if (input.ImageUrl != null)
                {
                    product.ImageUrl = await _fileService.SaveFile(input.ImageUrl, "Images");
                }
                _db.Products.Add(product);
                _db.SaveChanges();
                var admins = _db.Admins.Where(x => !x.IsDelete).ToList();
                foreach (var admin in admins)
                {
                    await _emailService.Send(admin.Email, "New Product Added!", $"A new product {product.Name} was added");
                }
                TempData["msg"] = "s:Product added successfuly";
                return RedirectToAction("Index");
            }
           
            ViewData["categoryList"] = new SelectList(_db.Categories.Where(x => !x.IsDelete).ToList(), "Id", "Name");
            return View(input);
        }
        public async Task<IActionResult> Index(int page = 1, string? searchKey = null)
        {
            var query = _db.Products.Include(x => x.Category).Where(x => !x.IsDelete);

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                query = query.Where(x => x.Name.Contains(searchKey));
            }
            var noOfPages = Math.Max(1, (int)Math.Ceiling(await query.CountAsync() / 10.0));
            if (page < 1 || page > noOfPages) page = 1;

            var products = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * 10)
                .Take(10)
                .ToListAsync();

            var productVM = new List<ProductViewModel>();
            foreach (var product in products)
            {
                var productView = new ProductViewModel();
                productView.Id = product.Id;
                productView.Name = product.Name;
                productView.Description = product.Description;
                productView.Price = product.Price;        
                productView.CategoryId = product.CategoryId;
                productView.ImageUrl = product.ImageUrl;
                productVM.Add(productView);
            }

            var result = new PagingResultsViewModel<ProductViewModel>();
            result.Data = productVM;
            result.NumberOfPages = noOfPages;
            result.CurrentPage = page;
            ViewBag.searchKey = searchKey;               
            return View(result);
        }
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            product.IsDelete = true;
            _db.Products.Update(product);
            _db.SaveChanges();
            TempData["msg"] = "s: product deleted succesfully";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _db.Products.SingleOrDefault(x=> x.Id == id && !x.IsDelete);
            if(product == null)
            {
                return NotFound();
            }
            var vm = new UpdateProductViewModel();
            vm.Id = product.Id;
            vm.Name = product.Name;
            vm.Description = product.Description;
            vm.CurrentImage = product.ImageUrl;
            vm.Price = product.Price;
            ViewData["categoryList"] = new SelectList(_db.Categories.Where(x => !x.IsDelete).ToList(), "Id", "Name");
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductViewModel input)
        {
            if (ModelState.IsValid)
            {
                var product = _db.Products.SingleOrDefault(x => x.Id == input.Id && !x.IsDelete);
                if (product == null)
                {
                    return NotFound();
                }
                product.Name = input.Name;
                product.Description = input.Description;
                product.Price = input.Price;
                product.CategoryId = input.CategoryId;
                product.UpdatedAt = DateTime.Now;
                if (input.NewImage != null)                                                  
                    product.ImageUrl = await _fileService.SaveFile(input.NewImage, "Images");

                _db.Products.Update(product);
                _db.SaveChanges();
                TempData["msg"] = "s:Item Updated Successfully";
                return RedirectToAction("Index");
            }
            ViewData["categoryList"] = new SelectList(_db.Categories.Where(x => !x.IsDelete).ToList(), "Id", "Name");

            return View(input);
        } 
    }
}
