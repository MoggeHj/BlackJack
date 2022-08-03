using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack.core.Factories;
using BlackJack.core.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlackJack.core
{
    public class GameFactory : GameProviderFactory
    {
        private readonly ILogger<GameFactory> _logger;
        private readonly List<IGame> _games;

        public GameFactory(ILogger<GameFactory> logger, IEnumerable<IGame> games)
        {
            _logger = logger;
            _games = games.ToList();
        }

        public override IGame CreateGame(string game)
        {
            try
            {
                if (_games.Find(x => x.Name == game) != null)
                {
                    return _games.Find(x => x.Name == game);
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