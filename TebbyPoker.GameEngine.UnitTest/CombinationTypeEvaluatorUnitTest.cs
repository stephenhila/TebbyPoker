using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TebbyPoker.GameEngine.Contracts;
using TebbyPoker.Models;

namespace TebbyPoker.GameEngine.UnitTest
{
    [TestClass]
    public class CombinationTypeEvaluatorUnitTest
    {
        ICombinationTypeEvaluator unitUnderTest;

        [TestInitialize]
        public void TestInitialize()
        {
            unitUnderTest = new CombinationTypeEvaluator();
        }

        [TestMethod]
        public void Evaluate_ReturnsCorrectCombinationType_RoyalFlush()
        {
            // Arrange
            List<Card> cards = new List<Card>();
            cards.Add(new Card(Rank.Ace, Suit.Spade));
            cards.Add(new Card(Rank.King, Suit.Spade));
            cards.Add(new Card(Rank.Queen, Suit.Spade));
            cards.Add(new Card(Rank.Jack, Suit.Spade));
            cards.Add(new Card(Rank.Ten, Suit.Spade));
            cards.Add(new Card(Rank.Nine, Suit.Spade));
            cards.Add(new Card(Rank.Seven, Suit.Heart));

            // Act
            Combination combination = unitUnderTest.Evaluate(cards);

            // Assert
            Assert.AreEqual(combination, Combination.RoyalFlush);
        }

        [TestMethod]
        public void Evaluate_ReturnsCorrectCombinationType_StraightFlush()
        {
            // Arrange
            List<Card> cards = new List<Card>();
            cards.Add(new Card(Rank.Ace, Suit.Spade));
            cards.Add(new Card(Rank.Two, Suit.Spade));
            cards.Add(new Card(Rank.Three, Suit.Spade));
            cards.Add(new Card(Rank.Four, Suit.Spade));
            cards.Add(new Card(Rank.Five, Suit.Spade));
            cards.Add(new Card(Rank.Six, Suit.Heart));
            cards.Add(new Card(Rank.Seven, Suit.Heart));

            // Act
            Combination combination = unitUnderTest.Evaluate(cards);

            // Assert
            Assert.AreEqual(combination, Combination.StraightFlush);
        }

        [TestMethod]
        public void Evaluate_ReturnsCorrectCombinationType_FourOfAKind()
        {
            // Arrange
            List<Card> cards = new List<Card>();
            cards.Add(new Card(Rank.Ace, Suit.Spade));
            cards.Add(new Card(Rank.Ace, Suit.Heart));
            cards.Add(new Card(Rank.Ace, Suit.Diamond));
            cards.Add(new Card(Rank.Ace, Suit.Club));
            cards.Add(new Card(Rank.Queen, Suit.Spade));
            cards.Add(new Card(Rank.Queen, Suit.Heart));
            cards.Add(new Card(Rank.Queen, Suit.Diamond));

            // Act
            Combination combination = unitUnderTest.Evaluate(cards);

            // Assert
            Assert.AreEqual(combination, Combination.FourOfAKind);
        }

        [TestMethod]
        public void Evaluate_ReturnsCorrectCombinationType_FullHouse()
        {
            // Arrange
            List<Card> cards = new List<Card>();
            cards.Add(new Card(Rank.Ace, Suit.Spade));
            cards.Add(new Card(Rank.Ace, Suit.Heart));
            cards.Add(new Card(Rank.Ace, Suit.Diamond));
            cards.Add(new Card(Rank.King, Suit.Spade));
            cards.Add(new Card(Rank.King, Suit.Heart));
            cards.Add(new Card(Rank.King, Suit.Diamond));
            cards.Add(new Card(Rank.Queen, Suit.Spade));

            // Act
            Combination combination = unitUnderTest.Evaluate(cards);

            // Assert
            Assert.AreEqual(combination, Combination.FullHouse);
        }

        [TestMethod]
        public void Evaluate_ReturnsCorrectCombinationType_Flush()
        {
            // Arrange
            List<Card> cards = new List<Card>();
            cards.Add(new Card(Rank.Ace, Suit.Spade));
            cards.Add(new Card(Rank.Three, Suit.Spade));
            cards.Add(new Card(Rank.Five, Suit.Spade));
            cards.Add(new Card(Rank.Seven, Suit.Spade));
            cards.Add(new Card(Rank.Nine, Suit.Spade));
            cards.Add(new Card(Rank.Nine, Suit.Heart));
            cards.Add(new Card(Rank.Seven, Suit.Heart));

            // Act
            Combination combination = unitUnderTest.Evaluate(cards);

            // Assert
            Assert.AreEqual(combination, Combination.Flush);
        }

