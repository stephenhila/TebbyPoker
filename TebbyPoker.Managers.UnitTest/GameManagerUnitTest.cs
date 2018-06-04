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
        IHandEvaluator handEvaluator;
        IGameManager unitUnderTest;

        [TestInitialize]
        public void Initialize()
        {
            handEvaluator = new HandEvaluator();
            unitUnderTest = new GameManager(handEvaluator);
        }

        [TestMethod]
        public void GameManager_DistributeOneCardEach_EachPlayerHasOneCardInHand()
        {
            // Arrange
            int expectedCardCount = 1;

            unitUnderTest.AddPlayer("Tebby");
            unitUnderTest.AddPlayer("Tobby");
            unitUnderTest.AddPlayer("Tubby");
            unitUnderTest.AddPlayer("Jeff");

            unitUnderTest.StartRound();

            // Act
            unitUnderTest.DistributeCards();

            // Assert
            foreach (var player in unitUnderTest.GetPlayers())
            {
                Assert.AreEqual(player.Hand.Count, expectedCardCount);
            }
        }

        [TestMethod]
        public void GameManager_DistributeTwoCardsEach_EachPlayerHasTwoCardsInHand()
        {
            // Arrange
            int expectedCardCount = 2;

            unitUnderTest.AddPlayer("Tebby");
            unitUnderTest.AddPlayer("Tobby");
            unitUnderTest.AddPlayer("Tubby");
            unitUnderTest.AddPlayer("Jeff");

            unitUnderTest.StartRound();

            // Act
            unitUnderTest.DistributeCards(2);

            // Assert
            foreach (var player in unitUnderTest.GetPlayers())
            {
                Assert.AreEqual(player.Hand.Count, expectedCardCount);
            }
        }

        [TestMethod]
        public void Round_EvaluateWinner_FourOfAKindWinsAgainstPair()
        {
            // Arrange
            string expectedWinner = "Michael Jordan";
            unitUnderTest.AddPlayer(expectedWinner);

            string expectedLoser = "Karl Malone";
            unitUnderTest.AddPlayer(expectedLoser);

            unitUnderTest.StartRound();

            // Arrange to force specific cards into players hands
            unitUnderTest.GetPlayers().First(p => p.Name == expectedWinner).GetCard(unitUnderTest.GetDeck(), Suit.Diamond, Rank.Six);
            unitUnderTest.GetPlayers().First(p => p.Name == expectedWinner).GetCard(unitUnderTest.GetDeck(), Suit.Spade, Rank.Six);

            unitUnderTest.GetPlayers().First(p => p.Name == expectedLoser).GetCard(unitUnderTest.GetDeck(), Suit.Heart, Rank.Two);
            unitUnderTest.GetPlayers().First(p => p.Name == expectedLoser).GetCard(unitUnderTest.GetDeck(), Suit.Club, Rank.Two);

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
