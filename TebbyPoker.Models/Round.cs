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

        public void PerformFlop(Deck deck)
        {
            deck.Discard();
            Flop.Add(deck.Draw());
            Flop.Add(deck.Draw());
            Flop.Add(deck.Draw());
        }

        public void PerformRiver(Deck deck)
        {
            deck.Discard();
            _river = deck.Draw();
        }

        public void PerformTurn(Deck deck)
        {
            deck.Discard();
            _turn = deck.Draw();
        }

        public Player GetWinner()
        {
            Player winner = null;

            List<Card> shownCards = new List<Card>();
            shownCards.AddRange(_flop);
            shownCards.Add(_river);
            shownCards.Add(_turn);


            throw new NotImplementedException();

            _winner = winner;
            return winner;
        }
    }
}
