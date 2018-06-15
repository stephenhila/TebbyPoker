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
        void StartNewRound();
        void JoinGame(Player player);
        void LeaveGame(Player player);
        Deck GetDeck();
        List<Player> GetActivePlayers();
        List<Card> GetRevealedCards();
        List<Round> GetRounds();
        Round GetCurrentRound();
        void PerformFlop();
        void PerformRiver();
        void PerformTurn();
        void DistributeCards();
        void DistributeCards(int timesToDistribute);
        void CalculateWinners();
    }
}
