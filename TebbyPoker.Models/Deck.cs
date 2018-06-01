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

        List<Card> _discardedCards;
        public List<Card> DiscardedCards { get { return _discardedCards; } }

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

            _discardedCards = new List<Card>();
        }

        /// <summary>
        /// Shuffles the deck once.
        /// </summary>
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

        /// <summary>
        /// Shuffles the deck by a specified amount.
        /// </summary>
        /// <param name="timesToShuffle">Number of times to perform shuffling.</param>
        public void Shuffle(int timesToShuffle)
        {
            for (int i = 0; i < timesToShuffle; i++)
            {
                Shuffle();
            }
        }

        /// <summary>
        /// Draws the top card.
        /// </summary>
        /// <returns></returns>
        public Card Draw()
        {
#warning TODO: could have used stack instead of a list.
            Card card = _cards.FirstOrDefault();
            _cards.Remove(card);
            return card;
        }

        /// <summary>
        /// Draws a card with specidied rank and suit.
        /// </summary>
        /// <param name="suit"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        public Card Draw(Suit suit, Rank rank)
        {
            Card card = _cards.FirstOrDefault(c => c.Suit == suit && c.Rank == rank);
            _cards.Remove(card);
            return card;
        }

        /// <summary>
        /// Discards the top card.
        /// </summary>
        public void Discard()
        {
            Card card = Cards.FirstOrDefault();
            Discard(card);
        }

        /// <summary>
        /// Discards a specific card.
        /// </summary>
        /// <param name="card"></param>
        public void Discard(Card card)
        {
            _cards.Remove(card);
            if (!_discardedCards.Contains(card))
            {
                _discardedCards.Add(card);
            }
        }

        /// <summary>
        /// Discards a specified list of cards. Can be used when a player folds so that cards are discarded.
        /// </summary>
        /// <param name="cards"></param>
        public void Discard(List<Card> cards)
        {
            cards.ForEach(card => Discard(card));
        }

        /// <summary>
        /// Re-adds the discard pile back into the deck.
        /// </summary>
        public void ReshuffleDiscarded()
        {
            _cards.AddRange(_discardedCards);
            _discardedCards.Clear();
        }
    }
}
