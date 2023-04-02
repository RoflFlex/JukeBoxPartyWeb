using JukeBoxPartyWeb.Data;
using JukeBoxPartyWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace JukeBoxPartyWeb.Controllers
{
    [Authorize(Roles = "Admin, AccountManager")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        public AccountController(ApplicationDbContext context, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)

        {
            this.userManager = userManager;
            this.roleManager = roleManager;
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
                ApplicationRole role =  _context.Roles.Where(i => i.Name.Equals(userManager.GetRolesAsync(user).Result.First())).First();
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
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            
            var applicationUser = await _context.Users.FindAsync(id);

            var roles = (await userManager.GetRolesAsync(applicationUser)).ToArray();

            if (roles.Contains("Admin"))
            {
                ViewBag.ErrorMessage = $"You may not change the data of Admin";
                return RedirectToAction("Index");
            }

            if (applicationUser == null)
            {
                return NotFound();
            }

            var applicationRole = await GetRoleOfUser(applicationUser);
            Account account = new Account()
            {
                User = applicationUser,
                Role = applicationRole
            };

            ViewBag.RoleList = ToSelectList(_context.Roles.ToList(),account.Role.Id);
            return View(account);
        }

        // POST: AccountController/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Account account)
        {
            
            var user = await _context.Users.FindAsync(account.User.Id);
            
            var role = _context.Roles.Where(rol => rol.Name == account.Role.Name).First();
            
            if (role == null)
            {
                return NotFound();
            }

            var roles = (await userManager.GetRolesAsync(user)).ToArray();

            if(User.Identity.Name == account.User.UserName)
            {
                ViewBag.ErrorMessage = $"You may not change own data!";
                return View("Index");
            }

            if (roles.Contains("Admin"))
            {
                ViewBag.ErrorMessage = $"You may not change the data of Admin";
                return View("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (role.Name == "Admin")
                    {
                        if (!User.IsInRole("Admin")){
                            ViewBag.ErrorMessage = $"You may not change the role to {role.Name}";
                            return View();
                        }
                    }
                    for (int i = 0; i < roles.Length; i++)
                    {
                        await userManager.RemoveFromRoleAsync(user, roles[i]);
                    }

                    await userManager.AddToRoleAsync(user, role.Name);

                    foreach (PropertyInfo prop in user.GetType().GetProperties())
                    {
                        var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                        if (type == typeof(string) || type == typeof(int) || type == typeof(bool))
                        {
                            if (prop.GetValue(account.User, null) != null)
                                prop.SetValue(user, prop.GetValue(account.User, null));
                        }

                    }
                    await userManager.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserRoleExists(account.User.Id) || !ApplicationUserExists(account.User.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach(var error in errors)
                {
                    Debug.WriteLine(error.ErrorMessage);
                }
            }
            account.Role = await GetRoleOfUser(user);
            account.User = user;
            ViewBag.RoleList = ToSelectList(_context.Roles.ToList(), account.Role.Id);
            ViewBag.Message = "Succeded";
            return View(account);
        }

        // GET: AccountController/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            

            var applicationUser = await _context.Users.FindAsync(id);

            if (User.Identity.Name == applicationUser.UserName)
            {
                ViewBag.ErrorMessage = "You may not delete own account";
                return View("Index");
            }


            if (applicationUser == null)
            {
                return NotFound();
            }

            var applicationRole = await GetRoleOfUser(applicationUser);
            Account account = new Account()
            {
                User = applicationUser,
                Role = applicationRole
            };

            return View(account);
        }

        // POST: AccountController/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {

            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
            }
            var applicationUser = await _context.Users.FindAsync(id);
            if (applicationUser != null)
            {
                await userManager.DeleteAsync(applicationUser);
            }
            
            return RedirectToAction(nameof(Index));
            
        }

        [NonAction]
        private async Task<ApplicationRole> GetRoleOfUser(ApplicationUser user)
        {
            ApplicationRole role = new ApplicationRole();
            var roleId = _context.UserRoles.Where(user1 => user1.UserId == user.Id).First().RoleId;
            if (roleId != null) { 
                role = _context.Roles.FirstOrDefault(rol => rol.Id == roleId);
            }
            return role;

        }
        [NonAction]
        private bool ApplicationUserRoleExists(Guid userid)
        {
            return (_context.UserRoles?.Any(e => e.UserId == userid )).GetValueOrDefault();
        }
        [NonAction]
        private bool ApplicationUserExists(Guid id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [NonAction]
        public SelectList ToSelectList(List<ApplicationRole> rawList, Guid selectedId)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (ApplicationRole role in rawList)
            {
                if(selectedId == role.Id)
                {
                    list.Add(new SelectListItem()
                    {
                        Text = role.Name,
                        Value = role.Name.ToString(),
                        Selected = true
                    });
                }
                else
                {
                    list.Add(new SelectListItem()
                    {
                        Text = role.Name,
                        Value = role.Name.ToString()
                    });
                }
                
            }

            return new SelectList(list, "Value", "Text");
        }
    }
}