        [TestMethod]
        public void Evaluate_ReturnsCorrectCombinationType_Straight()
        {
            // Arrange
            List<Card> cards = new List<Card>();
            cards.Add(new Card(Rank.Ace, Suit.Spade));
            cards.Add(new Card(Rank.Two, Suit.Diamond));
            cards.Add(new Card(Rank.Three, Suit.Heart));
            cards.Add(new Card(Rank.Four, Suit.Heart));
            cards.Add(new Card(Rank.Five, Suit.Spade));
            cards.Add(new Card(Rank.Seven, Suit.Heart));
            cards.Add(new Card(Rank.King, Suit.Heart));

            // Act
            Combination combination = unitUnderTest.Evaluate(cards);

            // Assert
            Assert.AreEqual(combination, Combination.Straight);
        }

        [TestMethod]
        public void Evaluate_ReturnsCorrectCombinationType_ThreeOfAKind()
        {
            // Arrange
            List<Card> cards = new List<Card>();
            cards.Add(new Card(Rank.Six, Suit.Spade));
            cards.Add(new Card(Rank.Six, Suit.Club));
            cards.Add(new Card(Rank.Six, Suit.Diamond));
            cards.Add(new Card(Rank.Ace, Suit.Spade));
            cards.Add(new Card(Rank.Five, Suit.Club));
            cards.Add(new Card(Rank.Seven, Suit.Spade));
            cards.Add(new Card(Rank.King, Suit.Club));

            // Act
            Combination combination = unitUnderTest.Evaluate(cards);

            // Assert
            Assert.AreEqual(combination, Combination.ThreeOfAKind);
        }

        [TestMethod]
        public void Evaluate_ReturnsCorrectCombinationType_TwoPair()
        {
            // Arrange
            List<Card> cards = new List<Card>();
            cards.Add(new Card(Rank.Jack, Suit.Spade));
            cards.Add(new Card(Rank.Jack, Suit.Diamond));
            cards.Add(new Card(Rank.Five, Suit.Spade));
            cards.Add(new Card(Rank.Five, Suit.Diamond));
            cards.Add(new Card(Rank.Seven, Suit.Club));
            cards.Add(new Card(Rank.Eight, Suit.Spade));
            cards.Add(new Card(Rank.Nine, Suit.Club));

            // Act
            Combination combination = unitUnderTest.Evaluate(cards);

            // Assert
            Assert.AreEqual(combination, Combination.TwoPair);
        }

        [TestMethod]
        public void Evaluate_ReturnsCorrectCombinationType_OnePair()
        {
            // Arrange
            List<Card> cards = new List<Card>();
            cards.Add(new Card(Rank.Queen, Suit.Spade));
            cards.Add(new Card(Rank.Queen, Suit.Diamond));
            cards.Add(new Card(Rank.Four, Suit.Spade));
            cards.Add(new Card(Rank.Two, Suit.Diamond));
            cards.Add(new Card(Rank.Seven, Suit.Club));
            cards.Add(new Card(Rank.Eight, Suit.Spade));
            cards.Add(new Card(Rank.Nine, Suit.Club));

            // Act
            Combination combination = unitUnderTest.Evaluate(cards);

            // Assert
            Assert.AreEqual(combination, Combination.OnePair);
        }

        [TestMethod]
        public void Evaluate_ReturnsCorrectCombinationType_HighCard()
        {
            // Arrange
            List<Card> cards = new List<Card>();
            cards.Add(new Card(Rank.Ace, Suit.Spade));
            cards.Add(new Card(Rank.Queen, Suit.Diamond));
            cards.Add(new Card(Rank.Four, Suit.Spade));
            cards.Add(new Card(Rank.Two, Suit.Diamond));
            cards.Add(new Card(Rank.Seven, Suit.Club));
            cards.Add(new Card(Rank.Eight, Suit.Spade));
            cards.Add(new Card(Rank.Nine, Suit.Club));

            // Act
            Combination combination = unitUnderTest.Evaluate(cards);

            // Assert
            Assert.AreEqual(combination, Combination.HighCard);
        }
    }
}
