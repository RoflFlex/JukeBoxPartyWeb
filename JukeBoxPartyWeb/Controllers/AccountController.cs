using JukeBoxPartyWeb.Data;
using JukeBoxPartyWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JukeBoxPartyWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            
            _context = context;
        }
        // GET: AccountController
        public async Task<ActionResult> Index()
        {
            if (_context.Users == null ||_context.Roles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
            }
            List<Account> accounts = new List<Account>();
            var users = await _context.Users.ToListAsync();
            var userRoles = await _context.UserRoles.ToListAsync();
            users.ForEach(async user =>
            {
                ApplicationRole role =  await GetRoleOfUser(user);
                accounts.Add(new Account()
                {
                    User = user,
                    Role = role
                }) ;
            });

            return View(accounts);
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        private async Task<ApplicationRole> GetRoleOfUser(ApplicationUser user)
        {
            ApplicationRole role = new ApplicationRole();
            var roleId = _context.UserRoles.FirstOrDefault(roleuser=> roleuser.UserId == user.Id).RoleId;
            if (roleId != null) { 
                role = _context.Roles.FirstOrDefault(rol => rol.Id == roleId);
            }
            return role;

        }
    }
}
