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

        

        private Player CreatePlayer(string name)
        {
            return new Player(name);
        }
    }

}
