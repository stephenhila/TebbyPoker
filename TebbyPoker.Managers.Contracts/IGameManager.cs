using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TebbyPoker.Models;

namespace TebbyPoker.Managers
{
    public interface IGameManager
    {
        void StartNewGame();
        void AddPlayer(string name);
        List<Player> GetPlayers();
        List<Card> GetRevealedCards();
        List<Round> GetRounds();
        void DistributeCards();
        void DistributeCards(int timesToDistribute);
    }
}
