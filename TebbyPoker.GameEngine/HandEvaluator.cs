using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TebbyPoker.GameEngine.Contracts;
using TebbyPoker.Models;

namespace TebbyPoker.GameEngine
{
    public class HandEvaluator : IHandEvaluator
    {
        public Combination Evaluate(List<Card> hand)
        {
            throw new NotImplementedException();
        }

        private bool IsFlush(List<Card> cards)
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
    }
}
