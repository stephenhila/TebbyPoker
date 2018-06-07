using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TebbyPoker.GameEngine;
using TebbyPoker.GameEngine.Contracts;
using TebbyPoker.Managers;
using TebbyPoker.Models;
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

        [HttpPost]
        public ActionResult StartGame(PlayIndexViewModel model)
        {
            List<Player> players = new List<Player>();

            foreach (var playerName in model.Players)
            {
                players.Add(new Player(playerName));
            }

            PlayGameViewModel gameModel = new PlayGameViewModel(players);

            return View("Game", gameModel);
        }
    }
}