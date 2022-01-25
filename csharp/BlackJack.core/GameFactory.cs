using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack.core.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlackJack.core
{
    public class GameFactory : IGameFactory
    {
        private readonly ILogger<GameFactory> _logger;
        private readonly List<IGame> _games;

        public GameFactory(ILogger<GameFactory> logger, IEnumerable<IGame> games)
        {
            _logger = logger;
            _games = games.ToList();
        }

        public IGame Create(string gameId)
        {
            try
            {
                if (_games.Find(x => x.Name == gameId) != null)
                {
                    return _games.Find(x => x.Name == gameId);
                }

                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

    }
}