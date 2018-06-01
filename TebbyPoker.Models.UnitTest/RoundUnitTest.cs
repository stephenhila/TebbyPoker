using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TebbyPoker.Models.UnitTest
{
    [TestClass]
    public class RoundUnitTest
    {
        List<Player> players;
        Deck deck;
        Round unitUnderTest;

        [TestInitialize]
        public void TestInitialize()
        {
            players = new List<Player>
            {
                CreatePlayer("Tebby"),
                CreatePlayer("Joe"),
                CreatePlayer("Bob")
            };
            deck = new Deck();
            deck.Shuffle(3);
            unitUnderTest = new Round(players);
        }

        [TestMethod]
        public void Round_Creation_PlayersAddedCorrectly()
        {
            // Arrange
            int expectedCount = players.Count;

            // Act

            // Assert
            Assert.AreEqual(unitUnderTest.Players.Count, expectedCount);
        }

        [TestMethod]
        public void Round_PerformFlop_FlopExecutedCorrectly()
        {
            // Arrange
            int expectedFlopCount = 3;
            int expectedBurnCount = 1;
            int expectedDeckCount = deck.Cards.Count - expectedFlopCount - expectedBurnCount;

            // Act
            unitUnderTest.PerformFlop(deck);

            // Assert
            Assert.AreEqual(unitUnderTest.Flop.Count, expectedFlopCount);
            Assert.AreEqual(deck.Cards.Count, expectedDeckCount);
        }

        [TestMethod]
        public void Round_PerformRiver_RiverExecutedCorrectly()
        {
            // Arrange
            int expectedRiverCount = 1;
            int expectedBurnCount = 1;
            int expectedDeckCount = deck.Cards.Count - expectedRiverCount - expectedBurnCount;

            // Act
            unitUnderTest.PerformRiver(deck);

            // Assert
            Assert.IsNotNull(unitUnderTest.River);
            Assert.AreEqual(deck.Cards.Count, expectedDeckCount);
        }

        [TestMethod]
        public void Round_PerformTurn_TurnExecutedCorrectly()
        {
            // Arrange
            int expectedTurnCount = 1;
            int expectedBurnCount = 1;
            int expectedDeckCount = deck.Cards.Count - expectedTurnCount - expectedBurnCount;

            // Act
            unitUnderTest.PerformTurn(deck);

            // Assert
            Assert.IsNotNull(unitUnderTest.Turn);
            Assert.AreEqual(deck.Cards.Count, expectedDeckCount);
        }

        [TestMethod]
        public void Round_CalculateWinners_FourOfAKindWinsAgainstPair()
        {
            // Arrange
            Player expectedWinner = CreatePlayer("Michael Jordan");
            Player expectedLoser = CreatePlayer("Karl Malone");

            expectedWinner.GetCard(deck, Suit.Diamond, Rank.Six);
            expectedWinner.GetCard(deck, Suit.Spade, Rank.Six);

            expectedLoser.GetCard(deck, Suit.Heart, Rank.Two);
            expectedLoser.GetCard(deck, Suit.Club, Rank.Two);

            int index = 0;

            // Arrange flop burn card
            deck.Cards.MoveItemToIndex(deck.Cards.Find(card => card.Rank == Rank.Ace && card.Suit == Suit.Club), index++);
            // Arrange flop cards
            deck.Cards.MoveItemToIndex(deck.Cards.Find(card => card.Rank == Rank.Six), index++);
            deck.Cards.MoveItemToIndex(deck.Cards.Find(card => card.Rank == Rank.Six), index++);
            deck.Cards.MoveItemToIndex(deck.Cards.Find(card => card.Rank == Rank.Eight && card.Suit == Suit.Spade), index++);

            // Arrange river burn card
            deck.Cards.MoveItemToIndex(deck.Cards.Find(card => card.Rank == Rank.Jack && card.Suit == Suit.Spade), index++);
            // Arrange river card
            deck.Cards.MoveItemToIndex(deck.Cards.Find(card => card.Rank == Rank.Three && card.Suit == Suit.Club), index++);

            // Arrange turn burn card
            deck.Cards.MoveItemToIndex(deck.Cards.Find(card => card.Rank == Rank.King && card.Suit == Suit.Diamond), index++);
            // Arrange turn card
            deck.Cards.MoveItemToIndex(deck.Cards.Find(card => card.Rank == Rank.King && card.Suit == Suit.Spade), index++);

            // Act
            unitUnderTest = new Round(new List<Player> { expectedWinner, expectedLoser });
            unitUnderTest.PerformFlop(deck);
            unitUnderTest.PerformRiver(deck);
            unitUnderTest.PerformTurn(deck);
            unitUnderTest.CalculateWinners();

            // Assert
            Assert.IsTrue(unitUnderTest.Winners.Contains(expectedWinner));
            Assert.IsFalse(unitUnderTest.Winners.Contains(expectedLoser));
        }

        private Player CreatePlayer(string name)
        {
            return new Player(name);
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
