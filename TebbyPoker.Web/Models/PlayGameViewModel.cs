using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TebbyPoker.GameEngine;
using TebbyPoker.Managers;
using TebbyPoker.Models;

namespace TebbyPoker.Web.Models
{
    public class PlayGameViewModel
    {
        List<Player> _players;
        public IEnumerable<Player> Players
        {
            get { return _manager.GetActivePlayers(); }
        }

        public Deck Deck { get { return _manager.GetDeck(); } }

        public Round CurrentRound { get { return _manager.GetCurrentRound(); } }

        GameManager _manager;

        public PlayGameViewModel(List<Player> players)
        {
            _manager = new GameManager(new CombinationTypeEvaluator());
            
            foreach (var player in players)
            {
                _manager.JoinGame(player);
            }

            _manager.StartNewRound();
            _manager.PerformFlop();
            _manager.PerformRiver();
            _manager.PerformTurn();

            _manager.CalculateWinners();
        }
    }
}