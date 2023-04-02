using JukeBoxPartyWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JukeBoxPartyWeb.Controllers
{
    [Authorize]
    public class LobbiesController : Controller
    {
        // GET: LobbyController
        public ActionResult Index()
        {
            List<Lobby> lobbies = APICaller.GetLobbies().Result;
            for(int i = 0; i < lobbies.Count; i++)
            {
                lobbies[i].IdToShow = i+1; 
            }

            return View(lobbies);
        }

        // GET: LobbyController/Lobby/5
        public ActionResult Lobby(Guid id)
        {
            List<Lobby> lobbies = APICaller.GetLobbies().Result;
            if (lobbies.Find(lobby => lobby.Id == id) != null)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // GET: LobbyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LobbyController/Create
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

        // GET: LobbyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LobbyController/Edit/5
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

        // GET: LobbyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LobbyController/Delete/5
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
    }
}
