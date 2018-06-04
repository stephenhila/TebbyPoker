using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TebbyPoker.GameEngine.Contracts;
using TebbyPoker.Models;

namespace TebbyPoker.Managers
{
    public class GameManager : IGameManager
    {
        IHandEvaluator _handEvaluator;

        Round _currentRound;
        public Round GetCurrentRound() { return _currentRound; }

        List<Round> _rounds;
        public List<Round> GetRounds() { return _rounds; }

        List<Player> _activePlayers;
        public List<Player> GetPlayers() { return _activePlayers; }

        Deck _deck;
        public Deck GetDeck() { return _deck; }

        List<Card> _revealedCards;
        public List<Card> GetRevealedCards() { return _revealedCards; }

        public GameManager(IHandEvaluator handEvaluator)
        {
            _activePlayers = new List<Player>();
            _deck = new Deck();
            _revealedCards = new List<Card>();
            _rounds = new List<Round>();

            _handEvaluator = handEvaluator;
        }

        public void AddPlayer(string name)
        {
            _activePlayers.Add(new Player(name));
        }

        public void RemovePlayer(string name)
        {
            _activePlayers.RemoveAll(p => p.Name == name);
        }

        public void StartRound()
        {
            if (_activePlayers == null || _activePlayers.Count < 1)
            { throw new InvalidOperationException("There are no players in the game!"); }

            _deck.Shuffle(3);

            Round currentRound = new Round(_activePlayers);
            _rounds.Add(_currentRound);
            _currentRound = currentRound;
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

        public void PerformFlop()
        {
            _currentRound.PerformFlop(_deck);
        }

        public void PerformRiver()
        {
            _currentRound.PerformRiver(_deck);
        }

        public void PerformTurn()
        {
            _currentRound.PerformTurn(_deck);
        }

        public void CalculateWinners()
        {
            Dictionary<Player, Combination> playerCardCombinations = new Dictionary<Player, Combination>();

            List<Card> shownCards = new List<Card>();
            shownCards.AddRange(_currentRound.Flop);
            shownCards.Add(_currentRound.River);
            shownCards.Add(_currentRound.Turn);

            foreach (var player in _activePlayers)
            {
                var cardsForCombination = new List<Card>(player.Hand);
                cardsForCombination.AddRange(shownCards);
                playerCardCombinations.Add(player, _handEvaluator.Evaluate(cardsForCombination));
            }

            Combination bestCombination = playerCardCombinations.Aggregate((l, r) => l.Value > r.Value ? l : r).Value;

            _currentRound.Winners = playerCardCombinations.Where(pc => pc.Value == bestCombination).Select(x => x.Key).ToList();

#warning TODO: add tie-breaker scenario.
        }
    }
}
