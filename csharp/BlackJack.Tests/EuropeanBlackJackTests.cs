using BlackJack.core.Games;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BlackJack.core.Tests
{
    public class EuropeanBlackJackTests : Scenario
    {
        private readonly TestDataProvider _testDataProvider;
        private readonly EuropeanBlackJack _europeanBlackJack;

        public EuropeanBlackJackTests()
        {
            _testDataProvider = new TestDataProvider();
            var logger = new Mock<ILogger<EuropeanBlackJack>>();
            _europeanBlackJack = new EuropeanBlackJack(logger.Object, _testDataProvider.GetDeck());
        }
        
        [Test]
        public void Should_bust_player_above_21_score()
        {
            //Arrange
            var player = _testDataProvider.GetPlayers()[0];
            player.Hand = _testDataProvider.GetHand(true);
            //Act
            var actual = _europeanBlackJack.Bust(player);
            //Assert
            Assert.AreEqual(true, actual);
            Assert.AreEqual(Status.Lost, player.Status);
        }

        public void Should_not_bust_player_below_21_score()
        {
            //Arrange
            var player = _testDataProvider.GetPlayers()[0];
            player.Hand = _testDataProvider.GetHand(false);
            //Act
            var actual = _europeanBlackJack.Bust(player);
            //Assert
            Assert.AreEqual(false, actual);
            Assert.AreEqual(Status.InGame, player.Status);
        }

        [Test]
        public void Should_calculate_players_hand_without_aces()
        {
            //Arrange
            var player = _testDataProvider.GetPlayers()[0];
            player.Hand = _testDataProvider.GetHand(false);
            //Act
            var actual = _europeanBlackJack.CalculatePlayerHand(player);
            //Assert
            Assert.AreEqual(player.Hand[0].Rank, actual);
        }

        [Test]
        public void Should_calculate_players_optimal_hand_with_one_ace()
        {
            //Arrange
            int numberOfAces = 1;
            var player = _testDataProvider.GetPlayers()[0];
            player.Hand = _testDataProvider.GetHandWithAces(numberOfAces);
            //Act
            var actual = _europeanBlackJack.CalculatePlayerHand(player);
            //Assert
            Assert.AreEqual(11, actual);
        }

        [Test]
        public void Should_calculate_players_optimal_hand_with_two_aces()
        {
            //Arrange
            int numberOfAces = 2;
            var player = _testDataProvider.GetPlayers()[0];
            player.Hand = _testDataProvider.GetHandWithAces(numberOfAces);
            //Act
            var actual = _europeanBlackJack.CalculatePlayerHand(player);
            //Assert
            Assert.AreEqual(12, actual);
        }

        [Test]
        public void Should_determine_dealer_as_winner_because_player_busts()
        {
            //Arrange
            var players = _testDataProvider.GetPlayers();
            var player = players[0];
            var dealer = players[1];
            player.Hand = _testDataProvider.GetHand(true);
            dealer.Hand = _testDataProvider.GetHand(false);
            //Act
            var actual = _europeanBlackJack.GetWinner(dealer, player);
            //Assert
            Assert.AreEqual(dealer, actual);
        }

        [Test]
        public void Should_determine_player_as_winner_because_player_busts()
        {
            //Arrange
            var players = _testDataProvider.GetPlayers();
            var player = players[0];
            var dealer = players[1];
            player.Hand = _testDataProvider.GetHand(false);
            dealer.Hand = _testDataProvider.GetHand(true);
            //Act
            var actual = _europeanBlackJack.GetWinner(dealer, player);
            //Assert
            Assert.AreEqual(player, actual);
        }

        [Test]
        public void Should_determine_player_as_winner_because_better_hand()
        {
            //Arrange
            var players = _testDataProvider.GetPlayers();
            var player = players[0];
            var dealer = players[1];
            player.Hand = _testDataProvider.GetHand(false);
            //Act
            var actual = _europeanBlackJack.GetWinner(dealer, player);
            //Assert
            Assert.AreEqual(player, actual);
        }

        [Test]
        public void Should_determine_dealer_as_winner_because_equal_hands()
        {
            //Arrange
            var players = _testDataProvider.GetPlayers();
            var player = players[0];
            var dealer = players[1];
            player.Hand = _testDataProvider.GetHand(false);
            dealer.Hand = _testDataProvider.GetHand(false);
            //Act
            var actual = _europeanBlackJack.GetWinner(dealer, player);
            //Assert
            Assert.AreEqual(dealer, actual);
        }
    }
}
