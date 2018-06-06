using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TebbyPoker.GameEngine;
using TebbyPoker.GameEngine.Contracts;
using TebbyPoker.Managers;
using TebbyPoker.Web.Models;

namespace TebbyPoker.Web.Controllers
{
    public class PlayController : Controller
    {
        public ActionResult Index()
        {
            PlayIndexViewModel model = new PlayIndexViewModel();

            return Index(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PlayIndexViewModel model)
        {
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPlayer(PlayIndexViewModel model)
        {
            model.Players.Add(model.PlayerToAdd);
            return Index(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePlayer(PlayIndexViewModel model, string player)
        {
            model.Players.Remove(player);
            return Index(model);
        }
    }
}