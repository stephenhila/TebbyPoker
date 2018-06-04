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
            ICombinationTypeEvaluator combinationTypeEvaluator = new CombinationTypeEvaluator();
            GameplayModel model = new GameplayModel(new GameManager(combinationTypeEvaluator));

            return Index(model);
        }

        [HttpPost]
        public ActionResult Index(GameplayModel model)
        {
            return View(model);
        }

        public ActionResult AddPlayer(GameplayModel model)
        {
#warning TODO: Implement adding of players. Think of way to have a view for starting the game (ie. adding players), and then a view for the gameplay action itself. Consider if there is such a thing as sub-views in MVC.
            throw new NotImplementedException();
            // model.AddPlayer()
            return Index(model);
        }
    }
}