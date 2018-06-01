using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TebbyPoker.Models
{
    public class Round
    {
        List<Player> _players;
        public List<Player> Players { get { return _players; } }

        List<Player> _winners;
        public List<Player> Winners { get { return _winners; } }

        List<Card> _flop;
        public List<Card> Flop { get { return _flop; } }

        Card _river;
        public Card River { get { return _river; } }

        Card _turn;
        public Card Turn { get { return _turn; } }

        public Round(List<Player> players)
        {
            _players = players;
            _winners = new List<Player>();
            _flop = new List<Card>();
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

        public void CalculateWinners()
        {
            Dictionary<Player, Combination> playerCardCombinations = new Dictionary<Player, Combination>();

            List<Card> shownCards = new List<Card>();
            shownCards.AddRange(_flop);
            shownCards.Add(_river);
            shownCards.Add(_turn);

            foreach (var player in _players)
            {
                var cardsForCombination = new List<Card>(player.Hand);
                cardsForCombination.AddRange(shownCards);
                playerCardCombinations.Add(player, GetBestCombination(cardsForCombination));
            }

            Combination bestCombination = playerCardCombinations.Aggregate((l, r) => l.Value > r.Value ? l : r).Value;

            _winners = playerCardCombinations.Where(pc => pc.Value == bestCombination).Select(x => x.Key).ToList();

#warning TODO: add tie-breaker scenario.
        }

        private Combination GetBestCombination(List<Card> cards)
        {
            throw new NotImplementedException();
        }
    }
}
