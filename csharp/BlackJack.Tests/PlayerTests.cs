using BlackJack.core;
using BlackJack.core.Interfaces;
using NUnit.Framework;

namespace BlackJack.Tests
{
    public class PlayerTests : Scenario
    {

        private IPlayer _player;
        public override void When()
        {
            _player = new Player();
        }

        [Test]
        public void Should_be_in_the_game() => Assert.AreEqual(Status.InGame, _player.Status );

        [Test]
        public void Should_have_empty_hand() => Assert.AreEqual(0, _player.Hand.Count);
    }
}