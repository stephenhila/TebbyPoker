using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TebbyPoker.Models
{
    public class Round
    {
        Player _winner;
        public Player Winner { get { return _winner; } }

        List<Player> _players;
        public List<Player> Players { get { return _players; } }

        List<Card> _flop;
        public List<Card> Flop { get { return _flop; } }

        Card _river;
        public Card River { get { return _river; } }

        Card _turn;
        public Card Turn { get { return _turn; } }

        public Round(List<Player> players)
        {
            _players = players;
        }

        public void EvaluateWinner()
        {
            throw new NotImplementedException();
        }
    }
}
