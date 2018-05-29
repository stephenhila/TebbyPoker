using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TebbyPoker.Models
{
    public class Card
    {
        Rank _rank;
        public Rank Rank { get { return _rank; } }

        Suit _suit;
        public Suit Suit { get { return _suit; } }

        public Card(Rank rank, Suit suit)
        {
            _rank = rank;
            _suit = suit;
        }

        public override string ToString()
        {
            return string.Format("{0} of {1}", Enum.GetName(typeof(Rank), _rank), Enum.GetName(typeof(Suit), _suit));
        }
    }
}
