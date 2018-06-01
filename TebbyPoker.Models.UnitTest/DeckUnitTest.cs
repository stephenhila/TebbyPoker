using System;
using System.Collections.Generic;
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
#warning TODO: add assertion that card order have indeed been randomized through shuffling.
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
#warning TODO: add assertion that card order have indeed been randomized through shuffling.
            Assert.AreEqual(unitUnderTest.Cards.Select(card => card.ToString()).Distinct().Count(), expectedCount);
        }

        [TestMethod]
        public void Deck_Draw_CardDrawnCorrectly()
        {
            // Arrange
            Deck unitUnderTest = new Deck();
            int expectedCount = unitUnderTest.Cards.Count - 1;

            // Act
            Card topCard = unitUnderTest.Cards.FirstOrDefault();
            Card drawnCard = unitUnderTest.Draw();

            // Assert
            Assert.AreEqual(topCard, drawnCard);
            Assert.AreEqual(unitUnderTest.Cards.Count, expectedCount);
        }

        [TestMethod]
        public void Deck_Discard_CardDiscardedCorrectly()
        {
            // Arrange
            Deck unitUnderTest = new Deck();
            int expectedCount = unitUnderTest.Cards.Count - 1;

            // Act
            unitUnderTest.Discard();

            // Assert
            Assert.AreEqual(unitUnderTest.Cards.Count, expectedCount);
        }

        [TestMethod]
        public void Deck_DiscardSpecificCard_CardDiscardedCorrectly()
        {
            // Arrange
            Deck unitUnderTest = new Deck();

            // Act
            Card card = unitUnderTest.Draw();
            unitUnderTest.Discard(card);

            // Assert
            Assert.IsFalse(unitUnderTest.Cards.Contains(card));
            Assert.IsTrue(unitUnderTest.DiscardedCards.Contains(card));
        }


        [TestMethod]
        public void Deck_DiscardListOfCard_CardsDiscardedCorrectly()
        {
            // Arrange
            Deck unitUnderTest = new Deck();
            List<Card> cards = new List<Card> { unitUnderTest.Draw(), unitUnderTest.Draw(), unitUnderTest.Draw() };

            // Act
            unitUnderTest.Discard(cards);

            // Assert
            cards.ForEach(card => Assert.IsFalse(unitUnderTest.Cards.Contains(card)));
            cards.ForEach(card => Assert.IsTrue(unitUnderTest.DiscardedCards.Contains(card)));
        }
    }
}
