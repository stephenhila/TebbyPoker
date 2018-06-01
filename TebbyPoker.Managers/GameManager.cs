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
        List<Round> _rounds;
        public List<Round> GetRounds() { return _rounds; }

        List<Player> _activePlayers;
        public List<Player> GetPlayers() { return _activePlayers; }

        Deck _deck;
        protected Deck GetDeck() { return _deck; }

        List<Card> _revealedCards;
        public List<Card> GetRevealedCards() { return _revealedCards; }

        public GameManager()
        {
            _activePlayers = new List<Player>();
            _deck = new Deck();
            _revealedCards = new List<Card>();
        }

        public void AddPlayer(string name)
        {
            _activePlayers.Add(new Player(name));
        }

        public void RemovePlayer(string name)
        {
            _activePlayers.RemoveAll(p => p.Name == name);
        }

        public void StartNewGame()
        {
            if (_activePlayers == null || _activePlayers.Count < 1)
            { throw new InvalidOperationException("There are no players in the game!"); }

            _deck.Shuffle(3);
        }

        public void DistributeCards()
        {
            foreach (var player in _activePlayers)
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
    }
}
