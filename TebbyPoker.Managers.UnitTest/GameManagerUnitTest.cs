using System;
using System.Collections.Generic;
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

            // Act
            unitUnderTest.AddPlayer("Tebby");
            unitUnderTest.AddPlayer("Tobby");
            unitUnderTest.AddPlayer("Tubby");
            unitUnderTest.AddPlayer("Jeff");
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

            // Act
            unitUnderTest.DistributeCards(2);

            // Assert
            foreach (var player in unitUnderTest.GetPlayers())
            {
                Assert.AreEqual(player.Hand.Count, expectedCardCount);
            }
        }

        // public void GameManager_EvaluateWinner
    }
}
