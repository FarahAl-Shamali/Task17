using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using ProductCatalogSystem.Data;
using ProductCatalogSystem.Models;
using ProductCatalogSystem.ViewModel;

namespace ProductCatalogSystem.Controllers
{
    public class ReviewController : Controller
    {
        private ApplicationDbContext _db;
        private IMapper _mapper;
        public ReviewController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public IActionResult Index(int productId)
        {
            var reviews = _db.Reviews.Where(x=> x.ProductId == productId && !x.IsDelete).ToList();
            var reviewsVm = _mapper.Map<List<Review>, List<ReviewViewModel>>(reviews);
            ViewBag.productId = productId;
            return View(reviewsVm);
        }

        [HttpGet]
        [HttpGet]
        public IActionResult Create(int productId)
        {
            return View(new CreateReviewViewModel { ProductId = productId });
        }

        [HttpPost]
        public IActionResult Create(CreateReviewViewModel input)
        {
            if (ModelState.IsValid)
            {
                var isExists = _db.Products.Any(x => x.Id == input.ProductId && !x.IsDelete);
                if (!isExists)
                {
                    return NotFound();
                }
                var review = new Review()
                {
                    Description = input.Description,
                    Rating = input.Rating,
                    ProductId = input.ProductId
                };
                _db.Reviews.Add(review);
                _db.SaveChanges();
                TempData["msg"] = "s: Review Added Successfuly";
                //because the index has as id parameter so  we want tu redirect it to the index and the same product review page
                return RedirectToAction("Index", new { productId = input.ProductId });
            }
            return View(input);
        }
        public IActionResult Delete(int id)
        {
            var review = _db.Reviews.Find(id);
            review.IsDelete = true;
            _db.Reviews.Update(review);
            _db.SaveChanges();
            TempData["msg"] = "s: review Deleted Successfuly";
            return RedirectToAction("Index", new { productId = review.ProductId });
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var review = _db.Reviews.SingleOrDefault(x => x.Id == id && !x.IsDelete);
            if (review == null)
            {
                return NotFound();
            }
            var vm = new UpdateReviewViewModel();
            vm.Id = review.Id;
            vm.ProductId = review.ProductId;
            vm.Rating = review.Rating;
            vm.Description = review.Description;
            return View(vm);

        }
        [HttpPost]
        public IActionResult Update(UpdateReviewViewModel input)
        {
            if (ModelState.IsValid)
            {
                var review = _db.Reviews.SingleOrDefault(x => x.Id == input.Id && !x.IsDelete);
                if (review == null)
                {
                    return NotFound();
                }
                review.Rating = input.Rating;
                review.Description = input.Description;
                review.Id = input.Id;
                _db.Reviews.Update(review);
                _db.SaveChanges();
                TempData["msg"] = "s:review Updated Successfully";
                return RedirectToAction("Index", new { productId = review.ProductId });
            }
            return View(input);
        }
    }
}
