using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TebbyPoker.Models
{
    [Table("Players")]
    public class Player
    {
        [Key()]
        Guid _id;
        public Guid Id { get { return _id; } }

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
        /// Gets a card from the specified deck.
        /// </summary>
        /// <param name="deck"></param>
        public void GetCard(Deck deck)
        {
            _hand.Add(deck.Draw());
        }

        /// <summary>
        /// Peculiarly gets a specific card from the specified deck.
        /// </summary>
        /// <param name="deck"></param>
        /// <param name="suit"></param>
        /// <param name="rank"></param>
        public void GetCard(Deck deck, Suit suit, Rank rank)
        {
            Hand.Add(deck.Draw(suit, rank));
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

            _hand.Clear();
        }
    }
}
