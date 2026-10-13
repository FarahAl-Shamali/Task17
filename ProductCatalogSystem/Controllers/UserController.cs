using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using ProductCatalogSystem.Data;
using ProductCatalogSystem.Models;
using ProductCatalogSystem.ViewModel;

namespace ProductCatalogSystem.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IMapper _mapper;

        public UserController(IMapper mapper, ApplicationDbContext db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var users = _db.Users.Where(x => !x.IsDelete).ToList();
            var userVm = _mapper.Map<List<User>, List<UserViewModel>>(users);
            return View(userVm);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<CreateUserViewModel, User>(input);
                user.UserName = input.Email;
                await _userManager.CreateAsync(user, input.Password);
                if (input.UserType == Enums.UserType.Admin)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else if (input.UserType == Enums.UserType.Employee)
                {
                    await _userManager.AddToRoleAsync(user, "Employee");
                }
                else if (input.UserType == Enums.UserType.Customer)
                {
                    await _userManager.AddToRoleAsync(user, "Customer");
                }
                return RedirectToAction("Index");
            }

            return View(input);
        }
        public IActionResult Delete(string Id)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == Id);
            user.IsDelete = true;
            _db.Update(user);
            _db.SaveChanges();
            return RedirectToAction("index");
        }
        public async Task<IActionResult> InitRoles()
        {
            if (!_db.Roles.Any())
            {
                var roles = new List<string>();
                roles.Add("Admin");
                roles.Add("Employee");
                roles.Add("Customer");
                foreach (var role in roles)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            return RedirectToAction("Index");
        }
    }
}