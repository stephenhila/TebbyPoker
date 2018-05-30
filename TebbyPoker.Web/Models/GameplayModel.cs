using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TebbyPoker.Managers;
using TebbyPoker.Models;

namespace TebbyPoker.Web.Models
{
    public class GameplayModel
    {
        public IEnumerable<Player> Players
        {
            get { return _manager.GetPlayers(); }
        }

        IGameManager _manager;

        public GameplayModel(IGameManager manager)
        {
            this._manager = manager;
        }

        public void AddPlayer(string name)
        {
            _manager.AddPlayer(name);
        }
    }
}