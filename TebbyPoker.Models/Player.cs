using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TebbyPoker.Models
{
    public class Player
    {
        string _name;
        public string Name { get { return _name; } }

        List<Card> _hand;
        public List<Card> Hand { get { return _hand; } }

        public Player(string name)
        {
            this._name = name;

            _hand = new List<Card>();
        }
    }
}
