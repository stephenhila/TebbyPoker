﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TebbyPoker.Models.UnitTest
{
    [TestClass]
    public class PlayerUnitTest
    {
        [TestMethod]
        public void Player_Creation_PlayerCreatedCorrectly()
        {
            // Arrange
            string expectedPlayerName = "Tebby";
            int expectedCardCount = 0;

            // Act
            Player tebby = new Player(expectedPlayerName);

            // Assert
            Assert.AreEqual(tebby.Name, expectedPlayerName);
            Assert.IsNotNull(tebby.Hand);
            Assert.AreEqual(tebby.Hand.Count, expectedCardCount);
        }

        [TestMethod]
        public void Player_GetCard_CardDrawnCorrectly()
        {
            // Arrange
            Deck deck = new Deck();
            deck.Shuffle(3);
            Player tebby = new Player("Tebby");

            int expectedHandCount = 1;
            int expectedDeckCount = deck.Cards.Count - expectedHandCount;

            // Act
            tebby.GetCard(deck);

            // Assert
            Assert.AreEqual(tebby.Hand.Count, expectedHandCount);
            Assert.AreEqual(deck.Cards.Count, expectedDeckCount);
        }

        [TestMethod]
        public void Player_Fold_PlayerFoldedCorrectly()
        {
            // Arrange
            Deck deck = new Deck();
            deck.Shuffle(3);
            Player tebby = new Player("Tebby");

            int expectedHandCount = 0;
            int expectedDiscardCount = 2;
            int expectedDeckCount = deck.Cards.Count - expectedDiscardCount;

            // Act
            tebby.GetCard(deck);
            tebby.GetCard(deck);
            tebby.Fold(deck);

            // Assert
            Assert.AreEqual(tebby.Hand.Count, expectedHandCount);
            Assert.AreEqual(deck.Cards.Count, expectedDeckCount);
            Assert.AreEqual(deck.DiscardedCards.Count, expectedDiscardCount);
#warning TODO: Assert.AreEqual(expected,actual) usage incorrect. Swap expected value and actual value into correct position.
        }
    }
}
