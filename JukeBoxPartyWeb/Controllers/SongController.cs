using JukeBoxPartyWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting.Internal;
using System.Data;

namespace JukeBoxPartyWeb.Controllers
{
    public class SongController : Controller
    {
        // GET: SongController
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        public SongController(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            hostingEnvironment = environment;
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: SongController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SongController/Create
        public async Task<ActionResult> Create()
        {
            var list = await APICaller.GetGenres();

            ViewBag.CategoryList = ToSelectList(list);
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Song model)
        {
            // var result = RedirectToAction("Index", "Home");
            // do other validations on your model as needed
            ViewBag.Message += model.ToString();
            if (model.Track != null)
            {
                var uniqueFileName = GetUniqueFileName(model.Track.FileName);
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "media/music");
                var filePath = Path.Combine(uploads, uniqueFileName);
                await model.Track.CopyToAsync(new FileStream(filePath, FileMode.Create));
                model.URL = uniqueFileName;
                //result = RedirectToAction("Privacy","Home");
                //to do : Save uniqueFileName  to your db table   

                //try
                //{
                    await APICaller.PostSong(model);
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", model.Track.FileName);
               // }
               // catch(Exception ex)
                //{
                    //ViewBag.Message += string.Format("<b>{0}</b> not uploaded."+ex.Message+ "<br />", model.Track.FileName);
               // }
                

            }
            // to do  : Return something
            return RedirectToAction("Create");
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        // GET: SongController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SongController/Edit/5
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

        // GET: SongController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SongController/Delete/5
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
        [NonAction]
        public SelectList ToSelectList(List<Genre> rawList)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (Genre genre in rawList)
            {
                list.Add(new SelectListItem()
                {
                    Text = genre.Title,
                    Value = genre.Id.ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }
    }
}
