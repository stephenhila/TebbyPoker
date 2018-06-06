using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TebbyPoker.Web.Models
{
    public class PlayIndexViewModel
    {
        public List<string> Players { get; set; }

        [DisplayName("Add a player:")]
        public string PlayerToAdd { get; set; }

        public PlayIndexViewModel()
        {
            Players = new List<string>();
        }
    }
}