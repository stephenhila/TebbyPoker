using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TebbyPoker.GameEngine;
using TebbyPoker.GameEngine.Contracts;
using TebbyPoker.Models;

namespace TebbyPoker.Managers.UnitTest
{
    [TestClass]
    public class GameManagerUnitTest
    {
        ICombinationTypeEvaluator combinationTypeEvaluator;
        IGameManager unitUnderTest;

        [TestInitialize]
        public void Initialize()
        {
            combinationTypeEvaluator = new CombinationTypeEvaluator();
            unitUnderTest = new GameManager(combinationTypeEvaluator);
        }

        [TestMethod]
        public void GameManager_StartNewRound_DistributeTwoCardsEach()
        {
            // Arrange
            int expectedCardCount = 2;

            unitUnderTest.JoinGame(new Player("Tebby"));
            unitUnderTest.JoinGame(new Player("Tobby"));
            unitUnderTest.JoinGame(new Player("Tubby"));
            unitUnderTest.JoinGame(new Player("Jeff"));

            // Act
            unitUnderTest.StartNewRound();

            // Assert
            foreach (var player in unitUnderTest.GetActivePlayers())
            {
                Assert.AreEqual(player.Hand.Count, expectedCardCount);
            }
        }

        [TestMethod]
        public void Round_EvaluateWinner_FourOfAKindWinsAgainstPair()
        {
            // Arrange
            string expectedWinner = "Michael Jordan";
            unitUnderTest.JoinGame(new Player(expectedWinner));

            string expectedLoser = "Karl Malone";
            unitUnderTest.JoinGame(new Player(expectedLoser));

            unitUnderTest.StartNewRound();

            // Arrange to force specific cards into players hands
            unitUnderTest.GetActivePlayers().First(p => p.Name == expectedWinner).GetCard(unitUnderTest.GetDeck(), Suit.Diamond, Rank.Six);
            unitUnderTest.GetActivePlayers().First(p => p.Name == expectedWinner).GetCard(unitUnderTest.GetDeck(), Suit.Spade, Rank.Six);

            unitUnderTest.GetActivePlayers().First(p => p.Name == expectedLoser).GetCard(unitUnderTest.GetDeck(), Suit.Heart, Rank.Two);
            unitUnderTest.GetActivePlayers().First(p => p.Name == expectedLoser).GetCard(unitUnderTest.GetDeck(), Suit.Club, Rank.Two);

            int index = 0;

            // Arrange flop burn card
            unitUnderTest.GetDeck().Cards.MoveItemToIndex(unitUnderTest.GetDeck().Cards.Find(card => card.Rank == Rank.Ace && card.Suit == Suit.Club), index++);
            // Arrange flop cards
            unitUnderTest.GetDeck().Cards.MoveItemToIndex(unitUnderTest.GetDeck().Cards.Find(card => card.Rank == Rank.Six), index++);
            unitUnderTest.GetDeck().Cards.MoveItemToIndex(unitUnderTest.GetDeck().Cards.Find(card => card.Rank == Rank.Six), index++);
            unitUnderTest.GetDeck().Cards.MoveItemToIndex(unitUnderTest.GetDeck().Cards.Find(card => card.Rank == Rank.Eight && card.Suit == Suit.Spade), index++);

            // Arrange river burn card
            unitUnderTest.GetDeck().Cards.MoveItemToIndex(unitUnderTest.GetDeck().Cards.Find(card => card.Rank == Rank.Jack && card.Suit == Suit.Spade), index++);
            // Arrange river card
            unitUnderTest.GetDeck().Cards.MoveItemToIndex(unitUnderTest.GetDeck().Cards.Find(card => card.Rank == Rank.Three && card.Suit == Suit.Club), index++);

            // Arrange turn burn card
            unitUnderTest.GetDeck().Cards.MoveItemToIndex(unitUnderTest.GetDeck().Cards.Find(card => card.Rank == Rank.King && card.Suit == Suit.Diamond), index++);
            // Arrange turn card
            unitUnderTest.GetDeck().Cards.MoveItemToIndex(unitUnderTest.GetDeck().Cards.Find(card => card.Rank == Rank.King && card.Suit == Suit.Spade), index++);

            // Arrange shown cards
            unitUnderTest.PerformFlop();
            unitUnderTest.PerformRiver();
            unitUnderTest.PerformTurn();

            // Act
            unitUnderTest.CalculateWinners();

            // Assert
            Assert.IsTrue(unitUnderTest.GetCurrentRound().Winners.Exists(w => w.Name == expectedWinner));
            Assert.IsFalse(unitUnderTest.GetCurrentRound().Winners.Exists(w => w.Name == expectedLoser));
        }
    }

    static class ListExtensions
    {
        public static void MoveItemToIndex<T>(this List<T> list, T item, int index)
        {
            list.Remove(item);
            list.Insert(index, item);
        }
    }
}
