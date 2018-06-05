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

        public bool IsRoyalFlush(List<Card> cards)
        {
            foreach (var ace in cards.Where(c => c.Rank == Rank.Ace))
            {
                return cards.Exists(c => c.Suit == ace.Suit && c.Rank == Rank.King)
                    && cards.Exists(c => c.Suit == ace.Suit && c.Rank == Rank.Queen)
                    && cards.Exists(c => c.Suit == ace.Suit && c.Rank == Rank.Jack)
                    && cards.Exists(c => c.Suit == ace.Suit && c.Rank == Rank.Ten);
            }
            return false;
        }

        public bool IsStraightFlush(List<Card> cards)
        {
            foreach (var cardGroup in cards.GroupBy(c => c.Suit))
            {
                if (cardGroup.Count() >= 5 && IsStraight(cardGroup.ToList()))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsFourOfAKind(List<Card> cards)
        {
            foreach (var cardGroup in cards.GroupBy(c => c.Rank))
            {
                if (cardGroup.Count() == 4)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsFullHouse(List<Card> cards)
        {
            bool hasThreeOfAKind = false;
            bool hasAPair = false;
            foreach (var cardGroup in cards.GroupBy(c => c.Rank))
            {
                if (cardGroup.Count() == 3)
                {
                    if (!hasThreeOfAKind)
                    {
                        hasThreeOfAKind = true;
                    }
                    else
                    {
                        // if a second three of a kind was found, then consider two of three of those as the pair card
                        hasAPair = true;
                    }
                }
                else if (cardGroup.Count() == 2)
                {
                    hasAPair = true;
                }
            }
            return hasThreeOfAKind && hasAPair;
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
            int straightCounter = 1;
            Card previousCard = null;
            foreach (var card in cards.OrderBy(c => c.Rank))
            {
                if (previousCard != null)
                {
                    if (card.Rank - 1 == previousCard.Rank)
                    {
                        straightCounter++;
                    }
                    else if (card.Rank == Rank.Ace && previousCard.Rank == Rank.King)
                    {
                        straightCounter++;
                    }
                    else
                    {
                        break;
                    }
                }
                previousCard = card;
            }
            return straightCounter >= 5;
        }

        public bool IsThreeOfAKind(List<Card> cards)
        {
            foreach (var cardGroup in cards.GroupBy(c => c.Rank))
            {
                if (cardGroup.Count() == 3)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsTwoPair(List<Card> cards)
        {
            int groupCount = 0;
            foreach (var cardGroup in cards.GroupBy(c => c.Rank))
            {
                if (cardGroup.Count() == 2)
                {
                    groupCount++;
                }
            }

            return groupCount > 1;
        }

        public bool IsOnePair(List<Card> cards)
        {
            int groupCount = 0;
            foreach (var cardGroup in cards.GroupBy(c => c.Rank))
            {
                if (cardGroup.Count() == 2)
                {
                    groupCount++;
                }
            }

            return groupCount == 1;
        }
    }
}
