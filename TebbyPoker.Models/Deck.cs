using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TebbyPoker.Models
{
    public class Deck
    {
        List<Card> _cards;
        public List<Card> Cards { get { return _cards; } }

        public Deck()
        {
            _cards = new List<Card>();
            foreach (var suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (var rank in Enum.GetValues(typeof(Rank)))
                {
                    _cards.Add(new Card((Rank)rank, (Suit)suit));
                }
            }
        }

        public void Shuffle()
        {
            // perform Fisher-Yates shuffle algorithm
            for (int i = _cards.Count - 1; i > 0; i--)
            {
                Random random = new Random();
                int swapIndex = random.Next(0, i);
                var tmp = _cards[swapIndex];
                _cards[swapIndex] = _cards[i];
                _cards[i] = tmp;
            }
        }

        public void Shuffle(int timesToShuffle)
        {
            for (int i = 0; i < timesToShuffle; i++)
            {
                Shuffle();
            }
        }

        public Card Draw()
        {
            Card card = _cards.FirstOrDefault();
            _cards.Remove(card);
            return card;
        }
    }
}
