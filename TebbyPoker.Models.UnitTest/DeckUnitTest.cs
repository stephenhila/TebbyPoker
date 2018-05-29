using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TebbyPoker.Models.UnitTest
{
    [TestClass]
    public class DeckUnitTest
    {
        [TestMethod]
        public void Deck_Creation_CardsAddedCorrectly()
        {
            // Arrange
            int expectedCount = 52;

            // Act
            Deck unitUnderTest = new Deck();

            // Assert
            Assert.AreEqual(unitUnderTest.Cards.Count, expectedCount);
        }

        [TestMethod]
        public void Deck_Creation_AllCardsAreUnique()
        {
            // Arrange
            int expectedCount = 52;

            // Act
            Deck unitUnderTest = new Deck();

            // Assert
            Assert.AreEqual(unitUnderTest.Cards.Select(card => card.ToString()).Distinct().Count(), expectedCount);
        }

        [TestMethod]
        public void Deck_ShuffleOnce_CardsShuffledCorrectly()
        {
            // Arrange
            int expectedCount = 52;
            Deck unitUnderTest = new Deck();

            // Act
            unitUnderTest.Shuffle();

            // Assert
            Assert.AreEqual(unitUnderTest.Cards.Select(card => card.ToString()).Distinct().Count(), expectedCount);
        }

        [TestMethod]
        public void Deck_ShuffleTwice_CardsShuffledCorrectly()
        {
            // Arrange
            int expectedCount = 52;
            Deck unitUnderTest = new Deck();

            // Act
            unitUnderTest.Shuffle(2);

            // Assert
            Assert.AreEqual(unitUnderTest.Cards.Select(card => card.ToString()).Distinct().Count(), expectedCount);
        }
    }
}
