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
        ICombinationTypeEvaluator _combinationTypeEvaluator;

        Round _currentRound;
        public Round GetCurrentRound() { return _currentRound; }

        List<Round> _rounds;
        public List<Round> GetRounds() { return _rounds; }

        List<Player> _activePlayers;
        public List<Player> GetActivePlayers() { return _activePlayers; }

        Queue<Player> _joiningPlayers;
        public List<Player> GetJoiningPlayers() { return _joiningPlayers.ToList(); }

        Queue<Player> _leavingPlayers;
        public List<Player> GetLeavingPlayers() { return _leavingPlayers.ToList(); }

        Deck _deck;
        public Deck GetDeck() { return _deck; }

        List<Card> _revealedCards;
        public List<Card> GetRevealedCards() { return _revealedCards; }

        public GameManager(ICombinationTypeEvaluator combinationTypeEvaluator)
        {
            _activePlayers = new List<Player>();
            _joiningPlayers = new Queue<Player>();
            _leavingPlayers = new Queue<Player>();
            _deck = new Deck();
            _revealedCards = new List<Card>();
            _rounds = new List<Round>();

            _combinationTypeEvaluator = combinationTypeEvaluator;
        }

        void AddPlayer(string name)
        {
#warning TODO: think of design to handle database transactions
            using (var context = new TebbyPokerContext())
            {
                Player player = context.Players.First(p => p.Name == name);
                AddPlayer(player);
            }
        }

        void AddPlayer(Player player)
        {
            _activePlayers.Add(player);
        }

        void RemovePlayer(string name)
        {
            _activePlayers.RemoveAll(p => p.Name == name);
        }

        void RemovePlayer(Player player)
        {
            RemovePlayer(player.Name);
        }

        public void JoinGame(Player player)
        {
            _joiningPlayers.Enqueue(player);
        }

        public void LeaveGame(Player player)
        {
            _leavingPlayers.Enqueue(player);
        }

        public void StartNewRound()
        {
            // remove all leaving players
            while (_leavingPlayers.Count > 0)
            {
                Player leavingPlayer = _leavingPlayers.Dequeue();
                leavingPlayer.Fold(_deck);
                RemovePlayer(leavingPlayer);
            }

            // add joining players
            while (_joiningPlayers.Count > 0)
            {
                Player joiningPlayer = _joiningPlayers.Dequeue();
                AddPlayer(joiningPlayer);
            }

            if (_activePlayers == null || _activePlayers.Count < 1)
            { throw new InvalidOperationException("There are no active players in the game!"); }

            _deck.Shuffle(10);
            DistributeCards(2);

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
                playerCardCombinations.Add(player, _combinationTypeEvaluator.Evaluate(cardsForCombination));
            }

            Combination bestCombination = playerCardCombinations.Aggregate((l, r) => l.Value > r.Value ? l : r).Value;

            _currentRound.Winners = playerCardCombinations.Where(pc => pc.Value == bestCombination).Select(x => x.Key).ToList();

#warning TODO: add tie-breaker scenario.
        }
    }
}
