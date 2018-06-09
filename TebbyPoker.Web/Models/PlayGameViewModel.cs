using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TebbyPoker.Managers;
using TebbyPoker.Models;

namespace TebbyPoker.Web.Models
{
    public class PlayGameViewModel
    {
        List<Player> _players;
        public IEnumerable<Player> Players
        {
            get { return _players; }
        }

        Deck _deck;
        public Deck Deck { get { return _deck; } }

        public PlayGameViewModel(List<Player> players)
        {
            _players = players;

            _deck = new Deck();
            _deck.Shuffle(3);

            DistributeCards(_players, _deck);
            DistributeCards(_players, _deck);
        }

        void DistributeCards(List<Player> players, Deck deck)
        {
            foreach (var player in players)
            {
                player.GetCard(deck);
            }
        }
    }
}