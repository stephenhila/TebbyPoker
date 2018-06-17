using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TebbyPoker.Models
{
    public  class TebbyPokerContext : DbContext
    {
        public TebbyPokerContext()
            : base("name=TebbyPokerConnection")
        { }

        public DbSet<Player> Players { get; set; }
    }
}
