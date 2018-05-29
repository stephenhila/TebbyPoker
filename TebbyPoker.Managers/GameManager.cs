using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TebbyPoker.Models;

namespace TebbyPoker.Managers
{
    public class GameManager
    {
        List<Player> _players;
        public List<Player> Players { get { return _players; } }

        Deck _deck;
        public Deck Deck { get { return _deck; } }

        List<Card> _discardedCards;

        List<Card> _revealedCards;
        public List<Card> RevealedCards { get { return _revealedCards; } }

        public GameManager(List<Player> players)
        {
            this._players = players;
            this._deck = new Deck();
            this._deck.Shuffle(3);

            _discardedCards = new List<Card>();
            _revealedCards = new List<Card>();
        }

        public void DistributeCards()
        {
            foreach (var player in _players)
            {
                player.Hand.Add(_deck.Draw());
            }
        }

        public void DistributeCards(int timesToDistribute)
        {
            for (int i = 0; i < timesToDistribute; i++)
            {
                DistributeCards();
            }
        }

        public void PerformFlop()
        {
            _discardedCards.Add(_deck.Draw());
            _revealedCards.Add(_deck.Draw());
            _revealedCards.Add(_deck.Draw());
            _revealedCards.Add(_deck.Draw());
        }

        public void PerformTurn()
        {
            _discardedCards.Add(_deck.Draw());
            _revealedCards.Add(_deck.Draw());
        }

        public void PerformRiver()
        {
            _discardedCards.Add(_deck.Draw());
            _revealedCards.Add(_deck.Draw());
        }

        public Player CalculateWinner()
        {
            throw new NotImplementedException();
        }
    }
}
