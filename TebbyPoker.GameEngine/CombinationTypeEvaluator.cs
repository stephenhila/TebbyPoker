using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TebbyPoker.GameEngine.Contracts;
using TebbyPoker.Models;

namespace TebbyPoker.GameEngine
{
    public class CombinationTypeEvaluator : ICombinationTypeEvaluator
    {
        public Combination Evaluate(List<Card> hand)
        {
            if (IsRoyalFlush(hand))
            {
                return Combination.RoyalFlush;
            }
            else if (IsStraightFlush(hand))
            {
                return Combination.StraightFlush;
            }
            else if (IsFourOfAKind(hand))
            {
                return Combination.FourOfAKind;
            }
            else if (IsFullHouse(hand))
            {
                return Combination.FullHouse;
            }
            else if (IsFlush(hand))
            {
                return Combination.Flush;
            }
            else if (IsStraight(hand))
            {
                return Combination.Straight;
            }
            else if (IsThreeOfAKind(hand))
            {
                return Combination.ThreeOfAKind;
            }
            else if (IsTwoPair(hand))
            {
                return Combination.TwoPair;
            }
            else if (IsOnePair(hand))
            {
                return Combination.OnePair;
            }
            else
            {
                return Combination.HighCard;
            }
        }

#warning TODO: try and think of a pattern to use for each combination-related methods which return bool.
        public bool IsRoyalFlush(List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public bool IsStraightFlush(List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public bool IsFourOfAKind(List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public bool IsFullHouse(List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public bool IsFlush(List<Card> cards)
        {
            foreach (var cardGroup in cards.GroupBy(c => c.Suit))
            {
                if (cardGroup.Count() >= 5)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsStraight(List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public bool IsThreeOfAKind(List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public bool IsTwoPair(List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public bool IsOnePair(List<Card> cards)
        {
            throw new NotImplementedException();
        }
    }
}
