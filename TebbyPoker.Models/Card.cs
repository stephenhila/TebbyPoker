using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TebbyPoker.Models
{
    public class Card : IComparable
    {
        Rank _rank;
        public Rank Rank { get { return _rank; } }

        Suit _suit;
        public Suit Suit { get { return _suit; } }

        /// <summary>
        /// Returns true if the card rank is either a Jack, Queen, King or an Ace.
        /// </summary>
        public bool IsRoyalCard
        {
            get
            {
                if (this.Rank == Rank.Ace ||
                    this.Rank >= Rank.Jack)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Card(Rank rank, Suit suit)
        {
            _rank = rank;
            _suit = suit;
        }

        public override string ToString()
        {
            return string.Format("{0} of {1}", Enum.GetName(typeof(Rank), _rank), Enum.GetName(typeof(Suit), _suit));
        }

        public int CompareTo(object obj)
        {
            if (obj is Card)
            {
                Card otherCard = obj as Card;
                if (this.Rank < otherCard.Rank)
                {
                    return -1;
                }
                else if (this.Rank > otherCard.Rank)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                throw new NotSupportedException("CompareTo failed due to being compared with another object that is not a typeof Card.");
            }
        }   
    }
}
