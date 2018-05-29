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

        Card[] _hand;
        public Card[] Hand { get { return _hand; } }
    }
}
