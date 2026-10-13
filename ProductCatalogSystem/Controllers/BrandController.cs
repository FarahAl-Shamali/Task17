using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProductCatalogSystem.Data;
using ProductCatalogSystem.Models;
using ProductCatalogSystem.ViewModel;

namespace ProductCatalogSystem.Controllers
{
    [Authorize(Roles ="Employee")]
    public class BrandController : Controller
    {
        private ApplicationDbContext _db;
        private IMapper _map;
        public BrandController(ApplicationDbContext db, IMapper map)
        {
            _db = db;
            _map = map;
        }
        public IActionResult Index()
        {
            var brands = _db.Brands.Where(x=> !x.IsDelete).ToList();
            var brandVm = _map.Map<List<Brand>, List<BrandViewModel>>(brands);
            return View(brandVm);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateBrandViewModel input)
        {
            if (ModelState.IsValid)
            {
                var isExist = _db.Brands.Any(x => x.Name == input.Name && !x.IsDelete);
                if (isExist)
                {
                    TempData["msg"] = "e: Brand Already Exists";
                    return View(input);
                }
                var brand = new Brand()
                {
                    Name = input.Name,
                };
                _db.Brands.Add(brand);
                _db.SaveChanges();
                TempData["msg"] = "s: Brand Added Successfuly";
                return RedirectToAction("Index");
            }
            return View(input);
        }
        public IActionResult Delete(int id)
        {
            var brand = _db.Brands.Find(id);
            brand.IsDelete = true;
            _db.Brands.Update(brand);
            _db.SaveChanges();
            TempData["msg"] = "s: Brand Deleted Successfuly";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var brand = _db.Brands.SingleOrDefault(x => x.Id == id && !x.IsDelete);
            if(brand == null)
            {
                return NotFound();
            }
            var vm = new UpdateBrandViewModel();
            vm.Id = brand.Id;
            vm.Name = brand.Name;
            return View(vm);

        }
        [HttpPost]
        public IActionResult Update(UpdateBrandViewModel input)
        {
            if (ModelState.IsValid)
            {
                var brand = _db.Brands.SingleOrDefault(x => x.Id == input.Id && !x.IsDelete);
                if (brand == null)
                {
                    return NotFound();
                }
                brand.Name = input.Name;
                brand.Id = input.Id;
                _db.Brands.Update(brand);
                _db.SaveChanges();
                TempData["msg"] = "s:brand Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(input);
        }

    }
}