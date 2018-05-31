using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TebbyPoker.Models
{
    public class Player
    {
        string _name;
        public string Name { get { return _name; } }

        List<Card> _hand;
        public List<Card> Hand { get { return _hand; } }

        public Player(string name)
        {
            this._name = name;

            _hand = new List<Card>();
        }

        /// <summary>
        /// When a player gives up, places the cards back into the deck's discard pile.
        /// </summary>
        /// <param name="deck"></param>
        public void Fold(Deck deck)
        {
            foreach (var card in _hand)
            {
                deck.Discard(card);
            }
        }
    }
}
