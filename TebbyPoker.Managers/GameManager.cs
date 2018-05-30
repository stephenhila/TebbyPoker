using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TebbyPoker.Models;

namespace TebbyPoker.Managers
{
    public class GameManager : IGameManager
    {
        List<Player> _players;
        public List<Player> GetPlayers() { return _players; }

        Deck _deck;
        protected Deck GetDeck() { return _deck; }

        List<Card> _discardedCards;

        List<Card> _revealedCards;
        public List<Card> GetRevealedCards() { return _revealedCards; }

        public GameManager()
        {
            _players = new List<Player>();
            _deck = new Deck();

            _discardedCards = new List<Card>();
            _revealedCards = new List<Card>();
        }

        public void AddPlayer(string name)
        {
            _players.Add(new Player(name));
        }

        public void StartNewGame()
        {
            if (_players == null || _players.Count < 1)
            { throw new InvalidOperationException("There are no players in the game!"); }

            _deck.Shuffle(3);
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
